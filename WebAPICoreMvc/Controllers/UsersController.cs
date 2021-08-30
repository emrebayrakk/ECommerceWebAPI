﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;

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
    }
}
