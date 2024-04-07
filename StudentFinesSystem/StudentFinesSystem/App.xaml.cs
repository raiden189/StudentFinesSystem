using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;

namespace StudentFinesSystem
{
    public partial class App : Application
    {
        public App(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser)
        {
            InitializeComponent();

            MainPage = new LoginPage(aPIHelper, customPopupService, loginUser);
        }
    }
}
