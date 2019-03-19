using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherStudentEditor.Models
{

    public class EditorSession : EntityBase
    {
        public EditorSession()
        {
            Questions = new List<SessionQuestion>();
        }

        [Required]
        public string Title { get; set; }
        public ApplicationUser Teacher { get; set; }
        public Language Language { get; set; }
        public string Code { get; set; }

        public IList<SessionQuestion> Questions { get; set; }
    }
}