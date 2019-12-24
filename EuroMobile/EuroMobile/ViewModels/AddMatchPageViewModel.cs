using Euro.Domain.ApiModels;
using EuroMobile.Services;
using EuroMobile.ViewModels.Base;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EuroMobile.ViewModels
{
    public class AddMatchPageViewModel : ViewModelBase
    {
        private readonly ITeamService _teamService;

        private IEnumerable<TeamResultApiModel> _teams;

        public IEnumerable<TeamResultApiModel> Teams
        {
            get => _teams;
            set => SetProperty(ref _teams, value);
        }

        public AddMatchPageViewModel(INavigationService navigationService, ITeamService teamService) : base(navigationService)
        {
            _teamService = teamService;
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

                Teams = await response.HandleSuccessfullGetAllTeams();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}