using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Controllers.Common
{
    public class FileDetail
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [ApiController]
    [Route("upLoadMedia")]
    public class UploadMedia : ControllerBase
    {
        private readonly IConfiguration _config;

        public UploadMedia(IConfiguration config)
        {
            _config = config;
        }

        [Route("uploadImage")]
        [HttpPost]
        public async Task<JsonResult> UploadImage (List<IFormFile> files)
        {
            if (files != null && files.Count() > 0)
            {
                long size = files.Sum(f => f.Length);
                List<FileDetail> filePaths = new List<FileDetail>();
                int i = 0;
                string url = _config.GetSection("config")["Url"];
                string uploadFolder = "";
                uploadFolder = _config.GetSection("config")["Directory"] + "\\mybond";
                foreach (var formFile in files)
                {
                    i += 1;
                    if (formFile.Length > 0)
                    {
                        try
                        {
                            //name folder
                            if (!Directory.Exists(uploadFolder))
                            {
                                Directory.CreateDirectory(uploadFolder);
                            }

                            //name file
                            //var uniqueFileName = Strings.RemoveDiacriticUrls(Path.GetFileNameWithoutExtension(formFile.FileName).Replace(" ", "")) + "_" + DateTime.Now.ToString("ssmmhhddMMyyyy") + i.ToString() + Path.GetExtension(formFile.FileName);
                            var uniqueFileName = "FileImg" + "_" + DateTime.Now.ToString("ssmmhhddMMyyyy") + i.ToString() + Path.GetExtension(formFile.FileName);
                            //path
                            var filePath = Path.Combine(uploadFolder, uniqueFileName);
                            if (!String.IsNullOrEmpty("mybond"))
                            {
                                var x = new FileDetail();
                                x.name = formFile.FileName;
                                x.url = url + "mybond" + "/" + uniqueFileName;
                                filePaths.Add(x);
                            }
                            else
                            {
                                var x = new FileDetail();
                                x.name = formFile.FileName;
                                x.url = url + uniqueFileName;
                                filePaths.Add(x);
                            }

                            using (var stream = System.IO.File.Create(filePath))
                            {
                                await formFile.CopyToAsync(stream);
                            }
                        }
                        catch (Exception ex)
                        {
                            return new JsonResult(new { status = 400, code = "ERROR", data = ex.Message });
                        }

                    }
                }
                return new JsonResult(new { status = 200, code = "OK", data = filePaths });
            }
            else
            {
                return new JsonResult(new { status = 400, code = "EMPTY_FILE" });
            }
        }
    }
}
