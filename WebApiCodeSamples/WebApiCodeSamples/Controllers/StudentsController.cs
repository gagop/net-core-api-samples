using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiSendFileTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        /// <summary>
        /// Example showing accepting the file using form-data
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableRequestSizeLimit]
        //[RequestSizeLimit(1024)]
        public async Task<IActionResult> GetFile([FromForm] IFormFile file)
        {
            using (BinaryReader binReader = new BinaryReader(file.OpenReadStream()))
            using (FileStream fs = new FileStream("file", FileMode.OpenOrCreate))
            {
                byte[] buf = new byte[4096];
                while (binReader.Read(buf, 0, buf.Length) > 0)
                {
                    await fs.WriteAsync(buf);
                }
            }

            return Ok();
        }

        /// <summary>
        /// Example showing sending data as a raw stream
        /// </summary>
        /// <returns></returns>
        [HttpPost("raw")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> GetRawFile()
        {
            using (FileStream fs = new FileStream("file_raw", FileMode.OpenOrCreate))
            {
                byte[] buf = new byte[4096];
                while (await Request.Body.ReadAsync(buf, 0, buf.Length) > 0)
                {
                    await fs.WriteAsync(buf);
                }
            }

            return Ok();
        }


    }
}
