using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using WebAPICoreMvc.ViewModels;

namespace WebAPICoreMvc.Controllers
{
    public class UsersController : Controller
    {
        #region Define

        private readonly HttpClient _httpClient;
        private string url= "http://localhost:28493/api/";


        #endregion

        #region Constructor

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var users = await _httpClient.GetFromJsonAsync<List<UserDetailDto>>(url+"Users/GetList");
            return View(users);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.GenderList = GenderFill();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddViewModel userAddViewModel)
        {
            UserAddDto userAddDto = new UserAddDto()
            {
                FirstName= userAddViewModel.FirstName,
                LastName= userAddViewModel.LastName,
                Gender = userAddViewModel.GenderID == 1 ? true : false,
                Address= userAddViewModel.Address,
                DateOfBirth = userAddViewModel.DateOfBirth,
                Password = userAddViewModel.Password,
                UserName= userAddViewModel.UserName,
                Email= userAddViewModel.Email,

            };
            HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(url + "Users/Add" , userAddDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        private List<Gender> GenderFill()
        {
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender(){Id=1 , GenderName="Erkek"});
            genders.Add(new Gender(){Id=1 , GenderName="Kadın"});
            return genders;
        }
        class Gender
        {
            public int Id { get; set; }
            public string GenderName { get; set; }
        }
    }
}
