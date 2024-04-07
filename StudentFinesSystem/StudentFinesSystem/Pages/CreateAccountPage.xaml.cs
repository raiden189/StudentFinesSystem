using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;

namespace StudentFinesSystem;

public partial class CreateAccountPage : ContentPage
{
	public CreateAccountPage(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser)
	{
		InitializeComponent();
		this.BindingContext = new CreateAccountViewModel(aPIHelper, customPopupService, loginUser);
	}
}