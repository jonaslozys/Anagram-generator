﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using AnagramGenerator.Contracts;
using Microsoft.AspNetCore.Http;
using AnagramGenerator.EF.DatabaseFirst;
using AnagramGenerator.EF.DatabaseFirst.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        //private IUsersRepository _usersRepository;
        private EfUsersRepository _efUsersRepository;
        private IAnagramsService _anagramsService;
        private AnagramsViewModel _anagramsModel;

        public HomeController(
            EfUsersRepository efUsersRepository,
            IAnagramsService anagramsService)
        {
            //_usersRepository = usersRepository;
            _efUsersRepository = efUsersRepository;
            _anagramsService = anagramsService;
        }
        public ActionResult Index(string word)
        {
            _anagramsModel = new AnagramsViewModel();

            if (word != null)
            {
                _anagramsModel.Word = word;
                string ip = HttpContext.Connection.RemoteIpAddress.ToString();
                UserLog userLog = new UserLog() { UserIp = ip, WordSearched = word, SearchDate = DateTime.Now };
                _efUsersRepository.AddUserLog(userLog, word);

                _anagramsModel.Anagrams = _anagramsService.GetAnagrams(word);

                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddMinutes(10);
                string searchHistory = Request.Cookies["searchHistory"];
                if (searchHistory == null)
                {
                    searchHistory += $"{word}";

                } else
                {
                    searchHistory += $",{word}";
                }

                Response.Cookies.Append("searchHistory", searchHistory, cookieOptions);
            }
            return View(_anagramsModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
