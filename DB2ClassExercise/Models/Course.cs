using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB2ClassExercise.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Course(int Id, string Name, string Description)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
        }
    }
}
