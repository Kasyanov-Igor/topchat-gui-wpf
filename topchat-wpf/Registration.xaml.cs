using System.Windows;
using TopChat.Services.Interfaces;
using TopChat.Services;
using TopChat.Models.Entities;


namespace topchat_wpf
{
    public partial class Registration : Window
    {
        private ADatabaseConnection _databaseConnection;

        private User? _user;

        public Registration()
        {
            this._databaseConnection = new SqliteConnection();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IUserServes userServes = new UserService(this._databaseConnection);

            if (UserLogin.Text != "" || UserPassword.Text != "")
            {
                if (!userServes.Registration(new User() { Login = UserLogin.Text, Password = UserPassword.Text }))
                {
                    MessageBox.Show("Такой логин уже существует. Пожалуйста, придумайте новый.", Name = "ERROR");
                }
                else
                {
                    this._user = userServes.GetUser(UserLogin.Text);
                    Menu viewMenu = new Menu(this._user);
                    viewMenu.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("ENTER LOGIN AND PASSWORD.", Name = "ERROR");
            }

        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void UserPassword_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
