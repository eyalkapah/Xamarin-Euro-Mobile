using Euro.Shared.In;
using Euro.Shared.Out;
using EuroMobile.Services;
using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EuroMobile.ViewModels
{
    public class AddMatchPageViewModel : ViewModelBase
    {
        private readonly IGroupService _groupService;
        private readonly IMatchService _matchService;
        private readonly ITeamService _teamService;
        private List<TeamResultApiModel> _allTeams;
        private List<GroupResultApiModel> _groups;
        private List<TeamResultApiModel> _guestTeams;
        private DateTime _matchDate;
        private TimeSpan _matchTime;
        private GroupResultApiModel _selectedGroup;
        private TeamResultApiModel _selectedGuestTeam;
        private TeamResultApiModel _selectedHomeTeam;
        private List<TeamResultApiModel> _teams;

        public List<GroupResultApiModel> Groups
        {
            get => _groups;
            set => SetProperty(ref _groups, value);
        }

        public List<TeamResultApiModel> GuestTeams
        {
            get => _guestTeams;
            set => SetProperty(ref _guestTeams, value);
        }

        public DateTime MatchDate
        {
            get => _matchDate;
            set => SetProperty(ref _matchDate, value);
        }

        public TimeSpan MatchTime
        {
            get => _matchTime;
            set => SetProperty(ref _matchTime, value);
        }

        public ICommand SaveCommand { get; set; }

        public GroupResultApiModel SelectedGroup
        {
            get => _selectedGroup;
            set => SetProperty(ref _selectedGroup, value, SelectedGroupChanged);
        }

        public TeamResultApiModel SelectedGuestTeam
        {
            get => _selectedGuestTeam;
            set => SetProperty(ref _selectedGuestTeam, value);
        }

        public TeamResultApiModel SelectedHomeTeam
        {
            get => _selectedHomeTeam;
            set => SetProperty(ref _selectedHomeTeam, value, SelectedHomeTeamChanged);
        }

        public List<TeamResultApiModel> Teams
        {
            get => _teams;
            set => SetProperty(ref _teams, value);
        }

        public AddMatchPageViewModel(INavigationService navigationService, ITeamService teamService, IMatchService matchService, IGroupService groupService) : base(navigationService)
        {
            _teamService = teamService;
            _matchService = matchService;
            _groupService = groupService;
            MatchDate = DateTime.Now;

            SaveCommand = new DelegateCommand(SaveAsync);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await FetchGroupsAsync();
            await FetchTeamsAsync();
        }

        private async Task FetchGroupsAsync()
        {
            try
            {
                var response = await _groupService.GetAllGroupsAsync();

                var result = await response.HandleSuccessfullGetAllGroups();

                Groups = result.ToList();
            }
            catch (Exception ex)
            {
                await IoC.PageDialog.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }

        private async Task FetchTeamsAsync()
        {
            try
            {
                var response = await _teamService.GetAllTeamsAsync();

                var result = await response.HandleSuccessfullGetAllTeams();

                _allTeams = result.ToList();
            }
            catch (Exception ex)
            {
                await IoC.PageDialog.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }

        private async void SaveAsync()
        {
            try
            {
                var matchDateTime = MatchDate + MatchTime;

                var response = await _matchService.AddMatchAsync(SelectedHomeTeam.TeamId, SelectedGuestTeam.TeamId, matchDateTime, SelectedGroup.GroupId);

                var result = await response.HandleSuccessfullAddMatchAsync();

                await NavigationService.NavigateAsync(typeof(MatchesPage).Name);
            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }

        private void SelectedGroupChanged()
        {
            SelectedHomeTeam = null;
            SelectedGuestTeam = null;

            var teams = _allTeams.OrderBy(t => t.Name).ToList();

            Teams = SelectedGroup.IsGroupLevel ? teams.Where(t => t.GroupId.Equals(SelectedGroup.GroupId)).ToList() : teams;
        }

        private void SelectedHomeTeamChanged()
        {
            if (SelectedHomeTeam == null || Teams == null)
                return;

            var teams = SelectedGroup.IsGroupLevel ? Teams : _allTeams.OrderBy(t => t.Name).ToList();

            GuestTeams = teams.Where(t => t.TeamId != SelectedHomeTeam.TeamId).ToList();
        }
    }
}