using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;

namespace StudentFinesSystem.Pages;

public partial class CreateNamePage : ContentPage
{
	public CreateNamePage(IAPIHelper aPIHelper, ICustomPopupService popupService, ILoginUser loginUser)
	{
		InitializeComponent();
		this.BindingContext = new CreateNameViewModel(aPIHelper, popupService, loginUser);
	}
}