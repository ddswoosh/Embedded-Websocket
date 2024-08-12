using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Configurations;

namespace Server.UnitTests.ConfigurationsTests
{
    public class JWTParamsTests
    {
        [Fact]
        public async void GetSecretStringTest()
        {
            await Configuration.GetSecret();
        }
    }
}
