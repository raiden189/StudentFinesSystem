using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;

namespace StudentFinesSystem;

public partial class AccountPage : ContentPage
{
	public AccountPage(IAPIHelper apiHelper, ICustomPopupService customPopupService, ILoginUser loginUser)
	{
		InitializeComponent();
		this.BindingContext = new AccountViewModel(apiHelper, customPopupService, loginUser);
	}
}