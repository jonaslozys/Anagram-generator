using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using AnagramLogic;
using Contracts;
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
        private AnagramsModel _anagramsModel;
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
            _anagramsModel = new AnagramsModel();

            if (word != null)
            {
                _anagrams = _anagramSolver.GetAnagrams(word);
                _anagramsModel.Anagrams = _anagrams;
                _anagramsModel.Word = word;
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
