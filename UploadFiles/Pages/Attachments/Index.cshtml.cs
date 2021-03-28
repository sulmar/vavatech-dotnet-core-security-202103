using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadFiles.IServices;

namespace UploadFiles.Pages.Attachments
{
    public class IndexModel : PageModel
    {
        private readonly IAttachmentRepository attachmentRepository;

        public byte[] Attachment { get; set; }

        public IndexModel(IAttachmentRepository attachmentRepository)
        {
            this.attachmentRepository = attachmentRepository;
        }

        public void OnGet()
        {
            Attachment = attachmentRepository.Get();
        }

        public IActionResult OnGetAttachment()
        {
            byte[] attachment = attachmentRepository.Get();

            if (attachment == null)
            {
                return NotFound();
            }

            return new FileContentResult(attachment, "application/pdf");
        }
    }
}
