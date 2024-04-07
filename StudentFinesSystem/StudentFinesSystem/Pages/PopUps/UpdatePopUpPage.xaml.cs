using CommunityToolkit.Maui.Views;
using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;

namespace StudentFinesSystem.Pages;

public partial class UpdatePopUpPage : Popup
{
	public UpdatePopUpPage(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser, FinesList finesList)
	{
		InitializeComponent();
		this.BindingContext = new UpdatePopupViewModel(aPIHelper, customPopupService, loginUser, finesList, this);
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		this.Close();
    }
}