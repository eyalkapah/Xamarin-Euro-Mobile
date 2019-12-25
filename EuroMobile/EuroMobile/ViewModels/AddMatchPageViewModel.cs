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
        private const string HomeTeamAndGuestTeamAreTheSame = "* Home team and guest team cannot be the same.";
        private const string HomeTeamCannotBeEmpty = "* Home team cannot be empty";
        private const string GuestTeamCannotBeEmpty = "* Guest team cannot be empty";

        private readonly ITeamService _teamService;
        private List<string> _teams;
        private string _selectedHomeTeam;
        private string _selectedGuestTeam;
        private DateTime _matchDate;
        private string __errors;
        private TimeSpan _matchTime;

        public List<string> Teams
        {
            get => _teams;
            set => SetProperty(ref _teams, value);
        }

        public string SelectedHomeTeam
        {
            get => _selectedHomeTeam;
            set => SetProperty(ref _selectedHomeTeam, value, SelectedHomeTeamChanged);
        }

        public string SelectedGuestTeam
        {
            get => _selectedGuestTeam;
            set => SetProperty(ref _selectedGuestTeam, value, SelectedGuestTeamChanged);
        }

        public string Errors
        {
            get => __errors;
            set => SetProperty(ref __errors, value);
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

        public AddMatchPageViewModel(INavigationService navigationService, ITeamService teamService) : base(navigationService)
        {
            _teamService = teamService;

            MatchDate = DateTime.Now;

            SaveCommand = new DelegateCommand(SaveAsync);
        }

        private void SelectedHomeTeamChanged() => ClearErrors();

        private void MatchTimeChanged() => ClearErrors();

        private void MatchDateChanged() => ClearErrors();

        private void SelectedGuestTeamChanged() => ClearErrors();

        private async void SaveAsync()
        {
            ValidateForm();

            if (!string.IsNullOrEmpty(Errors))
            {
                return;
            }

            await NavigationService.NavigateAsync(typeof(MatchesPage).Name);
        }

        private void ValidateForm()
        {
            if (string.IsNullOrEmpty(SelectedHomeTeam))
                Errors += $"{HomeTeamCannotBeEmpty}{Environment.NewLine}";
            if (string.IsNullOrEmpty(SelectedGuestTeam))
                Errors += $"{GuestTeamCannotBeEmpty}{Environment.NewLine}";
            if (!string.IsNullOrEmpty(SelectedHomeTeam) && !string.IsNullOrEmpty(SelectedGuestTeam) && SelectedHomeTeam.Equals(SelectedGuestTeam))
                Errors += $"{HomeTeamAndGuestTeamAreTheSame}{Environment.NewLine}";
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await FetchTeams();
        }

        private async Task FetchTeams()
        {
            try
            {
                var response = await _teamService.GetAllTeamsAsync();

                var teams = await response.HandleSuccessfullGetAllTeams();

                var orderedTeams = teams.OrderBy(c => c.Name).Select(c => c.Name);
                Teams = orderedTeams.ToList();
            }
            catch (Exception ex)
            {
                await IoC.PageDialog.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }

        private void ClearErrors()
        {
            Errors = string.Empty;
        }
    }
}