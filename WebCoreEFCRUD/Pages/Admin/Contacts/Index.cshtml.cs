using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebCoreEFCRUD.Models;
using WebCoreEFCRUD.Services;

namespace WebCoreEFCRUD.Pages.Admin.Contacts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public List<Contact> ContactList { get; set; } = new List<Contact>();

        //Pagination Functionality
        public int pageIndex = 1;
        public int totalPage = 0;
        public readonly int pageSize = 3;


        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet(int? PIndex)
        {
            IQueryable<Contact> query = context.Contacts
               .OrderByDescending(c => c.Id)
               .Include(c => c.Attachments);
            
            //Pagination functionality
            if(PIndex == null || PIndex < 1)
            {
                PIndex = 1;
            }

            this.pageIndex = (int)PIndex;

            decimal count = query.Count();
            totalPage = (int)Math.Ceiling(count / pageSize);

            query = query.Skip((this.pageIndex - 1) * pageSize).Take(pageSize);
            this.ContactList = query.ToList();
        }
    }
}
