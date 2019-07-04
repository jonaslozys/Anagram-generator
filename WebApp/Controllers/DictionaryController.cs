using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Contracts;
using AnagramLogic;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private IWordsRepository _wordsRepository;
        private DictionaryModel _dictionaryModel;
        private int _pageSize;

        public DictionaryController(IConfiguration config)
        {
            _wordsRepository = new WordsRepository();
            _dictionaryModel = new DictionaryModel();
            _pageSize = Int32.Parse(config.GetSection("Dictionary:pageSize").Value);

        }
        [Route("Dictionary/{page?}")]
        public IActionResult Index(int page = 1)
        {
            _dictionaryModel.wordsDictionary = _wordsRepository.GetPageOfWords(_pageSize, page);
            _dictionaryModel.page = page;
            return View(_dictionaryModel);
        }
    }
}