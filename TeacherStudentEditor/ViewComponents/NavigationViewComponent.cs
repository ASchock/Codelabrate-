using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TeacherStudentEditor.Data;

namespace TeacherStudentEditor.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        private ApplicationDbContext _applicationDbContext;

        public NavigationViewComponent(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _applicationDbContext.Categories.ToListAsync());
        }
    }
}
