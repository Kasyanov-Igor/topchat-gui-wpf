using System.Windows;
using TopChat.Services.Interfaces;
using TopChat.Services;
using TopChat.Models.Entities;

namespace topchat_wpf
{
    public partial class MainWindow : Window
	{
		private ADatabaseConnection _databaseConnection;

		private User? _user;

		public MainWindow()
		{
			this._databaseConnection = new SqliteConnection();
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			IUserServes userServes = new UserService(this._databaseConnection);

			if (UserLogin.Text != "" || UserPassword.Text != "")
			{
				if (userServes.FindUser(new User() { Login = UserLogin.Text, Password = UserPassword.Text }))
				{
					this._user = userServes.GetUser(UserLogin.Text);
					Menu menu = new Menu(this._user, this._databaseConnection, new ConnectionProviderUdp(), new DataConverterService());
					menu.Show();
					this.Close();
				}
				else
				{
					Registration registration = new Registration();
					registration.Show();
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

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Registration registration = new Registration();
			registration.Show();
			this.Close();
		}
	}
}