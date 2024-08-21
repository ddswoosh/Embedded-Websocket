using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Controllers;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Server.Configurations;
using Server.Utils;

namespace Server.UnitTests.ControllersTests
{
    public class AccountControllerTests
    {
        private AccountController controller;
        private Parser parser = new Parser();
        public AccountControllerTests()
        {
            this.controller = Mock.Of<AccountController>();
        }

        // Function Tests

        [Fact]
        public void ManageViewTest()
        {
            controller.Manage().Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void RegisterViewTest()
        {
            controller.Register().Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void LoginViewTest()
        {
            controller.Login().Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void TryLoginStringTest()
        {

        }

        [Fact]
        public void TryRegisterStringTest()
        {

        }

        [Fact]
        public void ReadJsonStringTest()
        {
            StreamReader body = new StreamReader(
                "username:testing,password:testing"
            );

            parser.ParseStream(body).Should().BeOfType<string[]>();
            parser.ParseStream(body).Should().BeEquivalentTo(
                new string[] {"testing", "testing" }
            );
            
        }

    }
}
