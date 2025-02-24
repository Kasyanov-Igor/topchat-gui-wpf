using System.Windows;
using TopChat.Models.Entities;
using TopChat.Services;
using TopChat.Services.Interfaces;

namespace topchat_wpf
{
    public partial class Menu : Window
	{
		private User _user;

		private ADatabaseConnection _connection = new SqliteConnection();

		public Menu(User user)
		{
			InitializeComponent();
			this._user = user;
		}

		private void ClientServerButton_Click(object sender, RoutedEventArgs e)
		{
			Client___Server client___Server = new Client___Server();
			client___Server.Show();
			this.Close();
		}

		private void Client_Button_Click_1(object sender, RoutedEventArgs e)
		{

		}

		private void ContactButton_Click_1(object sender, RoutedEventArgs e)
		{
			ContactList contact = new ContactList(this._user, _connection, new UserContactService(this._connection));
			contact.Show();
			this.Close();
		}
	}
}
