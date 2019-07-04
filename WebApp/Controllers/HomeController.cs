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

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private IWordsRepository _wordsRepository;
        private IAnagramSolver _anagramSolver;
        private AnagramConfiguration _anagramConfiguration;

        private List<string> _anagrams;

        public HomeController(IConfiguration config)
        {
            _config = config;
            _wordsRepository = new WordsRepository();
            _anagramConfiguration = new AnagramConfiguration(
                Int32.Parse(config.GetSection("AnagramSettings:minWordLength").Value),
                Int32.Parse(config.GetSection("AnagramSettings:maxResultsLength").Value)
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
