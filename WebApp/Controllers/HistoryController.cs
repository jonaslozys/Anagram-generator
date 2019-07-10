using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Contracts;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class HistoryController : Controller
    {
        private HistoryModel _historyModel;
        private IUsersRepository _usersRepository;
        private IHttpContextAccessor _accessor;
        public HistoryController(IUsersRepository usersRepository, IHttpContextAccessor accessor)
        {
            _historyModel = new HistoryModel();
            _usersRepository = usersRepository;
            _accessor = accessor;
        }
        public IActionResult Index()
        {
            /*string searchHistory = Request.Cookies["searchHistory"];

            if (searchHistory != null)
            {
                _historyModel.SearchHistory = searchHistory.Split(',').ToList();

            }*/
            string ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            _historyModel.HistoryLogs = _usersRepository.GetUserLogs(ip);

            return View(_historyModel);
        }
    }
}