using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Windows;
using TopChat.Models.Domains;
using TopChat.Models.Entities;
using TopChat.Services;
using TopChat.Services.Interfaces;

namespace topchat_wpf
{
	public partial class ClientPart : Window
	{
		private IConnectionProvider _providerUdp;

		private IMessageService _messageServiceClient;

		private IDataBaseService _dataBaseService;

		private ADatabaseConnection _connection;

		private List<string> _listUserName;

		private User _user;

		public ClientPart(User user, IConnectionProvider connectionProvider, IMessageService messageService, IDataBaseService baseService, ADatabaseConnection databaseConnection)
		{
			InitializeComponent();

			this._connection = databaseConnection;

			this._dataBaseService = baseService;

			this._providerUdp = connectionProvider;

			this._messageServiceClient = messageService;

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

		private void ButtonSend_Click(object sender, RoutedEventArgs e)
		{
			IUserServes userServes = new UserService(this._connection);

			var b = userServes.GetUser(ComboBox.Text);

			Message textUser = new Message()
			{
				DateTime = DateTime.Now,
				MediaData = new Media() { Text = ClientText.Text },
				Sender = userServes.GetUser(this._user.Login),
				Recipient = userServes.GetUser(ComboBox.Text)
			};

			this._messageServiceClient.AddMessage(textUser, SendType.Create);
			ClientText.Clear();
		}

		private void ButtonOpenFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			if (openFileDialog.ShowDialog() == true)
			{
				string filename = openFileDialog.FileName;

				MessageBox.Show("Выбран файл: " + filename);

				Message fileUser = new Message()
				{
					DateTime = DateTime.Now,
					MediaData = new Media() { PathToFile = filename }
				};

				this._messageServiceClient.AddMessage(fileUser, SendType.Create);
			}
		}

		private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{

		}
	}
}
