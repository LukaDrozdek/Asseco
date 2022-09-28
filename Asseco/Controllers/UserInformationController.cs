using Asseco.Data;
using Asseco.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Asseco.Controllers
{
    public class UserInformationController : Controller
    {

        private readonly ApplicationDbContext _db;



        public UserInformationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public JsonController jsonController = new JsonController();
        

        public IActionResult Index()
        {
            
            IEnumerable<UserInformation> userList = _db.UserInformation;
            return View(userList);
        }

        
        [HttpGet("Limit")]
        public IActionResult Create()
        {
            return View("Create");
        }  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(UserInformation? obj)
        {

            //jsonController.Json(obj);
            _db.UserInformation.Add(obj);
            _db.SaveChanges();
            //Email(obj);
            return RedirectToAction("Index");

        }

        public IActionResult Details(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.UserInformation.Find(Id);

            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(UserInformation obj)
        {
            jsonController.Json(obj);
            _db.UserInformation.Update(obj);
            _db.SaveChanges();
            //Email(obj);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.UserInformation.Find(Id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(UserInformation obj)
        {
            _db.UserInformation.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult SearchForBizMail()
        {
            var biz = from g in _db.UserInformation where g.Email.EndsWith(".biz") select g;
            return View(biz);
        }


        public void Email(UserInformation obj)
        {

            //Mail message
            var message = new MimeMessage();

            //Mail Content
            message.From.Add(MailboxAddress.Parse("luka04111993@gmail.com"));
            message.To.Add(MailboxAddress.Parse("luka04111993@gmail.com"));
            message.Subject = "Asseco";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Name: " + obj.Name +
                       "Lastname: " + obj.Lastname +
                       "Email: " + obj.Email +
                       "City: " + obj.City +
                       "Street: " + obj.Street +
                       "Suite: " + obj.Suite +
                       "Zipcode: " + obj.Zipcode +
                       "Lat: " + obj.Lat +
                       "Lng: " + obj.Lng +
                       "Phone: " + obj.Phone +
                       "Website: " + obj.Website +
                       "CompanyName: " + obj.CompanyName +
                       "CompanyCatchPhrase: " + obj.CompanyCatchPhrase +
                       "Bs: " + obj.Bs
            };

            //Server details
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.email.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

            //Credentails
            smtp.Authenticate("augustine.bauch17@ethereal.email", "cTn5pq2AgfHfmJVEQh");

            //SendEmail
            smtp.Send(message);
            smtp.Disconnect(true); 

        }

    }
}
