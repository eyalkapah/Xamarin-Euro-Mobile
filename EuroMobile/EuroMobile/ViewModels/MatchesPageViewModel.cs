using Euro.Shared.Out;
using EuroMobile.Services;
using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EuroMobile.ViewModels
{
    public class MatchesPageViewModel : ViewModelBase
    {
        private readonly IMatchService _matchService;

        private List<MatchResultApiModel> _matches;
        public ICommand AddMatchCommand { get; set; }

        public List<MatchResultApiModel> Matches
        {
            get => _matches;
            set => SetProperty(ref _matches, value);
        }

        public MatchesPageViewModel(INavigationService navigationService, IMatchService matchService) : base(navigationService)
        {
            AddMatchCommand = new DelegateCommand(AddMatch);
            _matchService = matchService;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                await FetchAllMatches();
            }
            catch (Exception ex)
            {
                await IoC.PageDialog.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }

        private async void AddMatch()
        {
            await NavigationService.NavigateAsync(typeof(AddMatchPage).Name);
        }

        private async Task FetchAllMatches()
        {
            var response = await _matchService.GetAllMatchesAsync();

            var result = await response.HandleSuccessfullGetAllMatches();

            Matches = result.ToList();
        }
    }
}