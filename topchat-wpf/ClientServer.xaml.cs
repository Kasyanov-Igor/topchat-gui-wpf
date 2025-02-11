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

		private string _userLogin;

		public Client___Server(string userLogin)
		{
			InitializeComponent();

			this._userLogin = userLogin;

			this._providerUdp = new ConnectionProviderUdp();
			this._providerUdp.SetDestination("127.0.0.1", 5000);
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			UdpReceiveResult result = await Task.Run(() => this._providerUdp.ReceiveAsync());

			byte[] receivedBytes = result.Buffer;

			ServerText.Text += this._userLogin + Encoding.UTF8.GetString(receivedBytes);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			this._providerUdp.Send(Encoding.UTF8.GetBytes(ClientText.Text));
		}
	}
}
