using System.Windows;
using TopChat.Services.Interfaces;
using TopChat.Services;

namespace topchat_wpf
{
	public partial class MainWindow : Window
	{
		private ADatabaseConnection _databaseConnection;

		public MainWindow()
		{
			this._databaseConnection = new SqliteConnection();
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			IUserServes userServes = new UserServes(this._databaseConnection);

			if (UserLogin.Text != "" || UserPassword.Text != "")
			{
				if (userServes.FindUser(UserLogin.Text))
				{
					Menu menu = new Menu();
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