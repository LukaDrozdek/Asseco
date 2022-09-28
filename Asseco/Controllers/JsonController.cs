using Asseco.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Asseco.Controllers
{
    public class JsonController : Controller
    {
        public void Json(UserInformation obj)
        {
            WebClient client = new WebClient();
            var strPageCode = client.DownloadString("http://jsonplaceholder.typicode.com/users");

            dynamic blogPosts = JArray.Parse(strPageCode);
            for (int i = 0; i < blogPosts.Count; i++)
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
    }
}
