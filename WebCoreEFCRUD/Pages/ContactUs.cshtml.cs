using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebCoreEFCRUD.Models;
using WebCoreEFCRUD.Services;

namespace WebCoreEFCRUD.Pages
{
    public class ContactUsModel : PageModel
    {
        [BindProperty]
        public ContactDto Contact { get; set; } = new ContactDto();

        public List<SelectListItem> SubjectList = new List<SelectListItem>() { 
            new SelectListItem { Value="Order Status", Text="Order Status" },
            new SelectListItem { Value="Refund Request", Text="Refund Request" },
            new SelectListItem { Value="Job Application", Text="Job Application" },
            new SelectListItem { Value="Other", Text="Others" },
        };
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        public ContactUsModel(IWebHostEnvironment environment,ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }


        public void OnGet()
        {
        }

        public string successMessage = "";
        public string errorMessage = "";
        public void OnPost()
        {
            if(!ModelState.IsValid)
            {
                errorMessage = "Please fill all the required fields";
                return;
            }

            var contact = new Contact()
            {
                FirstName = Contact.FirstName,
                LastName = Contact.LastName,
                Email = Contact.Email,
                Phone = Contact.Phone ?? "",
                Subject = Contact.Subject,
                Message = Contact.Message,
                CreatedAt = DateTime.Now,
                Attachments = new List<Attachment>(),
            };
            //save attachment, if any
            if(Contact.Attachments != null)
            {
                string path = environment.ContentRootPath + "/Storage/Attachments/";

                var guid = Guid.NewGuid();

                for(int i = 0; i < Contact.Attachments.Count; i++)
                {
                    var file = Contact.Attachments[i];
                    var storageFileName = guid + "-" + i + Path.GetExtension(file.FileName);
                    var fullFilePath = path + storageFileName;
                    using(var stream = System.IO.File.Create(fullFilePath))
                    {
                        file.CopyTo(stream);
                    }

                    var attachment = new Attachment()
                    {
                        OriginalFileName = file.FileName,
                        StorageFileName = storageFileName
                    };

                    contact.Attachments.Add(attachment);
                }


            }

            context.Contacts.Add(contact);
            context.SaveChanges();

            successMessage = "Your message has been received successfully";
            //clear the form..
            Contact.FirstName = "";
            Contact.LastName = "";
            Contact.Email = "";
            Contact.Phone = "";
            Contact.Subject = "";
            Contact.Message = "";

            ModelState.Clear();

        }
    }
}
