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
        private IWordsRepository _wordsRepository;
        private DictionaryViewModel _dictionaryModel;

        private IDictionaryService _dictionaryService;
        private int _pageSize;

        public DictionaryController(
            IOptionsMonitor<Dictionary> dictionaryConfiguration,
            IWordsRepository wordsRepository,
            IDictionaryService dictionaryService)
        {
            _dictionaryConfiguration = dictionaryConfiguration.CurrentValue;
            _dictionaryService = dictionaryService;
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
        public IActionResult Delete(IFormCollection collection)
        {
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            string wordToDelete = collection["word"];

            _dictionaryService.DeleteWord(wordToDelete, ip);

            return RedirectToAction("Index");
        }

        [HttpPost("AddWord")]
        public IActionResult AddWord(IFormCollection collection)
        {
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            string wordToAdd = collection["word"];

            _dictionaryService.AddWord(wordToAdd, ip);

            return RedirectToAction("Index");
        }

        [HttpPost("UpdateWord/{wordIndex:int?}")]
        public IActionResult UpdateWord(IFormCollection collection, int wordIndex = -1)
        {
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            string wordToUpdate = collection["word"];

            _dictionaryService.UpdateWord(wordToUpdate, ip, wordIndex);
            return RedirectToAction("Index");
        }
    }
}