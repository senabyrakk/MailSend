using MailSend.Models;
using MailSend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MailSend.Controllers
{
    [Route("api/mailsend")]
    [ApiController]
    public class MailController : Controller
    {
        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Send([FromBody]MailDto dto)
        {
           var result = await MailManager.SendAsync(dto);
            return StatusCode(200,result);
        }
    }
}
