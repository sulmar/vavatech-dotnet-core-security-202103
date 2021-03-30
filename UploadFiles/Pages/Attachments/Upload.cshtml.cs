using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadFiles.IServices;

namespace UploadFiles.Pages.Attachments
{
    [RequestSizeLimit(100)]
    public class UploadModel : PageModel
    {
        private readonly IAttachmentRepository attachmentRepository;

        public UploadModel(IAttachmentRepository attachmentRepository)
        {
            this.attachmentRepository = attachmentRepository;
        }

        [BindProperty]
        public IFormFile FormFile { get; set; }

        public void OnGet()
        {

        }

        
        public async Task<IActionResult> OnPost()
        {

            if (FormFile.ContentType == "application/pdf")
            {
                
            }

            byte[] attachment = await FormFile.GetBytes();

            attachmentRepository.Add(attachment);

            return RedirectToPage("/Attachments/Index");
        }
    }

    public static class FormFileExtensions
    {
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
