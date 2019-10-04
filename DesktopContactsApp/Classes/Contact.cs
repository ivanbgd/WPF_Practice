using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopContactsApp.Classes
{
    // "Contact" class & "Contact" table
    public class Contact
    {
        [PrimaryKey, AutoIncrement] // SQLite attributes
        public int Id { get; set; } // Unique Id (property & column); Primary key in DB

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public override string ToString()
        {
            return $"{Id:d2} {Name}\n     {Email} {Phone}";
        }
    }
}
