using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherStudentEditor.Models
{
    public class Question : EntityBase
    {
        protected Question()
        {
            Asked = DateTime.Now;
            Answers = new List<Answer>();
        }

        public Question(string title, string text, Category category) : this()
        {
            Title = title;
            Text = text;
            Category = category;
        }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public virtual Category Category { get; set; }
        public DateTime Asked { get; protected set; }
        [Required]
        public ApplicationUser AskedBy { get; set; }
        public int ViewCount { get; set; }
        public int VoteCount { get; set; }

        public virtual IList<Answer> Answers { get; protected set; }
    }
}