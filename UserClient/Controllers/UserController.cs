using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserClient.Models;

namespace UserClient.Controllers
{
    public class UserController : Controller
    {
        const string BASE_URL = "http://localhost:51143/UserService.svc";
        IHttpClientFactory _factory;

        public UserController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            HttpClient client = _factory.CreateClient();
            var respone = await client.GetAsync(BASE_URL + $"/api/user/username/{username}");
            // nhận về chuỗi json mô tả thông tin của user
            var data = await respone.Content.ReadAsStringAsync();
            // chuyển chuyển json thành object của lớp User
            var user = JsonConvert.DeserializeObject<User>(data);
            client.Dispose();   // hoàn thành phải giải phóng bộ nhớ cho client

            if (user != null && user.Password == password)
            {
                // đăng nhập đúng
                // lưu thông tin user vào session, sẽ lưu chuỗi json
                HttpContext.Session.SetString("user", data);
                if (user.Role == 1)
                {
                    return RedirectToAction("ListUsers");
                }
                else
                {
                    return RedirectToAction("UserDetails");
                }
            }
            else
            {
                // đăng nhập sai
                ViewData["Error"] = "Invalid Username or Password";
                return View("Index");
            }
        }

        public async Task<IActionResult> ListUsers()
        {
            HttpClient client = _factory.CreateClient();
            var response = await client.GetAsync(BASE_URL + "/api/users");
            var data = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            client.Dispose();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            HttpClient client = _factory.CreateClient();
            // biến đổi đối tượng user thành chuỗi json
            //var birthday = user.Birthday.Value.ToString("yyyy-MM-dd");

            var userJson = JsonConvert.SerializeObject(user);
            var strContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(BASE_URL + "/api/user", strContent);
            client.Dispose();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListUsers");
            }
            ViewData["Error"] = "Some fields are not valid";
            return View();
        }

        public IActionResult UserDetails()
        {
            var userJson = HttpContext.Session.GetString("user");
            // chuyển chuyển json thành object của lớp User
            var user = JsonConvert.DeserializeObject<User>(userJson);
            return View(user);
        }
    }
}
