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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool viewSortedByName;
        List<Contact> contacts;
        List<Contact> filteredContacts;
        
        public MainWindow()
        {
            InitializeComponent();

            viewSortedByName = false;
            contacts = new List<Contact>();
            filteredContacts = new List<Contact>();

            RefreshContactsView();
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            RefreshContactsViewIfShowDialogHasBeenAccepted();
        }

        private void ViewSortedButton_Click(object sender, RoutedEventArgs e)
        {
            viewSortedByName = !viewSortedByName;

            RefreshContactsView();
            
            if (viewSortedByName)
            {
                ((Button)sender).Content = "View Sorted by Id";
            }
            else
            {
                ((Button)sender).Content = "View Sorted by Name";
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TextBox searchTextBox = (TextBox)sender;  // Or: sender as TextBox; Or: (ContentControl)sender; But, no need for this local variable; this is just for reference, as an example.
            filteredContacts = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList(); // Can stay IEnumerable, no need for List.
            contactsListView.ItemsSource = filteredContacts;
        }

        private void ContactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            Contact selectedContact = (Contact)contactsListView.SelectedItem;
            if (selectedContact != null)
            {
                ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(selectedContact);
                contactDetailsWindow.ShowDialog();

                RefreshContactsViewIfShowDialogHasBeenAccepted();
            }
        }

        private void ReadDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                contacts = connection.Table<Contact>().ToList();
            }
        }

        private void RefreshContactsView()
        {
            ReadDatabase();

            if ((contacts != null) && (filteredContacts != null))
            {
                if (viewSortedByName)
                {
                    contacts = contacts.OrderBy(c => c.Name).ToList();
                    filteredContacts = filteredContacts.OrderBy(c => c.Name).ToList();
                }
                else
                {
                    contacts = contacts.OrderBy(c => c.Id).ToList();
                    filteredContacts = filteredContacts.OrderBy(c => c.Id).ToList();
                }
                contactsListView.ItemsSource = (searchTextBox.Text != "") ? filteredContacts : contacts;
            }
        }

        private void RefreshContactsViewIfShowDialogHasBeenAccepted(bool clearSearchTextBox = true)
        {
            if (App.saveClicked || App.updateOrDeleteClicked)
            {
                App.saveClicked = false;
                App.updateOrDeleteClicked = false;

                RefreshContactsView();          // Populates contacts, with newly added contact.
                                                // No need to populate filteredContacts from database, or to copy contacts to it [by: filteredContacts = contacts.ToList();].

                if (clearSearchTextBox)
                    searchTextBox.Text = "";    // Automatically enters the SearchTextBox_TextChanged() event handler, where it populates filteredContacts with full new contacts list.
                                                // This will automatically display all contacts, including the newly added one.
                                                // In the other case, searchTextBox will be left untact, along with the filteredContacts list, and the last display content
                                                // of this window, i.e., the last content of contactsListView. This means that the filter remains in effect.
            }
        }
    }
}
