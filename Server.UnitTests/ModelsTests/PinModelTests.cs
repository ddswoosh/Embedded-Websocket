using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Server.Models;

namespace Server.UnitTests.ModelsTests
{
    public class PinModelTests
    {
        private PinLive controller;
        public PinModelTests()
        {
            this.controller = Mock.Of<PinLive>();
        }
    }
}
