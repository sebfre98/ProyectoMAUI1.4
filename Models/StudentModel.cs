using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.Models
{
    public class StudentModel
    {
        [PrimaryKey, AutoIncrement]
        public int StudentID { get; set; }
        [Unique]
        public int Document { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(60), Unique]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Ignore]
        public string FullName => $"{FirstName} {LastName}";
    }
}
