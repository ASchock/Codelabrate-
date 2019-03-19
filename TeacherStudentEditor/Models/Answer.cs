using System;
using System.ComponentModel.DataAnnotations;

namespace TeacherStudentEditor.Models
{
    public class Answer : EntityBase
    {
        public Answer()
        {
            Answered = DateTime.Now;
        }
        public Answer(string text) : this()
        {
            Text = text;
        }

        
        public ApplicationUser AnsweredBy { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Answered { get; set; }
    }
}