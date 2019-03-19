using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeacherStudentEditor.Areas.Admin
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string Categories => "Categories";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string CategoriesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Categories);
   
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
