using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testapp.Models;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace testapp.Controllers
{

    [Route("api/user/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            _context = context;
        }

   
        [HttpPost]
        public async Task<ActionResult> Login(string email, string password) 
        {
            var _user = await _context.Users.Where(m => m.Email == email && m.Password == password).SingleOrDefaultAsync();
            if (_user == null)
                return Ok(new User());
      
            return Ok(_user);
        }

        [HttpPost]
        public async Task<ActionResult> Logout(int id)
        {
            var _user = await _context.Users.Where(m => m.UserId == id).SingleOrDefaultAsync();
            if (_user == null)
                return Ok(new User());
            return Ok(new User());
        }

        [HttpPost]
        public async Task<ActionResult> PageLoad([FromBody] User user) 
        {
            if (user.UserId == 0)
            {
                return Ok(new User());
            }

            var _user = await _context.Users.Where(m => m.UserId == user.UserId && m.Password == user.Password).SingleOrDefaultAsync();
            return Ok(_user);
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            /* using (ApplicationContext db = new ApplicationContext())
             {
                 _context.Database.EnsureCreated();
                 db.Database.EnsureCreated();

                 User user1 = new User { Name = "Tom" };
                 User user2 = new User { Name = "Alice" };

                 db.Users.Add(user1);
                 db.Users.Add(user2);
                 db.SaveChanges();
             }*/
            return new string[] { "value1", "value2" };
        }

        /*
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }*/
            
        // POST api/user/register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var AzureStorage = new AzureQueueStorage();

            User _user = new User
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password,
                SecondName = user.SecondName,
                IsClient = false
            };

            //User user1 = new User { Name = "Tom", IsClient = false};
            var userdb = await _context.Users.Where(m => m.Email == user.Email).SingleOrDefaultAsync();
            if (userdb != null)
            {
                return Ok("{\"err\": \"Юзер с таким емейлом уже есть\"}");
            }

            await _context.Users.AddAsync(_user);
            await _context.SaveChangesAsync();

            AzureStorage.AddMessage(_user);

            return Ok(_user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
