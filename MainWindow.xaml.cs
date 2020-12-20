using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string usernamex;
        private string pasworddx;
        private readonly string paswordd = "admin";
        private readonly string usernamee = "admin";
        public MainWindow()
        {
            InitializeComponent();
        }
        public okno2 drugie; // okno glowne po weryfikacji logowania
        private void Logowanie_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection thisConnection;
            if (pasworddx==paswordd && usernamex==usernamee)
            {
                MessageBox.Show("login and password is correct");
                thisConnection = new SqlConnection(@"Server=(local);Database=Testowa;Trusted_Connection=Yes;");
                thisConnection.Open();
                drugie = new okno2(thisConnection);
                drugie.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("login or password is incorect");
            }
        }


        private void pasword_TextChanged(object sender, TextChangedEventArgs e)
        {
            pasworddx = (sender as TextBox).Text.Trim();
        }

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {
            usernamex = (sender as TextBox).Text.Trim();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void Zamknij_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (drugie != null)
                drugie.Close();

        }
    }
}
