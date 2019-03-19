using Microsoft.AspNetCore.Http;

namespace TeacherStudentEditor.Models
{
    public static class EditorSessionExtensions
    {
        public static string SignalRTeacherGroupName(this EditorSession session)
        {
            return $"{session.Id}_Teachers";
        }

        public static string SignalRStudentGroupName(this EditorSession session)
        {
            return $"{session.Id}_Students";
        }
    }
}