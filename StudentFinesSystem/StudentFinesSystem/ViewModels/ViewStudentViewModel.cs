using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Pages;
using StudentFinesSystem.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.ViewModels
{
    public class ViewStudentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<StudentFines> StudentFines
        {
            get
            { return studentFines; }
            set
            {
                if (studentFines != value)
                    studentFines = value; OnPropertyChanged();
            }
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

        public Command AddCommand { get; }

        public Command CloseCommand { get; }

        public Command RefreshCommand { get; }

        public Command DeleteCommand { get; }

        private IAPIHelper _apiHelper;

        private ICustomPopupService _popUp;

        private ILoginUser _loginUser;

        private Student _student;

        private LoaderPage loader;

        private bool isRefreshing = false;

        private ObservableCollection<StudentFines> studentFines;

        public ViewStudentViewModel(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser, Student student)
        {
            this._apiHelper = aPIHelper;
            this._popUp = customPopupService;
            this._loginUser = loginUser;
            this._student = student;
            this.loader = new LoaderPage();
            OnAppearingCommand = new Command(OnAppear);
            RefreshCommand = new Command(OnRefresh);
            AddCommand = new Command(OnAdd);
            CloseCommand = new Command(OnClose);
            DeleteCommand = new Command(OnDelete);
            InitializePopup();
        }

        private void InitializePopup()
        {
            _popUp.Init(App.Current.MainPage);
        }

        private async void OnAppear()
        {
            _popUp.Show(loader);
            StudentFines = await GetStudentFines();
            _popUp.Close(loader);
        }

        private async void OnRefresh()
        {
            if (StudentFines.Any())
            {
                IsRefreshing = true;
                StudentFines.Clear();
            }
            StudentFines = await GetStudentFines();
            IsRefreshing = false;
        }

        private async void OnDelete(object sender)
        {
            StudentFines student = sender as StudentFines;
            var loaderPage = new LoaderPage();
            _popUp.Show(loaderPage);
            await _apiHelper.DeleteStudentFine(new StudentFineDelete
            {
                UserId = _student.UserId,
                FineId = student.FineId,
                Id = student.Id
            });

            _popUp.Close(loaderPage);
            await App.Current.MainPage.DisplayAlert("Success", "Successfully marked!", "OK");
        }

        private async Task<ObservableCollection<StudentFines>> GetStudentFines()
        {
            ObservableCollection<StudentFines> fines = new ObservableCollection<StudentFines>();

            if (_student != null)
            {
                var studentFines = await _apiHelper.GetStudentFineById(_student.UserId);
                foreach (var studentFine in studentFines)
                {
                    fines.Add(new StudentFines 
                    { 
                        Id = studentFine.Id,
                        FineId = studentFine.FineId,
                        Fine = $"\u20b1 {studentFine.Fine.ToString("N2")}",
                        FineName = studentFine.FineName,
                        FineDescription = studentFine.FineDescription
                    });
                }
            }
            return fines;
        }

        private async void OnAdd()
        {
            var allFines = await GetFines();
            var addPage = new AddStudentPopUpPage(_apiHelper, _popUp, _loginUser, _student.UserId, allFines);
            _popUp.Init(App.Current.MainPage);
            _popUp.Show(addPage);
        }

        private void OnClose()
        {
            App.Current.MainPage = new AdminShell(_apiHelper, _popUp, _loginUser);
        }

        private async Task<ObservableCollection<FinesList>> GetFines()
        {
            ObservableCollection<FinesList> dbFines = new ObservableCollection<FinesList>();
            var allFines = await _apiHelper.GetAllFines();

            foreach (var f in allFines)
            {
                dbFines.Add(new FinesList
                {
                    Id = f.Id.ToString(),
                    Fine = f.Fine.ToString(),
                    FineName = f.FineName,
                    FineDescription = f.FineDescription
                });
            }
            return dbFines;
        }
    }
}
