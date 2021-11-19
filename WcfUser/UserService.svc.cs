using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfUser.Models;

namespace WcfUser
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        T12008M0Entities _ctx = new T12008M0Entities();

        public async Task<bool> Create(UserContract user)
        {
            //var birthday = user.Birthday.Value.ToString("yyyy-MM-dd");
            //Date
            // tạo đối tượng User từ UserContract
            var bd = DateTime.ParseExact(user.Birthday, "yyyy-MM-dd", null);
            User u = new User { Username = user.Username, Password = user.Password, Fullname = user.Fullname, Birthday = bd, Email = user.Email, Role = user.Role };
            // lưu vào db
            _ctx.Users.Add(u);
            return (await _ctx.SaveChangesAsync() > 0);
        }

        public async Task<UserContract> FindByUsername(string username)
        {
            User user = await _ctx.Users.SingleOrDefaultAsync(u => u.Username == username);
            return new UserContract {Id = user.Id, Username = user.Username, Password = user.Password, Fullname = user.Fullname, Birthday = user.Birthday.Value.ToString(), Email = user.Email, Role = user.Role };
        }

        public async Task<UserContract> FindByUsernameOrEmail(string name)
        {
            User user = await _ctx.Users.SingleOrDefaultAsync(u => u.Username == name || u.Email == name);
            return new UserContract { Id = user.Id, Username = user.Username, Password = user.Password, Fullname = user.Fullname, Birthday = user.Birthday.Value.ToString(), Email = user.Email, Role = user.Role };
        }

        public async Task<UserContract> GetUser(string id)
        {
            int uid = Convert.ToInt32(id);
            User user = await _ctx.Users.SingleOrDefaultAsync(u => u.Id == uid);
            return new UserContract { Id = user.Id, Username = user.Username, Password = user.Password, Fullname = user.Fullname, Birthday = user.Birthday.Value.ToString(), Email = user.Email, Role = user.Role };
        }

        public async Task<List<UserContract>> GetUsers()
        {
            return await _ctx.Users
                .Select(user => new UserContract { Id = user.Id, Username = user.Username, Password = user.Password, Fullname = user.Fullname, Birthday = user.Birthday.Value.ToString(), Email = user.Email, Role = user.Role })
                .ToListAsync();
        }
    }
}
