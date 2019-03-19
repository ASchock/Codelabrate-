using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherStudentEditor.Models
{
    public class SessionQuestion : EntityBase
    {
        public SessionQuestion()
        {
            Comments = new List<SessionQuestionComment>();
        }

        public int StartLineNumber { get; set; }
        public int StartColumn { get; set; }
        public int EndLineNumber { get; set; }
        public int EndColumn { get; set; }
        [Required]
        public ApplicationUser AskedBy { get; set; }
        public DateTime Asked { get; set; }

        public IList<SessionQuestionComment> Comments { get; set; }
    }
}