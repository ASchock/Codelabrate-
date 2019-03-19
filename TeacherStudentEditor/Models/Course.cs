using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherStudentEditor.Models
{
    public class Course : EntityBase
    {
        public Course()
        {
            Students = new List<ApplicationUser>();
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public Category Category { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IList<ApplicationUser> Students { get; set; }
        public IList<Class> Classes { get; set; }
    }
}