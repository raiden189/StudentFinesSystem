using CommunityToolkit.Maui.Views;
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
    public class ChangePasswordViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ValidatableObject<string> CurrentPassword { get; set; } = new ValidatableObject<string>();

        public ValidatableObject<string> NewPassword { get; set; } = new ValidatableObject<string>();

        public ValidatableObject<string> ConfirmPassword { get; set; } = new ValidatableObject<string>();

        public Command ChangePasswordCommand { get; }

        public Command CloseCommand { get; }

        public bool IsVisibleCurrentPassError
        {
            get { return isCurrentPasswordVisible; }
            set { isCurrentPasswordVisible = value; OnPropertyChanged(); }
        }

        public bool IsVisibleNewPassError
        {
            get { return isNewPassVisible; }
            set { isNewPassVisible = value; OnPropertyChanged(); }
        }

        public bool IsVisibleConNewPassError
        {
            get { return isConNewPassVisible; }
            set { isConNewPassVisible = value; OnPropertyChanged(); }
        }

        private bool isCurrentPasswordVisible;
        private bool isNewPassVisible;
        private bool isConNewPassVisible;

        private readonly IAPIHelper _apiHelper;

        private readonly ILoginUser _loginUser;

        private Popup currentPopup;

        public ChangePasswordViewModel(Popup popup, IAPIHelper aPIHelper, ILoginUser loginUser)
        {
            this.currentPopup = popup;
            this._apiHelper = aPIHelper;
            this._loginUser = loginUser;
            ChangePasswordCommand = new Command(OnChangePasswordClick);
            CloseCommand = new Command(OnCloseClick);
            AddValidations();
        }

        private async void OnChangePasswordClick()
        {
            try
            {
                if (Validate())
                {
                    if (NewPassword.Value.Equals(ConfirmPassword.Value))
                    {
                        var response = await _apiHelper.ChangePassword<AccountAPIResponse<string>, AccountModel>(
                            new AccountModel 
                            { 
                                Username = _loginUser.Username,
                                Password = CurrentPassword.Value,
                                NewPassword = NewPassword.Value
                            });

                        if (response.ErrorList?.Any() ?? false)
                        {
                            string errorMsg = response.ErrorList.FirstOrDefault().Description;
                            await App.Current.MainPage.DisplayAlert("Error", errorMsg, "OK");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Success", "Password successfully changed!", "OK");
                            currentPopup.Close();
                        }
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "Password doesn't match", "OK");
                }
                else
                {
                    IsVisibleCurrentPassError = CurrentPassword.Errors.Any();
                    IsVisibleNewPassError = NewPassword.Errors.Any();
                    IsVisibleConNewPassError = ConfirmPassword.Errors.Any();
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error on updating password", "OK");
            }
        }

        private void OnCloseClick()
        {
            currentPopup.Close();
        }

        private bool Validate()
        {
            bool isPassValid = ValidateCurrentPassword();
            bool isNewPassValid = ValidateNewPassword();
            bool isConNewPassValid = ValidateConfirmNewPassword();
            return isPassValid && isNewPassValid && isConNewPassValid;
        }

        private bool ValidateCurrentPassword()
        {
            return CurrentPassword.Validate();
        }

        private bool ValidateNewPassword()
        {
            return NewPassword.Validate();
        }

        private bool ValidateConfirmNewPassword()
        {
            return ConfirmPassword.Validate();
        }

        private void AddValidations()
        {
            CurrentPassword.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Current Password is required."
            });

            NewPassword.Validations.AddRange(PasswordRules("New "));
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
    }
}
