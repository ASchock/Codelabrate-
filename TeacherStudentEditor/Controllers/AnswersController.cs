using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeacherStudentEditor.Data;
using TeacherStudentEditor.Models;
using Microsoft.EntityFrameworkCore;

namespace TeacherStudentEditor.Controllers
{
    public class AnswersController : Controller
    {
        private ApplicationDbContext _context;

        public AnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(CreateAnswerViewModel answer)
        {
            if (ModelState.IsValid)
            {
                Question question = await _context.Questions.SingleAsync(x => x.Id == answer.QuestionId);
                question.Answers.Add(new Answer(answer.Text));
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Questions", new { id = answer.QuestionId });
            }

            return View(answer);
        }
    }
}
