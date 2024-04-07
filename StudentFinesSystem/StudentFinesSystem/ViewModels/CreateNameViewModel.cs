using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Pages;
using StudentFinesSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.ViewModels
{
    public class CreateNameViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        private readonly IAPIHelper _apiHelper;

        private readonly ILoginUser _loginUser;

        private readonly ICustomPopupService _popUp;

        public Command SaveUserCommand { get; }

        public CreateNameViewModel(IAPIHelper aPIHelper, ICustomPopupService popup, ILoginUser loginUser)
        {
            this._apiHelper = aPIHelper;
            this._popUp = popup;
            this._loginUser = loginUser;
            InitializePopup();
            SaveUserCommand = new Command(OnSaveUser);
        }

        private void InitializePopup()
        {
            _popUp.Init(App.Current.MainPage);
        }

        private async void OnSaveUser()
        {
            var loader = new LoaderPage();
            try
            {
                _popUp.Show(loader);

                await _apiHelper.SaveUser(
                    new CreateNameModel 
                    { 
                        firstname = FirstName, 
                        middlename = MiddleName, 
                        lastname = LastName, 
                        gender = Gender 
                    });

                await App.Current.MainPage.DisplayAlert("Success", "Successfully saved!", "OK");

                _loginUser.FirstName = FirstName;
                _loginUser.MiddleName = MiddleName;
                _loginUser.LastName = LastName;
                _loginUser.Gender = Gender;

                _popUp.Close(loader);

                if (_loginUser.IsAdmin)
                {
                    App.Current.MainPage = new AdminShell(_apiHelper, _popUp, _loginUser);
                }
                else
                {
                    App.Current.MainPage = new StudentShell(_apiHelper, _popUp, _loginUser);
                }
            }
            catch (Exception)
            {
                if (_popUp.IsShow())
                    _popUp.Close(loader);
                await App.Current.MainPage.DisplayAlert("Error", "Error on saving!", "OK");
                throw;
            }
        }
    }
}
