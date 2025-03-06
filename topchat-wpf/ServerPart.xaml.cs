using Microsoft.EntityFrameworkCore;
using System.Windows;
using TopChat.Models.Domains;
using TopChat.Models.Entities;
using TopChat.Services;
using TopChat.Services.Interfaces;

namespace topchat_wpf
{
	public partial class ServerPart : Window
	{
		private IMessageService _messageServiceClient;

		private User _user;

		private List<string> _listUserName;

		private ADatabaseConnection _connection;

		public ServerPart(User user, IMessageService messageService, ADatabaseConnection connection)
		{
			InitializeComponent();

			this._messageServiceClient = messageService;

			this._connection = connection;

			this._user = user;

			this._listUserName = new List<string> { };

			foreach (var contact in this._connection.UserContacts.Include(uc => uc.User))
			{
				if (contact.User.Login == this._user.Login)
				{
					this._listUserName.Add(contact.UserName);
				}
			}

			ComboBox.ItemsSource = this._listUserName;
		}

		private async Task StartServer_ClickAsync(object sender, RoutedEventArgs e)
		{
            ConnectionProviderUdp providerUdp = new ConnectionProviderUdp();

            await providerUdp.StartAsync();

        }

		private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{

		}

		private void ViewButton_click(object sender, RoutedEventArgs e)
		{
			IUserServes userServes = new UserService(this._connection);

			List<Message> messages = this._messageServiceClient.GetMessages(userServes.GetUser(ComboBox.Text), SendType.Read);

			this.ServerText.Text += $"Sender - {messages.FirstOrDefault().Sender.Login}\tRecipient - {messages.FirstOrDefault().Recipient}\n";

			foreach (var message in messages)
			{
				this.ServerText.Text += $"{message.MediaData.Text} : {message.DateTime.TimeOfDay}";
			}
		}
	}
}
