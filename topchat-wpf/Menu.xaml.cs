using System.Windows;

namespace topchat_wpf
{
	public partial class Menu : Window
	{
		private string _userLogin;
		public Menu(string userLogin)
		{
			InitializeComponent();
			this._userLogin = userLogin;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Client___Server client___Server = new Client___Server(this._userLogin);
			client___Server.Show();
			this.Close();
		}
	}
}
