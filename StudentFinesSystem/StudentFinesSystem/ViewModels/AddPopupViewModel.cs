using StudentFinesSystem.Helpers.Interface;
using StudentFinesSystem.Models;
using StudentFinesSystem.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.ViewModels
{
    public class AddPopupViewModel : INotifyPropertyChanged
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
                if (fineName != value)
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

        public Command AddCommand { get; }

        private decimal fine;

        private string fineName;

        private string fineDescription;

        private readonly IAPIHelper _aPIHelper;

        private AddPopUpPage _popUpPage;

        public AddPopupViewModel(IAPIHelper aPIHelper, AddPopUpPage popUpPage)
        {
            _aPIHelper = aPIHelper;
            _popUpPage = popUpPage;
            AddCommand = new Command(OnAdd);
        }

        private async void OnAdd()
        {
            if (Fine > 0)
            {
                await Task.Run(() =>
                {
                    _aPIHelper.CreateFine(new FinesListModel
                    {
                        Fine = fine,
                        FineName = FineName,
                        FineDescription = FineDescription
                    });
                });
                await App.Current.MainPage.DisplayAlert("Success", "Data added successfully!", "OK");
                _popUpPage.Close();
            }
            else
                await App.Current.MainPage.DisplayAlert("Error", "Incorrect amount!", "OK");
        }
    }
}
