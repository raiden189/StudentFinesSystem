using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Pages;
using StudentFinesSystem.Service;

namespace StudentFinesSystem
{
    public partial class LoginPage : ContentPage
    { 
        public LoginPage(IAPIHelper apiHelper, ICustomPopupService customPopupService, ILoginUser loginUser)
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel(apiHelper, customPopupService, loginUser);
        }
    }
}
