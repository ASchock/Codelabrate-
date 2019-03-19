using System;

namespace TeacherStudentEditor.Models
{
    public class CreateAnswerViewModel
    {
        public string Text { get; set; }
        public Guid QuestionId { get; set; }
    }
}