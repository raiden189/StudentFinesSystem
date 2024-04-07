using CommunityToolkit.Maui.Views;
using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;
using System.Collections.ObjectModel;

namespace StudentFinesSystem.Pages;

public partial class AddStudentPopUpPage : Popup
{
	public AddStudentPopUpPage(IAPIHelper apiHelper, ICustomPopupService customPopupService, ILoginUser loginUser, string userId, ObservableCollection<FinesList> finesLists)
	{
		InitializeComponent();
		this.BindingContext = new AddStudentFineViewModel(apiHelper, customPopupService, loginUser, userId, this, finesLists);
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		this.Close();
    }
}