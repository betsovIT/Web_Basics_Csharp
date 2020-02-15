using PandaWebApp.Data;
using PandaWebApp.Models;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using SIS.MvcFramework;
using PandaWebApp.ViewModels.Users;
using PandaWebApp.Models.Enums;
using PandaWebApp.ViewModels.Packages;

namespace PandaWebApp.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool AnyRegisteredUsers()
        {
            return db.Users.Any();
        }

        public bool EmailExists(string email)
        {
            return this.db.Users.Any(u => u.Email == email);
        }

        public UserInfoModel GetUserData(string id)
        {
            return this.db.Users
                .Where(u => u.Id == id)
                .Select(x => new UserInfoModel
                {
                    Username = x.Username,
                    Role = (int)x.Role,
                    PendingPackages = db.Packages.Where(p => p.RecipientId == id && p.Status == Status.Pendin)
                    .Select(v => new PackageInfoViewModel{Id = v.Id, Description = v.Description}).ToList(),
                    ShippedPackages = db.Packages.Where(p => p.RecipientId == id && p.Status == Status.Shipped)
                    .Select(v => new PackageInfoViewModel {Id = v.Id, Description = v.Description }).ToList(),
                    DeliveredPackages = db.Packages.Where(p => p.RecipientId == id && p.Status == Status.Delivered)
                    .Select(v => new PackageInfoViewModel {Id = v.Id, Description = v.Description }).ToList()

                }).FirstOrDefault();
        }

        public string GetUserId(string username, string password)
        {
            string id = this.db.Users.FirstOrDefault(u => u.Username == username && u.Password == this.Hash(password))?.Id;

            return id;
        }

        public void Register(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Password = this.Hash(password),
                Email = email,
                Role = AnyRegisteredUsers() ? IdentityRole.User : IdentityRole.Admin
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        public bool UsernameExists(string username)
        {
            return this.db.Users.Any(u => u.Username == username);
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
