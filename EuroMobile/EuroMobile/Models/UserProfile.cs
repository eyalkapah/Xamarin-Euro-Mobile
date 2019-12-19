using Prism.Mvvm;
using System;

namespace EuroMobile.Models
{
    public class UserProfile : BindableBase
    {
        private string _bio;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _profileImage;

        public string Bio
        {
            get => _bio;
            set => SetProperty(ref _bio, value, () => RaisePropertyChanged(nameof(FullName)));
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string FullName => $"{FirstName} {LastName}";

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value, () => RaisePropertyChanged(nameof(FullName)));
        }

        public string ProfileImage
        {
            get => _profileImage;
            private set => SetProperty(ref _profileImage, value);
        }

        internal void SetProfileImage(string path)
        {
            ProfileImage = path;
        }
    }
}