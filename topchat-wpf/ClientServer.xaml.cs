using Microsoft.Win32;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using TopChat.Models.Entities;
using TopChat.Services.Interfaces;

namespace topchat_wpf
{
	public partial class Client___Server : Window
	{
		private IConnectionProvider _providerUdp;

		private IMessageService _messageServiceClient;

		private IDataBaseService _dataBaseService;

		public Client___Server(IConnectionProvider connectionProvider, IMessageService messageService, IDataBaseService baseService)
		{
			InitializeComponent();

			this._dataBaseService = baseService;

			this._providerUdp = connectionProvider;

			this._messageServiceClient = messageService;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			UdpClient u = new UdpClient(5000);

			UdpReceiveResult receivedResults = u.ReceiveAsync().Result;
			byte[] receivedBytes = receivedResults.Buffer;

			this.ServerText.Text += Encoding.UTF8.GetString(receivedBytes);

			//this._dataBaseService.AddMessage(receivedBytes);

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
