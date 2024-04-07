using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Service;
using System.Collections.ObjectModel;
using StudentFinesSystem.Models;
using StudentFinesSystem.Pages;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentFinesSystem.ViewModels
{
    public class MyFinesViewModel : INotifyPropertyChanged
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

        public string Total
        {
            get
            { return total; }
            set
            {
                if (total != value)
                    total = value; OnPropertyChanged();
            }
        }

        public Command OnAppearingCommand { get; }

        public Command RefreshCommand { get; }

        private bool isRefreshing = false;
        private string total;

        private ObservableCollection<StudentFines> studentFines;

        private IAPIHelper _apiHelper;

        private ICustomPopupService _popUp;

        private ILoginUser _loginUser;
        public MyFinesViewModel(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser)
        {
            this._apiHelper = aPIHelper;
            this._popUp = customPopupService;
            this._loginUser = loginUser;
            OnAppearingCommand = new Command(OnAppear);
            RefreshCommand = new Command(OnRefresh);
        }

        private async void OnAppear()
        {
            var loader = new LoaderPage();
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

        private async Task<ObservableCollection<StudentFines>> GetStudentFines()
        {
            ObservableCollection<StudentFines> fines = new ObservableCollection<StudentFines>();

            if (_loginUser != null)
            {
                var studentFines = await _apiHelper.GetStudentFineById(_loginUser.UserId);
                foreach (var studentFine in studentFines)
                {
                    fines.Add(new StudentFines
                    {
                        Id = studentFine.Id,
                        Fine = $"\u20b1 {studentFine.Fine.ToString("N2")}",
                        FineName = studentFine.FineName,
                        FineDescription = studentFine.FineDescription
                    });
                }
                Total = $"\u20b1 {studentFines.Select(s => s.Fine).Sum().ToString("N2")}";
            }
            return fines;
        }
    }
}
