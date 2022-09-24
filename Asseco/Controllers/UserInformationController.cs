using Asseco.Data;
using Asseco.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asseco.Controllers
{
    public class UserInformationController : Controller
    {

        private readonly ApplicationDbContext _db;


        public UserInformationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<UserInformation> userList = _db.UserInformation;
            return View(userList);
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserInformation? obj)
        {
            _db.UserInformation.Add(obj);
            _db.SaveChanges();
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
        public IActionResult Edit(UserInformation obj)
        {
            _db.UserInformation.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(UserInformation obj)
        {
            _db.UserInformation.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
