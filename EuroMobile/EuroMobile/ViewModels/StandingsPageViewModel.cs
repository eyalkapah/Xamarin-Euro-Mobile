using EuroMobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EuroMobile.ViewModels
{
    public class StandingsPageViewModel : ViewModelBase
    {
        private List<Team> _teams;

        public List<Team> Teams
        {
            get => _teams;
            set => SetProperty(ref _teams, value);
        }

        public StandingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var teams = new List<Team>
            {
                new Team
                {
                    Position = 1,
                    Name = "England",
                    Flag = "eng.png"
                },
                new Team
                {
                    Position = 2,
                    Name = "Czech Republic",
                    Flag = "cze.png"
                }
            };

            Teams = teams;
        }
    }
}