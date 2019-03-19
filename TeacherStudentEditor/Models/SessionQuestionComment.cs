using System;
using System.ComponentModel.DataAnnotations;

namespace TeacherStudentEditor.Models
{
    public class SessionQuestionComment : EntityBase
    {
        public SessionQuestionComment()
        {
            Posted = DateTime.Now;
        }

        public DateTime Posted { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public string Text { get; set; }
    }
}