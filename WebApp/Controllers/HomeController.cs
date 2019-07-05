using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using AnagramLogic;
using Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Configuration;
using Microsoft.Extensions.Options;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AnagramSettings _anagramSettings;
        private IWordsRepository _wordsRepository;
        private IAnagramSolver _anagramSolver;
        private AnagramConfiguration _anagramConfiguration;

        private List<string> _anagrams;

        public HomeController(IOptionsMonitor<AnagramSettings> anagramSettings)
        {
            _anagramSettings = anagramSettings.CurrentValue;
            _wordsRepository = new WordsRepository();
            _anagramConfiguration = new AnagramConfiguration(
                _anagramSettings.minWordLength,
                _anagramSettings.maxResultsLength
            );

            _anagramSolver = new AnagramSolver(_wordsRepository, _anagramConfiguration);
        }
        public ActionResult Index(string word)
        {
            if (word != null)
            {
                _anagrams = _anagramSolver.GetAnagrams(word);
                ViewBag.Word = word;
                ViewBag.Words = _anagrams;
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
