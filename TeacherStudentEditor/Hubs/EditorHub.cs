using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TeacherStudentEditor.Data;
using TeacherStudentEditor.Models;

namespace TeacherStudentEditor.Hubs
{
    public class EditorHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditorHub(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            string sessionId = Context.GetHttpContext().Request.Query["sessionid"];
            EditorSession editorSession = await _dbContext.Sessions.Include(x => x.Teacher).SingleOrDefaultAsync(x => x.Id == Guid.Parse(sessionId)); // SingleOrDefault(x => x.Id == Guid.Parse(sessionId));
            if (Context.User.Identity.Name != editorSession.Teacher.UserName)
                await Groups.AddToGroupAsync(Context.ConnectionId, editorSession.SignalRStudentGroupName());
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, editorSession.SignalRTeacherGroupName());
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SaveCode(string code)
        {
            string sessionId = Context.GetHttpContext().Request.Query["sessionid"];
            EditorSession editorSession = await _dbContext.Sessions.FindAsync(Guid.Parse(sessionId));
            editorSession.Code = code;
            await _dbContext.SaveChangesAsync();
        }

        public async Task PostChanges(object changes)
        {
            string sessionId = Context.GetHttpContext().Request.Query["sessionid"];
            await Clients.Group($"{sessionId}_Students").SendCoreAsync("ReceiveChanges", new object[]{changes});
        }

        public async Task PostComment(string questionId, JObject range, string comment)
        {
            string sessionId = Context.GetHttpContext().Request.Query["sessionid"];
            SessionQuestion sessionQuestion;
            if (string.IsNullOrWhiteSpace(questionId))
            {
                sessionQuestion = new SessionQuestion
                {
                    StartLineNumber = range.Value<int>("startLineNumber"),
                    StartColumn = range.Value<int>("startColumn"),
                    EndLineNumber = range.Value<int>("endLineNumber"),
                    EndColumn = range.Value<int>("endColumn"),
                    AskedBy = await _userManager.GetUserAsync(Context.User)
                };
                EditorSession editorSession = await _dbContext.Sessions.FindAsync(Guid.Parse(sessionId));
                editorSession.Questions.Add(sessionQuestion);
            }
            else
            {
                sessionQuestion = await _dbContext.SessionQuestions.FindAsync(Guid.Parse(questionId));
            }

            SessionQuestionComment sessionQuestionComment = new SessionQuestionComment
            {
                Text = comment,
                User = await _userManager.GetUserAsync(Context.User)
            };
            sessionQuestion.Comments.Add(sessionQuestionComment);

            _dbContext.SaveChanges();

            if (string.IsNullOrWhiteSpace(questionId))
            {
                await Clients.Group($"{sessionId}_Teachers").SendCoreAsync("QuestionPosted", new object[] { sessionQuestion });
                await Clients.Client(Context.ConnectionId).SendCoreAsync("QuestionPosted", new object[] { sessionQuestion });
            }
            else
            {
                await Clients.All.SendCoreAsync("CommentPosted", new object[] { sessionQuestion.Id, sessionQuestionComment });
            }
        }

        public async Task ClearQuery()
        {

        }
    }
}
