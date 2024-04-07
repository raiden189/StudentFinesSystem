using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
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
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public Command LoginCommand { get; }

        public Command SignUpCommand { get; }

        private readonly IAPIHelper _apiHelper;

        private readonly ICustomPopupService _popUp;

        private readonly ILoginUser _loginUser;

        public LoginViewModel(IAPIHelper aPIHelper, ICustomPopupService popup, ILoginUser loginUser)
        {
            this._apiHelper = aPIHelper;
            this._popUp = popup;
            this._loginUser = loginUser;
            InitializePopup();
            LoginCommand = new Command(OnLoginClicked);
            SignUpCommand = new Command(OnSignUpClick);
        }

        private void InitializePopup()
        {
            _popUp.Init(App.Current.MainPage);
        }

        private async void OnLoginClicked(object obj)
        {
            var loader = new LoaderPage();
            try
            {
                _popUp.Show(loader);

                var authenticate = await _apiHelper.Authenticate(this.UserName, this.Password);

                await _apiHelper.GetLoginUser(authenticate);
                await App.Current.MainPage.DisplayAlert("Success", "Successfully log in!", "OK");
                _popUp.Close(loader);

                if (!string.IsNullOrEmpty(_loginUser.FirstName))
                {
                    if (authenticate.IsAdmin)
                    {
                        App.Current.MainPage = new AdminShell(_apiHelper, _popUp, _loginUser);
                    }
                    else
                    {
                        App.Current.MainPage = new StudentShell(_apiHelper, _popUp, _loginUser);
                    }
                }
                else
                {
                    _loginUser.Username = authenticate.Username;
                    App.Current.MainPage = new CreateNamePage(_apiHelper, _popUp, _loginUser);
                }
            }
            catch (Exception ex)
            {
                _popUp.Close(loader);
                await App.Current.MainPage.DisplayAlert("Error", "Incorrect username or password", "OK");
            }
        }

        private void OnSignUpClick(object obj)
        {
            App.Current.MainPage = new CreateAccountPage(_apiHelper, _popUp, _loginUser);
        }
    }
}
