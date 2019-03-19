using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TeacherStudentEditor.Data;

namespace TeacherStudentEditor.Hubs
{
    public class QuestionHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task PostNewQuestion(Guid questionId)
        {
            await Clients.All.SendAsync(nameof(PostNewQuestion));
        }
    }
}
