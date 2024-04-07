using StudentFinesSystem.Models;
using StudentFinesSystem.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StudentFinesSystem.Controls
{
    public class StudentSearchHandler : SearchHandler
    {
        public static readonly BindableProperty StudentsProperty
            = BindableProperty.Create("Students", typeof(ObservableCollection<Student>), typeof(StudentSearchHandler), null);
        public ObservableCollection<Student> Students
        {
            get => (ObservableCollection<Student>)GetValue(StudentsProperty);
            set => SetValue(StudentsProperty, value);
        }

        public Type SelectedItemNavigationTarget { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Students
                    .Where(w => w.FullName.ToLower().Contains(newValue.ToLower()))
                    .ToList<Student>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            // Let the animation complete
            //await Task.Delay(1000);

            //ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            // The following route works because route names are unique in this app.
            //var currentPage = GetNavigationTarget();
            //var navigationParameter = new Dictionary<string, object>
            //{
            //    { "Students", new List<Student> { (Student)item } }
            //};

            //await Shell.Current.GoToAsync($"{state.Location.OriginalString}", navigationParameter);
            //await Shell.Current.GoToAsync($"{currentPage}?FullName={((Student)item).FullName}");
        }

        string GetNavigationTarget()
        {
            //return string.Empty;
            return (Shell.Current as AdminShell).CurrentState.Location.AbsolutePath;//.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
        }
    }
}
