using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace TeacherStudentEditor.Models
{
    public class CreateCourseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public Guid SelectedCategory { get; set; }
    }
}