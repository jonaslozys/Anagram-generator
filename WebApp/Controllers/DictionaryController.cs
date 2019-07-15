using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using AnagramGenerator.Contracts;
using AnagramGenerator.BusinessLogic;
using Microsoft.Extensions.Configuration;
using WebApp.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using AnagramGenerator.EF.DatabaseFirst;
using AnagramGenerator.Ef.CodeFirst;

namespace WebApp.Controllers
{
    [Route("dictionary")]
    public class DictionaryController : Controller
    {
        private Dictionary _dictionaryConfiguration;
        private WordsEfCodeFirstRepository _wordsRepository;
        private DictionaryViewModel _dictionaryModel;
        private int _pageSize;

        public DictionaryController(IOptionsMonitor<Dictionary> dictionaryConfiguration, WordsEfCodeFirstRepository wordsRepository)
        {
            _dictionaryConfiguration = dictionaryConfiguration.CurrentValue;
            _wordsRepository = wordsRepository;
            _dictionaryModel = new DictionaryViewModel();

        }
        [HttpGet, Route("")]
        [HttpGet, Route("{page:int?}")]
        public IActionResult Index(int page = 1)
        {
            _pageSize = _dictionaryConfiguration.pageSize;
            _dictionaryModel.wordsDictionary = _wordsRepository.GetPageOfWords(_pageSize, page);
            _dictionaryModel.page = page;
            
            return View(_dictionaryModel);
        }
        [HttpPost, Route("")]
        [HttpPost, Route("{page:int?}")]
        public IActionResult Search(string search)
        {
            if (search != null)
            {
                _dictionaryModel.wordsDictionary = _wordsRepository.GetSearchedWords(search);
                _dictionaryModel.SearchString = search;
            }

            return View(_dictionaryModel);
        }

        [HttpGet, Route("download")]
        public IActionResult Download()
        {
            IFileRepository fileRepository = new FileRepository();

            byte[] fileBytes = fileRepository.GetDictionaryFile();
            return File(fileBytes, "text/plain");
        }

        [HttpPost, Route("delete")]
        public IActionResult Delete(IFormCollection colletion)
        {
            string wordToDelete = colletion["word"];
            _wordsRepository.DeleteWord(wordToDelete);
            return RedirectToAction("Index");
        }
    }
}