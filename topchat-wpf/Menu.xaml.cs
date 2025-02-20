using System.Windows;
using TopChat.Models.Entities;

namespace topchat_wpf
{
    public partial class Menu : Window
	{
		private User _user;

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
			ContactList contact = new ContactList(this._user);
			contact.Show();
			this.Close();
		}
	}
}
