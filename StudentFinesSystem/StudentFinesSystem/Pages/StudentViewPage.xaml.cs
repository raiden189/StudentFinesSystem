using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;
using System.ComponentModel;

namespace StudentFinesSystem.Pages;

public partial class StudentViewPage : ContentPage
{
    public StudentViewPage(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser, Student student)
	{
		InitializeComponent();
        this.BindingContext = new ViewStudentViewModel(aPIHelper, customPopupService, loginUser, student);
    }
}