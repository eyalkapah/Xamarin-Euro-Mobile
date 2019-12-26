using Euro.Shared.Out;
using EuroMobile.Models;
using EuroMobile.Services;
using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EuroMobile.ViewModels
{
    public class MatchesPageViewModel : ViewModelBase
    {
        private readonly IMatchService _matchService;
        private readonly ITeamService _teamService;
        private List<TeamResultApiModel> _allTeams;
        private List<MatchResultApiModel> _allMatches;

        public ICommand AddMatchCommand { get; set; }

        public ObservableCollection<Match> Matches { get; set; }

        public MatchesPageViewModel(INavigationService navigationService, IMatchService matchService, ITeamService teamService) : base(navigationService)
        {
            Matches = new ObservableCollection<Match>();

            AddMatchCommand = new DelegateCommand(AddMatch);
            _matchService = matchService;
            _teamService = teamService;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                await FetchAllMatches();
                await FetchAllTeams();

                PopulateMatches();
            }
            catch (Exception ex)
            {
                await IoC.PageDialog.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }

        private void PopulateMatches()
        {
            foreach (var m in _allMatches)
            {
                Matches.Add(new Match
                {
                    GroupId = m.GroupId,
                    GuestScored = m.GuestScored,
                    HostScored = m.HostScored,
                    MatchId = m.MatchId,
                    PlayDateTime = m.PlayDateTime,
                    GuestTeam = _allTeams.First(t => t.TeamId.Equals(m.GuestTeamId)),
                    HomeTeam = _allTeams.First(t => t.TeamId.Equals(m.HostTeamId)),
                });
            }

            var matchGroup = new ObservableCollection<MatchGroup>();

            foreach (var item in _allMatches.GroupBy(m => m.PlayDateTime))
            {
                //item.Key
            }
        }

        private async void AddMatch()
        {
            await NavigationService.NavigateAsync(typeof(AddMatchPage).Name);
        }

        private async Task FetchAllTeams()
        {
            var response = await _teamService.GetAllTeamsAsync();

            var result = await response.HandleSuccessfullGetAllTeams();

            _allTeams = result.ToList();
        }

        private async Task FetchAllMatches()
        {
            var response = await _matchService.GetAllMatchesAsync();

            var result = await response.HandleSuccessfullGetAllMatches();

            _allMatches = result.ToList();
        }
    }
}