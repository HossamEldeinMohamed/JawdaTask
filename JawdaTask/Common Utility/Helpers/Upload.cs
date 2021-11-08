using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JawdaTask.Common_Utility.Helpers
{
    public static class Upload
    {
        public static string UploadedFile(IFormFile file ,string wwwrootPath)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                //string uploadsFolder = Path.Combine(IWebHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" +file.FileName;
                string filePath = Path.Combine(wwwrootPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
