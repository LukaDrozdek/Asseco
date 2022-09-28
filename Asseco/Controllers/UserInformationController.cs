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

        public string obj;

        public IActionResult Index()
        {

            IEnumerable<UserInformation> userList = _db.UserInformation;
            return View(userList);
        }

        public void json(UserInformation obj)
        {
          
            WebClient client = new WebClient();
            var strPageCode = client.DownloadString("http://jsonplaceholder.typicode.com/users");

            dynamic blogPosts = JArray.Parse(strPageCode);
            for (int i = 0; i < blogPosts.Count ; i++)
            {
                dynamic blogPost = blogPosts[i];
                Console.WriteLine(blogPost);

                string name = blogPost.name;
                string username = blogPost.username;
                string email = blogPost.email;

                string street = blogPost.address.street;
                string suite = blogPost.address.suite;
                string city = blogPost.address.city;
                string zipcode = blogPost.address.zipcode;
                float lat = blogPost.address.geo.lat;
                float lng = blogPost.address.geo.lng;

                string phone = blogPost.phone;
                string website = blogPost.website;

                string conpanyName = blogPost.company.name;
                string catchPhrase = blogPost.company.catchPhrase;
                string bs = blogPost.company.bs;


                if (obj.Lastname == username && obj.Email == email)
                {
                    obj.Street = street;
                    obj.Suite = suite;
                    obj.City = city;
                    obj.Zipcode = zipcode;
                    obj.Lat = lat;
                    obj.Lng = lng;
                    obj.Phone = phone;
                    obj.Website = website;
                    obj.CompanyName = conpanyName;
                    obj.CompanyCatchPhrase = catchPhrase;
                    obj.Bs = bs;
                }
            }
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
            json(obj);
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
            json(obj);
            _db.UserInformation.Update(obj);
            _db.SaveChanges();
            //Email(obj);
            return RedirectToAction("Index");
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
