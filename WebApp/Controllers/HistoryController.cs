using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using AnagramGenerator.Contracts;
using Microsoft.AspNetCore.Http;
using AnagramGenerator.EF.DatabaseFirst;

namespace WebApp.Controllers
{
    public class HistoryController : Controller
    {
        private HistoryViewModel _historyModel;
        //private IUsersRepository _usersRepository;
        private EfUsersRepository _efUsersRepository;
        public HistoryController(EfUsersRepository efUsersRepository)
        {
            _historyModel = new HistoryViewModel();
            _efUsersRepository = efUsersRepository;
        }
        public IActionResult Index()
        {
            /*string searchHistory = Request.Cookies["searchHistory"];

            if (searchHistory != null)
            {
                _historyModel.SearchHistory = searchHistory.Split(',').ToList();

            }*/
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();

            _historyModel.HistoryLogs = _efUsersRepository.GetUserLogs(ip);

            return View(_historyModel);
        }
    }
}