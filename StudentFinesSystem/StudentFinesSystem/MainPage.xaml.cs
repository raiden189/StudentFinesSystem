namespace StudentFinesSystem
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Login();
        }

        private async Task Login()
        {
            string userName = Username.Text;
            string password = Password.Text;

            if (userName == "admin" && password == "123")
            {
                await Shell.Current.GoToAsync("//FinesListPage");
                await DisplayAlert("Login", "Login successfully", "OK");
            }
            else
                await DisplayAlert("Login", "Incorrect username or password", "OK");
        }
    }
        
}
