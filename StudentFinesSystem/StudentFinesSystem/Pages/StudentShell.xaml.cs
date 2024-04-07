using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;

namespace StudentFinesSystem.Pages;

public partial class StudentShell : Shell
{
    private IAPIHelper _apiHelper;
    private ICustomPopupService _customPopupService;
    private ILoginUser _loginUser;

    public StudentShell(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser)
	{
		InitializeComponent();
        RegisterRoutes();
        this._apiHelper = aPIHelper;
        this._loginUser = loginUser;
        this._customPopupService = customPopupService;
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(MyFinesPage), typeof(MyFinesPage));
        Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
    }

    private void LogOut_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage(_apiHelper, _customPopupService, _loginUser);
    }
}