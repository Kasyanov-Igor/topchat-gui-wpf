using System.Windows;
using TopChat.Models.Entities;
using TopChat.Services;
using TopChat.Services.Interfaces;

namespace topchat_wpf
{
	public partial class Menu : Window
	{
		private User _user;

		private ADatabaseConnection _connection;

		private IConnectionProvider _connectionProvider = new ConnectionProviderUdp();

		private IDataConverterService _dataConverterService = new DataConverterService();

		public Menu(User user, ADatabaseConnection aDatabaseConnection, IConnectionProvider connectionProvider, IDataConverterService dataConverterService)
		{
			InitializeComponent();

			this._user = user;
			this._connectionProvider = connectionProvider;
			this._connection = aDatabaseConnection;
			this._dataConverterService = dataConverterService;
		}

		private void ClientServerButton_Click(object sender, RoutedEventArgs e)
		{
			Client___Server client___Server = new Client___Server(this._connectionProvider,
				new MessageServiceClient(_dataConverterService, new NetworkDataService(this._connectionProvider)),
				new DataBaseService(_dataConverterService, this._connection));

			client___Server.Show();
			this.Close();
		}

		private void Client_Button_Click_1(object sender, RoutedEventArgs e)
		{
			ClientPart client = new ClientPart(this._user, this._connectionProvider,
				new MessageServiceClient(_dataConverterService, new NetworkDataService(this._connectionProvider)),
				new DataBaseService(_dataConverterService, this._connection), this._connection);

			client.Show();
			this.Close();
		}

		private void ContactButton_Click_1(object sender, RoutedEventArgs e)
		{
			ContactList contact = new ContactList(this._user, _connection, new UserContactService(this._connection));
			contact.Show();
			this.Close();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ServerPart server = new ServerPart(this._user, new MessageServiceClient(_dataConverterService, new NetworkDataService(this._connectionProvider)),
				this._connection);

			server.Show();
			this.Close();
		}
	}
}
