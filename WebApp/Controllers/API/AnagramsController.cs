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
using AnagramGenerator.Contracts.WebAPIResponseModels;

namespace WebApp.Controllers.API
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
        public ActionResult Anagrams(string word)
        {
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();

            try
            {
                List<WordResponseModel> anagrams = _anagramsService.GetAnagrams(word, ip);
                if (anagrams.Count > 0)
                {
                    return Ok(new { anagrams });
                }
                else
                {
                    return NotFound("No anagrams were found");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        } 
    }
}