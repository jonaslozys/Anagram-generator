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
            IOptions<Dictionary> dictionaryConfiguration,
            IWordsRepository wordsRepository,
            IDictionaryService dictionaryService)
        {
            _dictionaryConfiguration = dictionaryConfiguration.Value;
            _dictionaryService = dictionaryService;
            _wordsRepository = wordsRepository;
            _dictionaryModel = new DictionaryViewModel();

        }
        [HttpGet, Route("")]
        [HttpGet, Route("{page:int?}")]
        public IActionResult Index(int page = 1, string ErrorMessage = null)
        {
            if (ErrorMessage != null)
            {
                _dictionaryModel.ErrorMessage = ErrorMessage;
            }
            _pageSize = _dictionaryConfiguration.pageSize;
            _dictionaryModel.wordsDictionary = _wordsRepository.GetPageOfWords(_pageSize, page);
            _dictionaryModel.page = page;
            
            return View(_dictionaryModel);
        }
        [HttpPost, Route("")]
        [HttpPost, Route("{page:int?}")]
        public IActionResult Search(string search)
        {
            if (search != null && search.Length > 0)
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

            try
            {
                _dictionaryService.DeleteWord(wordToDelete, ip);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { ErrorMessage = ex.Message });
            }

            return RedirectToAction("Index");
        }

        [HttpPost("AddWord")]
        public IActionResult AddWord(IFormCollection collection)
        {
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            string wordToAdd = collection["word"];

            try
            {
                _dictionaryService.AddWord(wordToAdd, ip);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { ErrorMessage = ex.Message });
            }

            return RedirectToAction("Index");
        }

        [HttpPost("UpdateWord/{wordIndex:int?}")]
        public IActionResult UpdateWord(IFormCollection collection, int wordIndex = -1)
        {
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();
            string wordToUpdate = collection["word"];


            try
            {
                _dictionaryService.UpdateWord(wordToUpdate, ip, wordIndex);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { ErrorMessage = ex.Message });
            }
            return RedirectToAction("Index");
        }
    }
}