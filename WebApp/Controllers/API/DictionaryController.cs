﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Configuration;
using AnagramGenerator.Contracts.WebAPIResponseModels;
using WebApp.Models;

namespace WebApp.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private Dictionary _dictionaryConfiguration;
        private IWordsRepository _wordsRepository;
        private DictionaryViewModel _dictionaryModel;

        private IDictionaryService _dictionaryService;

        public DictionaryController(
            IOptions<Dictionary> dictionaryConfiguration,
            IWordsRepository wordsRepository,
            IDictionaryService dictionaryService)
        {
            _dictionaryConfiguration = dictionaryConfiguration.Value;
            _dictionaryService = dictionaryService;
            _wordsRepository = wordsRepository;

        }


        [HttpGet]
        [HttpGet("{pageNumber}")]
        public IActionResult Page(int pageNumber = 1)
        {
            List<WordResponseModel> words = _wordsRepository
                                            .GetPageOfWords(_dictionaryConfiguration.pageSize, pageNumber);
                                     
            if (words.Count > 0)
            {
                return Ok(new { page = pageNumber, words });

            } else
            {
                return NotFound();
            }
        }
    }
}