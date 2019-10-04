using DesktopContactsApp.Classes;
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

namespace DesktopContactsApp.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        public Contact Contact
        {
            get { return (Contact)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register(
                "Contact",
                typeof(Contact),
                typeof(ContactControl),
                new PropertyMetadata(
                    new Contact()                   // Can be null, too.
                    {
                        Name = "Name",
                        Email = "Email",
                        Phone = "Phone number"
                    },
                    SetText
                    )
                );

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContactControl control)
            {
                Contact contact = e.NewValue as Contact;
                control.idTextBlock.Text = contact.Id.ToString();
                control.nameTextBlock.Text = contact.Name;
                control.emailTextBlock.Text = contact.Email;
                control.phoneTextBlock.Text = contact.Phone;
            }
        }

        public ContactControl()
        {
            InitializeComponent();
        }
    }
}
