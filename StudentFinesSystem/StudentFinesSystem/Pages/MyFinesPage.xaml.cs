using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;

namespace StudentFinesSystem;

public partial class MyFinesPage : ContentPage
{
	public MyFinesPage(IAPIHelper aPIHelper, ICustomPopupService customPopupService ,ILoginUser loginUser)
	{
		InitializeComponent();
		this.BindingContext = new MyFinesViewModel(aPIHelper, customPopupService, loginUser);
	}
}