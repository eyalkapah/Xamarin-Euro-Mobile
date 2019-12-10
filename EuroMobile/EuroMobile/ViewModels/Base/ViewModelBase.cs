using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobile.ViewModels.Base
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        private string _title;
        protected INavigationService NavigationService { get; private set; }
        public IPageDialogService PageDialogService => IoC.PageDialog;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ApplicationViewModel ApplicationViewModel => IoC.ApplicationViewModel;

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {
        }
    }
}