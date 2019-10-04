using DesktopContactsApp.Classes;
using SQLite;
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

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact()
            {
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                Phone = phoneNumberTextBox.Text
            };

            // Closes the connection automatically, thanks to SQLiteConnection class implementing the IDisposable interface,
            // which has Dispose() method that will be called when we exit the scope, and which in turn calls the Close()
            // method of the connection object. This is the "using" statement, not directive.
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Insert(contact);
            }
            
            App.saveClicked = true;

            Close();    // this.Close() - closes this window
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if ((textBox.Text == "Name") || (textBox.Text == "Email") || (textBox.Text == "Phone number"))
            {
                textBox.Text = "";
                textBox.FontStyle = new FontStyle();
            }
        }
    }
}
