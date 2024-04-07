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

namespace StudentFinesSystem.ViewModels
{
    public class FinesListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<FinesList> Fines
        {
            get
            { return fines; }
            set
            { 
                if(fines != value)
                    fines = value; OnPropertyChanged(); 
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

        public Command UpdateCommand { get; }

        public Command RefreshCommand { get; }

        private bool isRefreshing = false;

        private ObservableCollection<FinesList> fines;

        private readonly IAPIHelper _aPIHelper;

        private readonly ICustomPopupService _popUp;

        private readonly ILoginUser _loginUser;

        public FinesListViewModel(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser)
        {
            _aPIHelper = aPIHelper;
            _popUp = customPopupService;
            _loginUser = loginUser;
            OnAppearingCommand = new Command(OnAppearing);
            AddCommand = new Command(OnAdding);
            UpdateCommand = new Command(OnUpdate);
            RefreshCommand = new Command(OnRefresh);
            InitializePopup();
        }

        private void InitializePopup()
        {
            var currentMain = (App.Current.MainPage as Shell);
            var currentPage = currentMain.CurrentPage;
            _popUp.Init(currentPage);
        }

        protected async void OnAppearing()
        {
            var loader = new LoaderPage();
            _popUp.Show(loader);
            if (Fines == null)
                Fines = await GetAllFines();

            _popUp.Close(loader);
        }

        private async void OnRefresh()
        {
            if (Fines.Any())
            {
                IsRefreshing = true;
                Fines.Clear();
            }
            Fines = await GetAllFines();
            IsRefreshing = false;
        }

        private void OnAdding()
        {
            var addPage = new AddPopUpPage(_aPIHelper);
            _popUp.Init(App.Current.MainPage);
            _popUp.Show(addPage);
        }

        private void OnUpdate(object sender)
        {
            FinesList finesList = sender as FinesList;
            var updatePage = new UpdatePopUpPage(_aPIHelper, _popUp, _loginUser, finesList);
            _popUp.Init(App.Current.MainPage);
            _popUp.Show(updatePage);
        }

        private async Task<ObservableCollection<FinesList>> GetAllFines()
        {
            ObservableCollection<FinesList> oFines = new ObservableCollection<FinesList>();

            var allFines = await _aPIHelper.GetAllFines();
            foreach (var fine in allFines)
            {
                oFines.Add(new FinesList
                {
                    Id = fine.Id.ToString(),
                    Fine = $"\u20b1 {fine.Fine.ToString("N2")}",
                    FineName = fine.FineName,
                    FineDescription = fine.FineDescription,
                });
            }
            return oFines;
        }
    }
}
