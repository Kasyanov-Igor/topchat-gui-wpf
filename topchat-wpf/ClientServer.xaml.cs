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

		public Client___Server()
		{
			InitializeComponent();

			this._providerUdp = new ConnectionProviderUdp();

			this._messageServiceClient = new MessageServiceClient(new DataConverterService(), new NetworkDataService(this._providerUdp));
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			UdpClient u = new UdpClient(5000);

			UdpReceiveResult receivedResults = u.ReceiveAsync().Result;
			byte[] receivedBytes = receivedResults.Buffer;
			IPEndPoint remoteEndPoint = receivedResults.RemoteEndPoint;

			ServerText.Text += $"Получено от {remoteEndPoint.Address}: {Encoding.UTF8.GetString(receivedBytes)}";
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
	}
}
