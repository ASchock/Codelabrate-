using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherStudentEditor.Models
{
    public class CreateQuestionViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Title { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        [Required]
        public Guid SelectedCategory { get; set; }
    }
}