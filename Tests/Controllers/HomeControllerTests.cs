using System;
using System.Collections.Generic;
using System.Text;
using WebApp;
using WebApp.Controllers;
using WebApp.Models;
using AnagramGenerator.Contracts;
using NUnit;
using NSubstitute;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;

namespace AnagramGenerator.Tests.Controllers
{
    public class HomeControllerTests
    {
        private HomeController _homeController;
        private IUsersRepository _usersRepository;
        private IAnagramsService _anagramsService;

        [SetUp]
        public void Setup()
        {
            _usersRepository = Substitute.For<IUsersRepository>();
            _anagramsService = Substitute.For<IAnagramsService>();

            _homeController = new HomeController(_usersRepository, _anagramsService);

        }

        [Test]
        public void Index_Should_Return_View_Without_Anagrams_In_Model_When_Input_Word_Is_Not_Supplied()
        {
            ActionResult viewResult = _homeController.Index("");
        }

        

    }
}
