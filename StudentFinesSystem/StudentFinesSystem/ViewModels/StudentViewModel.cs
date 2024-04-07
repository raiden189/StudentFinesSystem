using StudentFinesSystem.Helpers;
using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Pages;
using StudentFinesSystem.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentFinesSystem.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Student> Students
        {
            get
            { return students; }
            set
            { students = value; OnPropertyChanged(); }
        }

        public bool IsRefreshing
        {
            get
            { return isRefreshing; }
            set
            {
                if (isRefreshing != value)
                    isRefreshing = value; OnPropertyChanged();
            }
        }

        public Command OnAppearingCommand { get; }

        public Command OnDisapearCommand { get; }

        public Command ViewCommand { get; }

        public Command RefreshCommand { get; }

        private bool isRefreshing = false;

        private ObservableCollection<Student> students;

        private readonly IAPIHelper _aPIHelper;

        private ICustomPopupService _popUp;

        private ILoginUser _loginUser;

        public StudentViewModel(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser)
        {
            _aPIHelper = aPIHelper;
            _popUp = customPopupService;
            _loginUser = loginUser;
            OnAppearingCommand = new Command(OnAppearing);
            ViewCommand = new Command(OnView);
            RefreshCommand = new Command(OnRefresh);
        }

        protected async void OnAppearing()
        {
            if(Students == null)
                Students = await GetStudents();
        }

        private async void OnRefresh()
        {
            if (Students.Any())
            {
                IsRefreshing = true;
                Students.Clear();
            }
            Students = await GetStudents();
            IsRefreshing = false;
        }

        private async void OnView(object sender)
        {
            Student student = sender as Student;
            App.Current.MainPage = new StudentViewPage(_aPIHelper, _popUp, _loginUser, student);
        }

        private async Task<ObservableCollection<Student>> GetStudents()
        {
            ObservableCollection<Student> obStudents = new ObservableCollection<Student>();

            ImageSource maleImgSource = ImageSource.FromFile("male.png");
            ImageSource femaleImgSource = ImageSource.FromFile("female.png");

            var students = await _aPIHelper.GetAllStudents();
            foreach (var student in students.OrderBy(o => o.FirstName)) 
            {
                obStudents.Add(new Student
                {
                    UserId = student.UserId,
                    StudentImage = student.Gender == "MALE" ? maleImgSource : femaleImgSource,
                    FullName = $"{student.FirstName} {student.MiddleName} {student.LastName}",
                    Gender = student.Gender
                });
            }
            return obStudents;
        }
    }
}
