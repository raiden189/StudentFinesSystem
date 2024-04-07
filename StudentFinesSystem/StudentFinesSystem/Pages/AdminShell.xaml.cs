using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;

namespace StudentFinesSystem.Pages;

public partial class AdminShell : Shell
{
    private IAPIHelper _apiHelper;
    private ICustomPopupService _customPopupService;
    private ILoginUser _loginUser;
	public AdminShell(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser)
	{
		InitializeComponent();
        RegisterRoutes();
        this._apiHelper = aPIHelper;
        this._loginUser = loginUser;
        this._customPopupService = customPopupService;

    }
    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(StudentsPage), typeof(StudentsPage));
        Routing.RegisterRoute(nameof(FinesListPage), typeof(FinesListPage));
        Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
        Routing.RegisterRoute(nameof(StudentViewPage), typeof(StudentViewPage));
    }

    private void LogOut_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage(_apiHelper, _customPopupService, _loginUser);
    }
}