using System;

namespace TeacherStudentEditor.Models
{
    public class QuestionSummaryViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public DateTime Asked { get; set; }
        public string AskedBy { get; set; }
        public string Text { get; set; }
        public int ViewCount { get; set; }
        public int AnswerCount { get; set; }
        public int VoteCount { get; set; }
    }
}