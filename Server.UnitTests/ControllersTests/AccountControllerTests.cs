using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Controllers;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Server.UnitTests.ControllersTests
{
    public class AccountControllerTests
    {
        private AccountController controller;
        public AccountControllerTests()
        {
            this.controller = Mock.Of<AccountController>();
        }

        // Function Tests

        [Fact]
        public void ManageViewTest()
        {
            controller.Should().BeOfType<ViewResult>();

        }
    
    

        // API Tests
  
    }
}
