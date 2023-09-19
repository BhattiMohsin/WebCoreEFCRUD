using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebCoreEFCRUD.Models;
using WebCoreEFCRUD.Services;

namespace WebCoreEFCRUD.Pages.Admin.Contacts
{
    public class DetailsModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        public Contact ContactProperty { get; set; } = new Contact();

        public DetailsModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Contacts/Index");
                return;
            }

            var contact = context.Contacts.Include(c => c.Attachments).FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                Response.Redirect("/Admin/Contacts/Index");
                return;
            }

            ContactProperty = contact;
        }


        public ActionResult OnGetAttachment(int? attachmentId)
        {
            if (attachmentId == null)
            {
                return NotFound();
            }

            var attachment = context.Attachments.Find(attachmentId);
            if (attachment == null)
            {
                return NotFound();
            }

            var path = environment.ContentRootPath + "/Storage/Attachments/";
            var fullFilePath = path + attachment.StorageFileName;
            if (System.IO.File.Exists(fullFilePath))
            {
                //Read the File data into Byte Array.  
                byte[] bytes = System.IO.File.ReadAllBytes(fullFilePath);

                //Send the File to Download.  
                return File(bytes, "application/octet-stream", attachment.OriginalFileName);
            }

            return NotFound();
        }
    }
}
