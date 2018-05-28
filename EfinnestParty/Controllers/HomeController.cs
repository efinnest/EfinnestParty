using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EfinnestParty.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace EfinnestParty.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "This is a Webapp for my upcoming Party.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [HttpGet]
        public IActionResult RsvpForm()
        {

            return View();
        }
        [HttpPost]
        public IActionResult RsvpForm(Invitees invite)
        {
            if (ModelState.IsValid)
            {

                Repository.AddInvites(invite);
                return View("Thanks", invite);
            }
            else
            {
                return View();
            }
        }
        public IActionResult ListInvites()
        {
            return View(Repository.invites.Where(r => r.will_attend == true));
        }
        public IActionResult Thanks()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }
    }
}
