using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;

namespace StudentFinesSystem;

public partial class FinesListPage : ContentPage
{
    public StudentModel Students { get; set; }
    public FinesListPage(IAPIHelper apiHelper, ICustomPopupService customPopupService, ILoginUser loginUser)
	{
		InitializeComponent();
		BindingContext = new FinesListViewModel(apiHelper, customPopupService, loginUser);
	}
}	