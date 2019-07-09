using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using AnagramLogic;
using Contracts;
using WebApp.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AnagramSettings _anagramSettings;
        private IWordsRepository _wordsRepository;
        private IAnagramSolver _anagramSolver;
        private AnagramConfiguration _anagramConfiguration;
        private AnagramsModel _anagramsModel;
        private List<string> _anagrams;

        public HomeController(IOptionsMonitor<AnagramSettings> anagramSettings, IWordsRepository wordsRepository, IAnagramSolver anagramSolver)
        {
            _anagramSettings = anagramSettings.CurrentValue;
            _wordsRepository = wordsRepository;
            _anagramConfiguration = new AnagramConfiguration(
                _anagramSettings.minWordLength,
                _anagramSettings.maxResultsLength
            );
            _anagramSolver = anagramSolver;
        }
        public ActionResult Index(string word)
        {
            _anagramsModel = new AnagramsModel();

            if (word != null)
            {
                _anagrams = _anagramSolver.GetAnagrams(word, _anagramConfiguration);
                _anagramsModel.Anagrams = _anagrams;
                _anagramsModel.Word = word;

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
