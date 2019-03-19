using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherStudentEditor.Data;

namespace TeacherStudentEditor.Areas.Admin.Controllers
{
    [Authorize(Roles = ApplicationDbContext.AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
