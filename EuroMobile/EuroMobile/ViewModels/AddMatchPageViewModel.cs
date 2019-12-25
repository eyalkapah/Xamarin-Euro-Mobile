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
        private const string GuestTeamCannotBeEmpty = "* Guest team cannot be empty";
        private const string HomeTeamAndGuestTeamAreTheSame = "* Home team and guest team cannot be the same.";
        private const string HomeTeamCannotBeEmpty = "* Home team cannot be empty";
        private readonly IGroupService _groupService;
        private readonly IMatchService _matchService;
        private readonly ITeamService _teamService;
        private string __errors;
        private IEnumerable<GroupResultApiModel> _allGroups;
        private IEnumerable<TeamResultApiModel> _allTeams;
        private string _group;
        private List<string> _groups;
        private DateTime _matchDate;
        private TimeSpan _matchTime;
        private string _selectedGroup;
        private string _selectedGuestTeam;
        private string _selectedHomeTeam;
        private List<string> _teams;

        public string Errors
        {
            get => __errors;
            set => SetProperty(ref __errors, value);
        }

        public string Group
        {
            get => _group;
            set => SetProperty(ref _group, value);
        }

        public List<string> Groups
        {
            get => _groups;
            set => SetProperty(ref _groups, value);
        }

        public DateTime MatchDate
        {
            get => _matchDate;
            set => SetProperty(ref _matchDate, value, MatchDateChanged);
        }

        public TimeSpan MatchTime
        {
            get => _matchTime;
            set => SetProperty(ref _matchTime, value, MatchTimeChanged);
        }

        public ICommand SaveCommand { get; set; }

        public string SelectedGroup
        {
            get => _selectedGroup;
            set => SetProperty(ref _selectedGroup, value, SelectedGroupChanged);
        }

        public string SelectedGuestTeam
        {
            get => _selectedGuestTeam;
            set => SetProperty(ref _selectedGuestTeam, value, SelectedGuestTeamChanged);
        }

        public string SelectedHomeTeam
        {
            get => _selectedHomeTeam;
            set => SetProperty(ref _selectedHomeTeam, value, SelectedHomeTeamChanged);
        }

        public List<string> Teams
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

        private void ClearErrors()
        {
            Errors = string.Empty;
        }

        private async Task FetchGroupsAsync()
        {
            try
            {
                var response = await _groupService.GetAllGroupsAsync();

                _allGroups = await response.HandleSuccessfullGetAllGroups();

                Groups = _allGroups.Select(c => c.Name).ToList();
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

                _allTeams = await response.HandleSuccessfullGetAllTeams();

                Teams = _allTeams.OrderBy(c => c.Name).Select(c => c.Name).ToList();
            }
            catch (Exception ex)
            {
                await IoC.PageDialog.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }

        private void MatchDateChanged() => ClearErrors();

        private void MatchTimeChanged() => ClearErrors();

        private async void SaveAsync()
        {
            try
            {
                ValidateForm();

                if (!string.IsNullOrEmpty(Errors))
                {
                    return;
                }

                var homeTeamId = _allTeams.First(t => t.Name.Equals(SelectedHomeTeam)).TeamId;
                var guestTeamId = _allTeams.First(t => t.Name.Equals(SelectedGuestTeam)).TeamId;
                var matchDateTime = MatchDate + MatchTime;

                var response = await _matchService.AddMatchAsync(homeTeamId, guestTeamId, matchDateTime);

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
        }

        private void SelectedGuestTeamChanged() => ClearErrors();

        private void SelectedHomeTeamChanged() => ClearErrors();

        private void ValidateForm()
        {
            if (string.IsNullOrEmpty(SelectedHomeTeam))
                Errors += $"{HomeTeamCannotBeEmpty}{Environment.NewLine}";
            if (string.IsNullOrEmpty(SelectedGuestTeam))
                Errors += $"{GuestTeamCannotBeEmpty}{Environment.NewLine}";
            if (!string.IsNullOrEmpty(SelectedHomeTeam) && !string.IsNullOrEmpty(SelectedGuestTeam) && SelectedHomeTeam.Equals(SelectedGuestTeam))
                Errors += $"{HomeTeamAndGuestTeamAreTheSame}{Environment.NewLine}";
        }
    }
}