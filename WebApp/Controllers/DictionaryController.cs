using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Contracts;
using AnagramLogic;
using Microsoft.Extensions.Configuration;
using WebApp.Configuration;
using Microsoft.Extensions.Options;

namespace WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private Dictionary _dictionaryConfiguration;
        private IWordsRepository _wordsRepository;
        private DictionaryModel _dictionaryModel;
        private int _pageSize;

        public DictionaryController(IOptionsMonitor<Dictionary> dictionaryConfiguration)
        {
            _dictionaryConfiguration = dictionaryConfiguration.CurrentValue;
            _wordsRepository = new WordsRepository();
            _dictionaryModel = new DictionaryModel();

        }
        [Route("Dictionary/")]
        [Route("Dictionary/{page:int?}")]
        public IActionResult Index(int page = 1)
        {
            _pageSize = _dictionaryConfiguration.pageSize;
            _dictionaryModel.wordsDictionary = _wordsRepository.GetPageOfWords(_pageSize, page);
            _dictionaryModel.page = page;
            
            return View(_dictionaryModel);
        }

        public IActionResult Download()
        {
            string target = _dictionaryConfiguration.path;
            byte[] fileBytes = System.IO.File.ReadAllBytes(target);
            return File(fileBytes, "application/force-download");
        }
    }
}