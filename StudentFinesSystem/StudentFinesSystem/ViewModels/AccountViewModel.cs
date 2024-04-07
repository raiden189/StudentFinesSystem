using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Pages;
using StudentFinesSystem.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged(); }
        }

        public Command ChangePasswordCommand { get; }

        public Command OnAppearingCommand { get; }

        private readonly IAPIHelper _apiHelper;

        private readonly ICustomPopupService _popUp;

        private readonly ILoginUser _loginUser;

        private string fullName;

        private string email;

        private string gender;

        public AccountViewModel(IAPIHelper aPIHelper, ICustomPopupService popup, ILoginUser loginUser)
        {
            this._apiHelper = aPIHelper;
            this._loginUser = loginUser;
            this._popUp = popup;
            InitializePopup();
            ChangePasswordCommand = new Command(OnChangePassword);
            OnAppearingCommand = new Command(OnAppear);

        }

        private void InitializePopup()
        {
            _popUp.Init(App.Current.MainPage);
        }

        private void OnAppear()
        {
            FullName = $"{_loginUser.FirstName} {_loginUser.MiddleName} {_loginUser.LastName}";
            Email = _loginUser.Username;
            Gender = _loginUser.Gender;
        }

        private void OnChangePassword()
        {
            var changePassPopup = new ChangePasswordPage(_apiHelper, _loginUser);
            _popUp.Show(changePassPopup);
        }
    }
}
