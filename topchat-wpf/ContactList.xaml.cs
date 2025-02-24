using Microsoft.EntityFrameworkCore;
using System.Windows;
using TopChat.Models.Entities;
using TopChat.Services;
using TopChat.Services.Interfaces;


namespace topchat_wpf
{
	public partial class ContactList : Window
	{
		private ADatabaseConnection _databaseConnection;

		private User _user;

		private UserContactService _userContactService;

		public ContactList(User user, ADatabaseConnection connection, UserContactService contactService)
		{
			this._databaseConnection = connection;

			this._user = user;

			this._userContactService = contactService;

			InitializeComponent();
		}

		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{
			this.NewNameLogin.Visibility = Visibility.Collapsed;
			this.LoginTabl.Visibility = Visibility.Visible;
			this.IpAdressTabl.Visibility = Visibility.Visible;
			this.InfaContact.Visibility = Visibility.Visible;
			this.NameContact.Visibility = Visibility.Visible;
			this.ButtonEnter.Visibility = Visibility.Visible;
		}

		private void ButtonEnter_Click_1(object sender, RoutedEventArgs e)
		{
			if (this.NameContact.Text != "" || this.InfaContact.Text != "")
			{
				if (this.AddContactRadioButton.IsChecked == true)
				{
					if (this.NameContact.Text != "" || this.InfaContact.Text != "")
					{
						IUserServes userServes = new UserService(this._databaseConnection);

						UserContactService userContact = new UserContactService(this._databaseConnection);

						UserContact contact = new UserContact()
						{
							User = userServes.GetUser(this._user.Login),
							UserName = this.NameContact.Text,
							UserIp = this.InfaContact.Text,
							dateTime = DateTime.Now
						};

						if (userServes.FindUser(userServes.GetUser(this.NameContact.Text)))
						{
							if (this._userContactService.AddContact(contact))
							{
								this.NameContact.Clear();
								this.InfaContact.Clear();

								MessageBox.Show("Contact Added", "OK");
							}
						}
						else
						{
							MessageBox.Show("No such user exists", "ERROR");
						}
					}
				}
				else if (this.DeleteContactRadioButton.IsChecked == true)
				{
					if (this.NameContact.Text != "" || this.InfaContact.Text != "")
					{
						if (this._userContactService.DeleteContact(this.NameContact.Text))
						{
							this.NameContact.Clear();
							this.InfaContact.Clear();

							MessageBox.Show("Contact Delete", "OK");
						}
					}
				}
				else if (this.RenameContactRadioButton.IsChecked == true)
				{
					if (this.NameContact.Text != "" || this.InfaContact.Text != "")
					{
						if (this._userContactService.RenameContact(this.NameContact.Text, this.InfaContact.Text))
						{
							this.NameContact.Clear();
							this.InfaContact.Clear();

							MessageBox.Show("Contact Rename", "OK");
						}
					}
				}
			}
			else
			{
				this.ContactListText.Visibility = Visibility.Collapsed;
				this.ContactListText.Clear();
			}
		}

		private void ContactListButton_Click(object sender, RoutedEventArgs e)
		{

			if (this._databaseConnection.UserContacts.Any())
			{
				this.ContactListText.Visibility = Visibility.Visible;

				foreach (var contact in this._databaseConnection.UserContacts.Include(uc => uc.User))
				{
					if (contact.User.Login == this._user.Login)
					{
						this.ContactListText.Text += $"name - {contact.UserName} ; add date - {contact.dateTime}\n";
					}
				}
			}
			else
			{
				MessageBox.Show("list empty", "ERROR");
			}
		}

		private void DeleteContact_Checked(object sender, RoutedEventArgs e)
		{
			this.LoginTabl.Visibility = Visibility.Visible;
			this.IpAdressTabl.Visibility = Visibility.Collapsed;
			this.NewNameLogin.Visibility = Visibility.Collapsed;
			this.InfaContact.Visibility = Visibility.Collapsed;
			this.NameContact.Visibility = Visibility.Visible;
			this.ButtonEnter.Visibility = Visibility.Visible;
		}

		private void RenameContact_Checked(object sender, RoutedEventArgs e)
		{
			this.LoginTabl.Visibility = Visibility.Visible;
			this.NewNameLogin.Visibility = Visibility.Visible;
			this.IpAdressTabl.Visibility = Visibility.Collapsed;
			this.InfaContact.Visibility = Visibility.Visible;
			this.NameContact.Visibility = Visibility.Visible;
			this.ButtonEnter.Visibility = Visibility.Visible;
		}
	}
}
