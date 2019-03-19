using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TeacherStudentEditor.Data;
using TeacherStudentEditor.Models;

namespace TeacherStudentEditor.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public QuestionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questions.Select(x => new QuestionSummaryViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                CategoryName = x.Category.Name,
                VoteCount = x.VoteCount,
                AnswerCount = x.Answers.Count(),
                ViewCount = x.ViewCount,
                Asked = x.Asked,
                AskedBy = x.AskedBy.UserName,
                Text = x.Text
            }).ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Question question = await _context.Questions.Include(x => x.Category).Include(x => x.AskedBy).Include(x => x.Answers).SingleOrDefaultAsync(x => x.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            question.ViewCount += 1;
            await _context.SaveChangesAsync();
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            CreateQuestionViewModel viewModel = new CreateQuestionViewModel()
            {
                Categories = _context.Categories.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name })
            };
            return View(viewModel);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateQuestionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Category selectedCategory = await _context.Categories.SingleAsync(x => x.Id == viewModel.SelectedCategory);
                var question = new Question(viewModel.Title, viewModel.Text, selectedCategory);
                question.AskedBy = await _userManager.GetUserAsync(User);
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Questions/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Question question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            CreateQuestionViewModel viewModel = new CreateQuestionViewModel()
            {
                Categories = _context.Categories.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name })
            };
            Mapper.Map(question, viewModel);
            return View(viewModel);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, CreateQuestionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var question = await _context.Questions.FindAsync(id);
                Mapper.Map(viewModel, question);
                _context.Update(question);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Questions/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Question question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Question question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
