using Microsoft.Win32;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using TopChat.Models;
using TopChat.Services;
using TopChat.Services.Interfaces;

namespace topchat_wpf
{
	public partial class Client___Server : Window
	{
		private IConnectionProvider _providerUdp;

		private IMessageService _messageServiceClient;

		private IDataBaseService _dataBaseService;

		public Client___Server()
		{
			InitializeComponent();

			this._dataBaseService = new DataBaseService(new DataConverterService(), new SqliteConnection());

			this._providerUdp = new ConnectionProviderUdp();

			this._messageServiceClient = new MessageServiceClient(new DataConverterService(), new NetworkDataService(this._providerUdp));
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			UdpClient u = new UdpClient(5000);

			UdpReceiveResult receivedResults = u.ReceiveAsync().Result;
			byte[] receivedBytes = receivedResults.Buffer;

			this._dataBaseService.AddMessage(receivedBytes);

			//this._messageServiceClient.GetMessages();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Message textUser = new Message()
			{
				DateTime = DateTime.Now,
				MediaData = new Media() { Text = ClientText.Text }
			};

			this._messageServiceClient.AddMessage(textUser);
			ClientText.Clear();
		}

		private void OpenFile_Click(object sender, RoutedEventArgs e)
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

				this._messageServiceClient.AddMessage(fileUser);
			}
		}
	}
}
