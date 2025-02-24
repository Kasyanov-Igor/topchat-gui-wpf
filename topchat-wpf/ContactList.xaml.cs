﻿using System.Windows;
using TopChat.Models.Domains;
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

		public ContactList(User user)
		{
			this._databaseConnection = new SqliteConnection();
			this._user = user;
			this._userContactService = new UserContactService(this._databaseConnection);

			InitializeComponent();
		}

		private void DeleteContact_Click(object sender, RoutedEventArgs e)
		{
            this.LoginTabl.Visibility = Visibility.Visible;
            this.IpAdressTabl.Visibility = Visibility.Visible;
			this.ButtonEnterDelete.Visibility = Visibility.Visible;

        }

		private void RenameContact_Click(object sender, RoutedEventArgs e)
		{

			if (this.NameContact.Text != null || this.InfaContact.Text != null)
			{
				UserContact contact = new UserContact()
				{
					UserName = this.NameContact.Text,
					UserIp = this.InfaContact.Text,
				};
			}
			else
			{
				MessageBox.Show("ENTER INF.", Name = "ERROR");
			}
		}

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
			if (this.NameContact.Text != "" || this.InfaContact.Text != "")
			{
				IUserServes userServes = new UserService(this._databaseConnection);

				UserContact contact = new UserContact()
				{
					user = userServes.GetUser(this._user.Login),
					UserName = this.NameContact.Text,
					UserIp = this.InfaContact.Text,
					dateTime = DateTime.Now
				};

				if (this._userContactService.AddContact(contact))
				{
					this.NameContact.Clear();
					this.InfaContact.Clear();
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

				foreach (var contact in this._databaseConnection.UserContacts)
				{
					if (contact.user.Login == this._user.Login)
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

        private void ButtonEnterDelete_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.NameContact.Text != "" || this.InfaContact.Text != "")
            {
                IUserServes userServes = new UserService(this._databaseConnection);

                UserContact contact = new UserContact()
                {
                    user = userServes.GetUser(this._user.Login),
                    UserName = this.NameContact.Text,
                    UserIp = this.InfaContact.Text,
                    dateTime = DateTime.Now
                };

                if (this._userContactService.AddContact(contact))
                {
                    this.NameContact.Clear();
                    this.InfaContact.Clear();
                }
            }
            else
            {
                this.ContactListText.Visibility = Visibility.Collapsed;
                this.ContactListText.Clear();
            }
        }
    }
}
