using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Console
{
    class UserContext : DbContext
    {
        public UserContext() : base("DbConnection")
        {
        }
        public DbSet<User> Users { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            using (UserContext db = new UserContext())
            {
                // создаем два объекта User
                User user1 = new User { Name = "Tom", Age = 33 };
                User user2 = new User { Name = "Sam", Age = 26 };

                // добавляем их в бд
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
                System.Console.WriteLine("Объекты успешно сохранены");

                // получаем объекты из бд и выводим на консоль
                var users = db.Users;
                System.Console.WriteLine("Список объектов:");
                foreach (User u in users)
                {
                    System.Console.WriteLine("{0}.{1} - {2}", u.Id, u.Name, u.Age);
                }
                System.Console.Read();
            }
        }
    }
}
