using CommunityToolkit.Maui.Alerts;
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
    public class AddStudentFineViewModel : INotifyPropertyChanged
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
                if (fines != value)
                    fines = value; OnPropertyChanged();
            }
        }

        public FinesList SelectedFine
        {
            get
            { return selectedFine; }
            set
            {
                if (selectedFine != value)
                    selectedFine = value; OnPropertyChanged();
            }
        }

        public Command AddCommand { get; }

        private IAPIHelper _apiHelper;
        private ICustomPopupService _popUp;
        private ILoginUser _loginUser;
        private string _userId;
        private ObservableCollection<FinesList> fines;
        private FinesList selectedFine;
        private AddStudentPopUpPage popupPage;

        public AddStudentFineViewModel(IAPIHelper aPIHelper, ICustomPopupService customPopupService, ILoginUser loginUser, string userId, AddStudentPopUpPage popUpPage, ObservableCollection<FinesList> finesLists)
        {
            this._apiHelper = aPIHelper;
            this._popUp = customPopupService;
            this._loginUser = loginUser;
            this._userId = userId;
            this.popupPage = popUpPage;
            AddCommand = new Command(OnAdd);
            Fines = finesLists;
        }

        private async void OnAdd()
        {
            await _apiHelper.AddStudentFine(new Models.Fines
            {
                FineId = int.Parse(SelectedFine.Id),
                UserId = _userId,
                CreatedBy = _loginUser.UserId
            });
            await App.Current.MainPage.DisplayAlert("Success", "Successfully added!", "OK");
            popupPage.Close();
        }
    }
}
