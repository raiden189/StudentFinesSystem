using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;
using StudentFinesSystem.Pages;
using StudentFinesSystem.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.ViewModels
{
    public class UpdatePopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string FineName
        {
            get
            { return fineName; }
            set
            { 
                if(fineName != value)
                    fineName = value; OnPropertyChanged();
            }
        }

        public string FineDescription
        {
            get
            { return fineDescription; }
            set
            {
                if (fineDescription != value)
                    fineDescription = value; OnPropertyChanged(); 
            }
        }

        public decimal Fine
        {
            get
            { return fine; }
            set
            {
                if (fine != value)
                    fine = value; OnPropertyChanged(); 
            }
        }

        public Command UpdateCommand { get; }

        private decimal fine;

        private string fineName;

        private string fineDescription;

        private readonly IAPIHelper _aPIHelper;

        private readonly ICustomPopupService _popUp;

        private readonly ILoginUser _loginUser;

        private int fineId;

        private UpdatePopUpPage _mainPage;

        public UpdatePopupViewModel(IAPIHelper aPIHelper, ICustomPopupService popup, ILoginUser loginUser,
            FinesList finesList, UpdatePopUpPage mainPage)
        {
            _aPIHelper = aPIHelper;
            _popUp = popup;
            _loginUser = loginUser;
            _mainPage = mainPage;

            if (finesList != null)
            {
                fineId = int.Parse(finesList.Id);
                Fine = decimal.Parse(finesList.Fine.Split(' ')[1]);
                FineName = finesList.FineName;
                FineDescription = finesList.FineDescription;
            }

            UpdateCommand = new Command(OnUpdate);
        }

        private async void OnUpdate()
        {
            if (Fine > 0)
            {
                await Task.Run(() =>
                {
                    _aPIHelper.UpdateFine(new FinesListModel
                    {
                        Id = fineId,
                        Fine = fine,
                        FineName = FineName,
                        FineDescription = FineDescription
                    });
                });
                await App.Current.MainPage.DisplayAlert("Success", "Data updated successfully!", "OK");
                _mainPage.Close();
            }
            else
                await App.Current.MainPage.DisplayAlert("Error", "Incorrect amount!", "OK");
        }
    }
}
