using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnagramGenerator.BusinessLogic;
using AnagramGenerator.Contracts;
using WebApp.Configuration;
using WebApp.Models;
using Microsoft.Extensions.Options;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagramsController : ControllerBase
    {
        private IAnagramsService _anagramsService;

        public AnagramsController(IAnagramsService anagramsService)
        {
            _anagramsService = anagramsService;
        }

        [HttpGet("{word}")]
        public async Task<ActionResult<List<string>>> GetAnagrams(string word)
        {

            List<string> anagrams = _anagramsService.GetAnagrams(word);

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