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

        #region CRUD

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
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<UserDto>(url + "Users/GetById/"+id);
            UserUpdateViewModel userUpdateViewModel = new UserUpdateViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                GenderID = user.Gender == true ? 1 : 2,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                Password = user.Password,
                UserName = user.UserName,
                Email = user.Email,
            };
            ViewBag.GenderList = GenderFill();
            return View(userUpdateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UserUpdateViewModel userUpdateViewModel)
        {
            
            UserUpdateDto userUpdateDto = new UserUpdateDto()
            {
                FirstName = userUpdateViewModel.FirstName,
                LastName = userUpdateViewModel.LastName,
                Gender = userUpdateViewModel.GenderID == 1 ? true : false,
                Address = userUpdateViewModel.Address,
                DateOfBirth = userUpdateViewModel.DateOfBirth,
                Password = userUpdateViewModel.Password,
                UserName = userUpdateViewModel.UserName,
                Email = userUpdateViewModel.Email,
                Id = id,
            };
            HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(url + "Users/Update", userUpdateDto);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }

        #endregion

        #region Methods

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

        #endregion
    }
}
