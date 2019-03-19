using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeacherStudentEditor.Data;
using TeacherStudentEditor.Models;

namespace TeacherStudentEditor.Controllers
{
    [Authorize]
    public class EditorSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditorSessionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EditorSessions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sessions.Include(x => x.Teacher).ToListAsync());
        }

        // GET: EditorSessions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorSession = await _context.Sessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editorSession == null)
            {
                return NotFound();
            }

            return View(editorSession);
        }

        // GET: EditorSessions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EditorSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EditorSession editorSession)
        {
            if (ModelState.IsValid)
            {
                //editorSession.Id = Guid.NewGuid();
                editorSession.Teacher = await _userManager.GetUserAsync(User);
                _context.Add(editorSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editorSession);
        }

        // GET: EditorSessions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorSession = await _context.Sessions.FindAsync(id);
            if (editorSession == null)
            {
                return NotFound();
            }
            return View(editorSession);
        }

        // POST: EditorSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Teacher,Subject,Language,Started,Ended")] EditorSession editorSession)
        {
            if (id != editorSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editorSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorSessionExists(editorSession.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(editorSession);
        }

        // GET: EditorSessions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorSession = await _context.Sessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (editorSession == null)
            {
                return NotFound();
            }

            return View(editorSession);
        }

        // POST: EditorSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var editorSession = await _context.Sessions.FindAsync(id);
            _context.Sessions.Remove(editorSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditorSessionExists(Guid id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }

        public async Task<IActionResult> JoinAsTeacher(Guid id)
        {
            var editorSession = await _context.Sessions.Include(x => x.Teacher)
                .Include(x => x.Questions)
                    .ThenInclude(x => x.Comments)
                        .ThenInclude(x => x.User)
                .Include(x => x.Questions)
                    .ThenInclude(x => x.AskedBy)
                .SingleAsync(x => x.Id == id);
            return View("Editor", editorSession);
        }

        public async Task<IActionResult> JoinAsStudent(Guid id)
        {
            var editorSession = await _context.Sessions.Include(x => x.Teacher)
                .Include(x => x.Questions)
                    .ThenInclude(x => x.Comments)
                        .ThenInclude(x => x.User)
                .Include(x => x.Questions)
                    .ThenInclude(x => x.AskedBy)
                .SingleAsync(x => x.Id == id);
            editorSession.Questions = editorSession.Questions.Where(x => x.AskedBy.UserName == HttpContext.User.Identity.Name).ToList();
            return View("Editor", editorSession);
        }

        //public async Task<IActionResult> Questions(Guid sessionId)
        //{
        //    var editorSession = await _context.Sessions.Include(x => x.Questions).SingleAsync(x => x.Id == sessionId);
        //    foreach (SessionQuestion editorSessionQuestion in editorSession.Questions)
        //    {
        //        editorSessionQuestion.Comments.Clear();
        //    }

        //    return Ok(editorSession.Questions);
        //}
    }
}
