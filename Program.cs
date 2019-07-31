using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using testapp.Models;

namespace testapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args); //CreateWebHostBuilder(args).Build().Run();

            /*using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureCreated();

                User user1 = new User { Name = "Tom"  };
                User user2 = new User { Name = "Alice" };

                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
                //Console.WriteLine("Объекты успешно сохранены");

                var users = db.Users.ToList();
                //Console.WriteLine("Список объектов:");
                foreach (User u in users)
                {
                    //Console.WriteLine($"{u.Id}.{u.Name}");
                }
            }*/
            //Console.Read();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}
