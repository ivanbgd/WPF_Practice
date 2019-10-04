using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string databaseName = "Contacts.db";
        //static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //static string folderPath = System.IO.Directory.GetCurrentDirectory();
        static string folderPath = "..\\..\\";  // "d:\\OneDrive\\Projects\\Visual Studio\\Udemy WPF Masterclass\\DesktopContactsApp\\DesktopContactsApp\\";
        public static string databasePath = System.IO.Path.Combine(folderPath, databaseName);

        public static bool saveClicked = false; // Has saveButton in NewContactWindow.xaml been clicked (true) or we have canceled the dialog (false).
        public static bool updateOrDeleteClicked = false; // Has updateButton or deleteButton in ContactDetailsWindow.xaml been clicked (true) or we have canceled the dialog (false).
    }
}
