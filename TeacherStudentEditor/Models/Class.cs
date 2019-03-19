using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherStudentEditor.Models
{
    public class Class : EntityBase
    {
        public Class()
        {
            //Files = new List<EditorFile>();
        }

        [Required]
        public string Title { get; set; }
        
        public int Difficulty { get; set; }
        public ApplicationUser Teacher { get; set; }

        //public IList<EditorFile> Files { get; set; }
    }
}