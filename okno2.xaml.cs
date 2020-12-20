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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for okno2.xaml
    /// </summary>
    public partial class okno2 : Window
    {
        public SqlConnection thisConnection;
        public SqlDataReader myReader;
        public SqlCommand myCommand;
        public DataObject objdata=null;
        public int sizeList = 0;
        System.Data.SqlClient.SqlCommand cmd;
        public class DataObject
        {
            public string id { get; set; }
            public string imie { get; set; }
            public string nazwisko { get; set; }
            public string pensja { get; set; }
        }
        public okno2(SqlConnection thisConnect)
        {
            thisConnection = thisConnect;
            InitializeComponent();
            myCommand = new SqlCommand("select * from identyfikacja;", thisConnection);
            var list = new ObservableCollection<DataObject>();
            myReader = myCommand.ExecuteReader();
            sizeList = 0;
            while (myReader.Read())
            {
                objdata = new DataObject();
                objdata.id = myReader["id"].ToString();
                objdata.imie = myReader["imie"].ToString();
                objdata.nazwisko = myReader["nazwisko"].ToString();
                objdata.pensja = myReader["pensja"].ToString();
                list.Add(objdata);
                sizeList++;
            }
            this.tabela1.ItemsSource = list;
            myReader.Close();

        }
        private void Wyswietl_baze_Click(object sender, RoutedEventArgs e)
        {
            var list = new ObservableCollection<DataObject>();
            myReader = myCommand.ExecuteReader();
            sizeList = 0;
            while (myReader.Read())
            {
                objdata = new DataObject();
                objdata.id = myReader["id"].ToString();
                objdata.imie = myReader["imie"].ToString();
                objdata.nazwisko = myReader["nazwisko"].ToString();
                objdata.pensja = myReader["pensja"].ToString();
                list.Add(objdata);
                sizeList++;
            }
            this.tabela1.ItemsSource = list;
            myReader.Close();
        }

        private void dodaj_Click(object sender, RoutedEventArgs e)
        {
            if(cmd!=null)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO identyfikacja(id, imie, nazwisko, pensja)"+ "Values('" + (sizeList+1).ToString() + "', '" + objdata.imie + "', '" + objdata.nazwisko + "', '" + objdata.pensja + "')";
                cmd.Connection = thisConnection;
            }
            else
            {
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO identyfikacja(id, imie, nazwisko, pensja)" + "Values('" + (sizeList + 1).ToString() + "', '" + objdata.imie + "', '" + objdata.nazwisko + "', '" + objdata.pensja + "')";
                cmd.Connection = thisConnection;
            }
            //thisConnection.Open();
            cmd.ExecuteNonQuery();
            //thisConnection.Close();
        }
        private void usun_Click(object sender, RoutedEventArgs e)
        {
            if (cmd!=null)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "delete from Testowa.dbo.identyfikacja where imie="+"'"+objdata.imie+"'";
                cmd.Connection = thisConnection;
            }
            else
            {
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "delete from Testowa.dbo.identyfikacja where imie="+"'"+objdata.imie+"'";
                cmd.Connection = thisConnection;
            }
            cmd.ExecuteNonQuery();
        }

        private void idSet_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= idSet_GotFocus;
        }
        private void wyszukaj_Click(object sender, RoutedEventArgs e)
        {
            myReader = myCommand.ExecuteReader();
            int sizeListImie = 0;
            int sizeListNazwisko = 0;
            bool bImie = false;
            bool bNazwisko = false;
            while (myReader.Read())
            {
                // MessageBox.Show(myReader["imie"].ToString());
                char[] t1 = myReader["imie"].ToString().ToCharArray();
                char[] t2 = objdata.imie.ToCharArray();
                char[] t3 = myReader["nazwisko"].ToString().ToCharArray();
                char[] t4 = objdata.nazwisko.ToCharArray();
                for (int i = 0; i < t1.Length; i++)
                {
                    if (t1[i] != ' ')
                        sizeListImie++;
                }
                for (int i = 0; i < t3.Length; i++)
                {
                    if (t3[i] != ' ')
                        sizeListNazwisko++;
                }
                if (sizeListImie == t2.Length)
                {
                    int count1 = 0;
                    for (int i = 0; i < sizeListImie; i++)
                    {
                        if (t1[i] == t2[i])
                            count1++;
                    }
                    if (count1 == sizeListImie)
                        bImie = true;
                }
                if (sizeListNazwisko == t4.Length)
                {
                    int count2 = 0;
                    for (int i = 0; i < sizeListNazwisko; i++)
                    {
                        if (t3[i] == t4[i])
                            count2++;
                    }
                    if (count2 == sizeListNazwisko)
                        bNazwisko = true;
                }
                sizeListImie = 0;
                sizeListNazwisko = 0;
            }
            if (bImie && bNazwisko)
                MessageBox.Show("Wyszukiwany uzytkowanik jest w bazie");
            else
                MessageBox.Show("Wyszukiwany uzytkowanik nie istnieje");
            myReader.Close();
        }

        private void imie_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= imie_GotFocus;
        }

        private void nazwisko_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= nazwisko_GotFocus;
        }

        private void pensja_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= pensja_GotFocus;
        }

        private void idSet_TextChanged(object sender, TextChangedEventArgs e)
        {
            idSet.Text = (sizeList + 1).ToString();
            if (objdata==null)
            {
                DataObject objdata = new DataObject();
                objdata.id = (sender as TextBox).Text;
            }
            else
                objdata.id = (sender as TextBox).Text;
        }

        private void imie_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (objdata == null)
            {
                DataObject objdata = new DataObject();
                objdata.imie = (sender as TextBox).Text;
            }
            else
                objdata.imie = (sender as TextBox).Text;
        }

        private void nazwisko_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (objdata == null)
            {
                DataObject objdata = new DataObject();
                objdata.nazwisko = (sender as TextBox).Text;
            }
            else
                objdata.nazwisko = (sender as TextBox).Text;
        }

        private void pensja_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (objdata == null)
            {
                DataObject objdata = new DataObject();
                objdata.pensja = (sender as TextBox).Text;
            }
            else
                objdata.pensja = (sender as TextBox).Text;
        }

        private void usun1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (objdata == null)
            {
                DataObject objdata = new DataObject();
                objdata.imie = (sender as TextBox).Text;
            }
            else
                objdata.imie = (sender as TextBox).Text;
        }

        private void usun1_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= usun1_GotFocus;
        }

        private void wyszukajInput_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= wyszukajInput_GotFocus;
        }

        private void wyszukajInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (objdata == null)
            {
                DataObject objdata = new DataObject();
                objdata.imie = (sender as TextBox).Text;
            }
            else
                objdata.imie = (sender as TextBox).Text;
        }

        private void wyszukaj2Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (objdata == null)
            {
                DataObject objdata = new DataObject();
                objdata.nazwisko = (sender as TextBox).Text;
            }
            else
                objdata.nazwisko = (sender as TextBox).Text;
        }

        private void wyszukaj2Input_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= wyszukaj2Input_GotFocus;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void usun1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string text = "";
            usun1.Text = text;
        }
    }
}
