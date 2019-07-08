using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HistoryController : Controller
    {
        private HistoryModel _historyModel;
        public HistoryController()
        {
            this._historyModel = new HistoryModel();
        }
        public IActionResult Index()
        {
            string searchHistory = Request.Cookies["searchHistory"];

            if (searchHistory != null)
            {
                _historyModel.SearchHistory = searchHistory.Split(',').ToList();

            }

            return View(_historyModel);
        }
    }
}