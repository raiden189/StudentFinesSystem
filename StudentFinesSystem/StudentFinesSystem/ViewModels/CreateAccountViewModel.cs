using CommunityToolkit.Maui.Converters;
using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.API;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;
using StudentFinesSystem.Validations;
using StudentFinesSystem.Validations.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.ViewModels
{
    public class CreateAccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ValidatableObject<string> UserName { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; private set; } = new ValidatableObject<string>();
        public ValidatableObject<string> ConfirmPassword { get; private set; } = new ValidatableObject<string>();

        public bool IsVisibleUsernameError
        {
            get { return isUserVisible; }
            set { isUserVisible = value; OnPropertyChanged(); }
        }

        public bool IsVisiblePassError
        {
            get { return isPasswordVisible; }
            set { isPasswordVisible = value; OnPropertyChanged(); }
        }

        public bool IsVisibleConPassError
        {
            get { return isConPassVisible; }
            set { isConPassVisible = value; OnPropertyChanged(); }
        }

        private bool isUserVisible;
        private bool isPasswordVisible;
        private bool isConPassVisible;

        public Command LoginCommand { get; }

        public Command SignUpCommand { get; }

        private readonly IAPIHelper _apiHelper;

        private readonly ICustomPopupService _popUp;

        private readonly ILoginUser _loginUser;

        public CreateAccountViewModel(IAPIHelper aPIHelper, ICustomPopupService popup, ILoginUser loginUser)
        {
            this._apiHelper = aPIHelper;
            this._popUp = popup;
            this._loginUser = loginUser;
            _popUp.Init(App.Current.MainPage);
            LoginCommand = new Command(OnLoginClicked);
            SignUpCommand = new Command(OnSignUpClick);
            AddValidations();
        }

        private async void OnSignUpClick(object obj)
        {
            var loader = new LoaderPage();
            try
            {
                if (Validate())
                {
                    if (this.Password.Value.Equals(this.ConfirmPassword.Value))
                    {
                        _popUp.Init(App.Current.MainPage);
                        _popUp.Show(loader);

                        var response = await _apiHelper.Register<AccountAPIResponse<string>, AccountModel>(new Models.AccountModel 
                        {
                            Username = UserName.Value,
                            Password = Password.Value,
                            Role = "Student"
                        });

                        if (response.ErrorList?.Any() ?? false)
                        {
                            string errorMsg = response.ErrorList.FirstOrDefault().Description;
                            await App.Current.MainPage.DisplayAlert("Error", errorMsg, "OK");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Success", "Account successfully created!", "OK");
                            _popUp.Close(loader);
                            App.Current.MainPage = new LoginPage(_apiHelper, _popUp, _loginUser);
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Password doesn't match", "OK");
                        return;
                    }
                }
                else
                {
                    IsVisibleUsernameError = UserName.Errors.Any();
                    IsVisiblePassError = Password.Errors.Any();
                    IsVisibleConPassError = ConfirmPassword.Errors.Any();
                }
            }
            catch (Exception)
            {
                if(_popUp.IsShow())
                { _popUp.Close(loader); }
                await App.Current.MainPage.DisplayAlert("Error", "Error on creating account", "OK");
            }
        }

        private void Clear()
        {
            UserName = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            ConfirmPassword = new ValidatableObject<string>();
        }

        private bool Validate()
        {
            bool isUserValid = ValidateUsername();
            bool isPassValid = ValidatePassword();
            bool isConPassValid = ValidateConfirmPassword();
            return isUserValid && isPassValid && isConPassValid;
        }

        private bool ValidateUsername()
        {
            return UserName.Validate();
        }

        private bool ValidatePassword()
        {
            return Password.Validate();
        }

        private bool ValidateConfirmPassword()
        {
            return ConfirmPassword.Validate();
        }

        private void AddValidations()
        {
            UserName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Username is required."
            });

            UserName.Validations.Add(new EmailRule<string>
            {
                ValidationMessage = "Invalid email."
            });

            Password.Validations.AddRange(PasswordRules());
            ConfirmPassword.Validations.AddRange(PasswordRules("Confirm "));
        }

        private List<IValidationRule<string>> PasswordRules(string prefix = "")
        {
            List<IValidationRule<string>> passRules = new List<IValidationRule<string>>();

            passRules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = $"{prefix}Password is required."
            });

            passRules.Add(new PasswordRuleDigit<string> 
            {
                ValidationMessage = $"{prefix}Password should contain digit."
            });

            passRules.Add(new PasswordRuleLowerCase<string>
            {
                ValidationMessage = $"{prefix}Password should contain lower case."
            });

            passRules.Add(new PasswordRuleUpperCase<string>
            {
                ValidationMessage = $"{prefix}Password should contain upper case."
            });

            passRules.Add(new PasswordRuleNonAlphaNumeric<string>
            {
                ValidationMessage = $"{prefix}Password should contain punctuations."
            });

            passRules.Add(new PasswordRuleStrength<string>
            {
                ValidationMessage = $"{prefix}Password length should be greater than or equal to 5."
            });

            passRules.Add(new PasswordRuleUniqueChars<string>
            {
                ValidationMessage = $"{prefix}Password should contain unique character."
            });

            passRules.Add(new PasswordRuleUniqueChars<string>
            {
                ValidationMessage = $"{prefix}Password should contain unique character."
            });

            return passRules;
        }

        private void OnLoginClicked(object obj)
        {
            App.Current.MainPage = new LoginPage(_apiHelper, _popUp, _loginUser);
        }
    }
}
