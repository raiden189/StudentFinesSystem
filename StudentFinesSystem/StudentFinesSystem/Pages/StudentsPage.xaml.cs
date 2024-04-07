using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;

namespace StudentFinesSystem;

public partial class StudentsPage : ContentPage
{
	public StudentsPage(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser)
	{
		InitializeComponent();
		this.BindingContext = new StudentViewModel(aPIHelper, customPopupService, loginUser);
	}
}