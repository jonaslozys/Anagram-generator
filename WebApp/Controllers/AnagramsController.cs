using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnagramLogic;
using Contracts;
using WebApp.Configuration;
using WebApp.Models;
using Microsoft.Extensions.Options;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagramsController : ControllerBase
    {
        private readonly AnagramSettings _anagramSettings;
        private IWordsRepository _wordsRepository;
        private IAnagramSolver _anagramSolver;
        private AnagramConfiguration _anagramConfiguration;

        public AnagramsController(IOptionsMonitor<AnagramSettings> anagramSettings)
        {
            this._anagramSettings = anagramSettings.CurrentValue;
            this._wordsRepository = new WordsRepository();
            this._anagramConfiguration = new AnagramConfiguration(_anagramSettings.minWordLength, _anagramSettings.maxResultsLength);
        }

        [HttpGet("{word}")]
        public async Task<ActionResult<List<string>>> GetAnagrams(string word)
        {
            this._anagramSolver = new AnagramSolver(_wordsRepository, _anagramConfiguration);

            List<string> anagrams = this._anagramSolver.GetAnagrams(word);

            if (anagrams.Count > 0)
            {
                return Ok(anagrams);
            } else
            {
                return NotFound();
            }
        } 
    }
}