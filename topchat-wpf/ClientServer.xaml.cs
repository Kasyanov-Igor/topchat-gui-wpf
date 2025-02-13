using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using TopChat.Services;
using TopChat.Services.Interfaces;

namespace topchat_wpf
{
	public partial class Client___Server : Window
	{
		private IConnectionProvider _providerUdp;

		public Client___Server()
		{
			InitializeComponent();

			this._providerUdp = new ConnectionProviderUdp();
			this._providerUdp.SetDestination("127.0.0.1", 5000);
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
			this._providerUdp.Send(Encoding.UTF8.GetBytes(ClientText.Text));
			ClientText.Clear();
		}
	}
}
