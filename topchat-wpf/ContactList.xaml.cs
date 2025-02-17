using System.Windows;
using TopChat.Models;
using TopChat.Services;
using TopChat.Services.Interfaces;


namespace topchat_wpf
{
	public partial class ContactList : Window
	{

		private ADatabaseConnection _databaseConnection;

		private User _user;

		public ContactList(User user)
		{
			this._databaseConnection = new SqliteConnection();
			this._user = user;

			InitializeComponent();
		}

		private void DeleteContact_Click(object sender, RoutedEventArgs e)
		{
			IUserServes userServes = new UserServes(this._databaseConnection);

			if (this.NameContact.Text != null || this.InfaContact.Text != null)
			{
				string[] ipAndPort = this.InfaContact.Text.Split(":");

				UserContact contact = new UserContact()
				{
					UserName = this.NameContact.Text,
					UserIp = ipAndPort[0],
					UserPort = ipAndPort[1]
				};

				if (userServes.DeleteContact(this._user, contact))
				{
					MessageBox.Show("User successfully added.", Name = "Ok");
				}
			}
			else
			{
				MessageBox.Show("ENTER INF.", Name = "ERROR");
			}
		}

		private void RenameContact_Click(object sender, RoutedEventArgs e)
		{
			IUserServes userServes = new UserServes(this._databaseConnection);

			if (this.NameContact.Text != null || this.InfaContact.Text != null)
			{
				string[] ipAndPort = this.InfaContact.Text.Split(":");

				UserContact contact = new UserContact()
				{
					UserName = this.NameContact.Text,
					UserIp = ipAndPort[0],
					UserPort = ipAndPort[1]
				};

				//if (userServes.RenameContact(this._user, contact))
				//{
				//    MessageBox.Show("User successfully added.", Name = "Ok");
				//}
			}
			else
			{
				MessageBox.Show("ENTER INF.", Name = "ERROR");
			}
		}

		//     private void AddContact_Checked(object sender, RoutedEventArgs e)
		//     {
		//this.LoginTabl.Visibility = Visibility.Visible;
		//         this.IpAdressTabl.Visibility = Visibility.Visible;
		//         this.InfaContact.Visibility = Visibility.Visible;
		//         this.NameContact.Visibility = Visibility.Visible;

		//         IUserServes userServes = new UserServes(this._databaseConnection);

		//         if (this.NameContact.Text != null || this.InfaContact.Text != null)
		//         {
		//             string[] ipAndPort = this.InfaContact.Text.Split(":");

		//             UserContact contact = new UserContact()
		//             {
		//                 UserName = this.NameContact.Text,
		//                 UserIp = ipAndPort[0],
		//                 UserPort = ipAndPort[1]
		//             };

		//             if (userServes.AddContact(this._user, contact))
		//             {
		//                 MessageBox.Show("User successfully added.", Name = "Ok");
		//             }
		//         }
		//         else
		//         {
		//             MessageBox.Show("ENTER INF.", Name = "ERROR");
		//         }
		//     }

		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{
			this.LoginTabl.Visibility = Visibility.Visible;
			this.IpAdressTabl.Visibility = Visibility.Visible;
			this.InfaContact.Visibility = Visibility.Visible;
			this.NameContact.Visibility = Visibility.Visible;
			this.ButtonEnter.Visibility = Visibility.Visible;
		}


		private void ButtonEnter_Click_1(object sender, RoutedEventArgs e)
		{
			IUserServes userServes = new UserServes(this._databaseConnection);

			if (this.NameContact.Text != "" || this.InfaContact.Text != "")
			{
				string[] ipAndPort = this.InfaContact.Text.Split(":");

				UserContact contact = new UserContact()
				{
					UserName = this.NameContact.Text,
					UserIp = ipAndPort[0],
					UserPort = ipAndPort[1]
				};

				if (userServes.AddContact(this._user, contact))
				{
					MessageBox.Show("User successfully added.", Name = "Ok");
					this.NameContact.Clear();
					this.InfaContact.Clear();
				}
			}
			else
			{
				MessageBox.Show("ENTER INF.", Name = "ERROR");
			}
		}
	}
}
