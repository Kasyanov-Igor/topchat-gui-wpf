using System.Windows;

namespace topchat_wpf
{
	public partial class Menu : Window
	{
		public Menu()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Client___Server client___Server = new Client___Server();
			client___Server.Show();
			this.Close();
		}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
