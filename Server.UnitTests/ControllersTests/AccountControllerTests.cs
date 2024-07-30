using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Controllers;
using Moq;

namespace Server.UnitTests.ControllersTests
{
    public class AccountControllerTests
    {
        private AccountController controller;
        public AccountControllerTests(AccountControllerTests controller)
        {
            this.controller = Mock.Of<AccountController>();
        }
    }

}
