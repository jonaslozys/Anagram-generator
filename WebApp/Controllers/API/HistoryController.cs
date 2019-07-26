using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        
        private IUsersRepository _usersRepository;
        public HistoryController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public IActionResult Index ()
        {
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();

            HistoryViewModel _historyModel = new HistoryViewModel();

            try
            {
                _historyModel.HistoryLogs = _usersRepository.GetUserLogs(ip);
                return Ok(new { _historyModel.HistoryLogs });
            } catch (Exception ex)
            {
                return BadRequest("Unable to find history for the user.");
            }
        }
    }
}