using Asseco.Data;
using Asseco.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asseco.Controllers
{
    public class UserInformationController : Controller
    {

        private readonly ApplicationDbContext _db;
        public JsonController jsonController = new JsonController();
        public EmailController emailController = new EmailController();


        public UserInformationController(ApplicationDbContext db)
        {
            _db = db;
        }

        

        public IActionResult Index()
        {
            
            IEnumerable<UserInformation> userList = _db.UserInformation;
            return View(userList);
        }

        

        // Create

        [HttpGet("Limit")]
        public IActionResult Create()
        {
            return View("Create");
        }  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(UserInformation? obj)
        {

            jsonController.Json(obj);
            _db.UserInformation.Add(obj);
            _db.SaveChanges();
            emailController.Email(obj);
            return RedirectToAction("Index");

        }



        // Details

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


        // Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(UserInformation obj)
        {
            jsonController.Json(obj);
            _db.UserInformation.Update(obj);
            _db.SaveChanges();
            emailController.Email(obj);
            return RedirectToAction("Index");
        }


        // Delete

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


        // .biz Mail

        public IActionResult SearchForBizMail()
        {
            var biz = from g in _db.UserInformation where g.Email.EndsWith(".biz") select g;
            return View(biz);
        }
    }
}
