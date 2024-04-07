using CommunityToolkit.Maui.Views;
using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;

namespace StudentFinesSystem.Pages;

public partial class ChangePasswordPage : Popup
{
	public ChangePasswordPage(IAPIHelper apiHelper, ILoginUser loginUser)
	{
		InitializeComponent();
		this.BindingContext = new ChangePasswordViewModel(this, apiHelper, loginUser);
	}
}