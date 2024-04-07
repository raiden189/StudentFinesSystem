using CommunityToolkit.Maui.Views;
using StudentFinesSystem.Helpers.Interface;

namespace StudentFinesSystem.Pages;

public partial class AddPopUpPage : Popup
{
	public AddPopUpPage(IAPIHelper aPIHelper)
	{
		InitializeComponent();
		this.BindingContext = new AddPopupViewModel(aPIHelper, this);
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		this.Close();
    }
}