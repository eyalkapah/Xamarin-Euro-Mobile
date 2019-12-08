using Prism.Mvvm;

namespace EuroMobile.Models
{
    public class UserProfile : BindableBase
    {
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value, () => RaisePropertyChanged(nameof(FullName)));
        }

        private string _bio;

        public string Bio
        {
            get => _bio;
            set => SetProperty(ref _bio, value, () => RaisePropertyChanged(nameof(FullName)));
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}