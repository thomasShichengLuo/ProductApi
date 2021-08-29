using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.TestHost;
using Framecad.Nexa.MyFramecad.Tests;

namespace Product.Test
{
    public abstract class BaseTest
    {
        protected internal readonly TestServer _testServer;

        public readonly IFlurlClient flurlClient;

        protected BaseTest(TestServerFixture testServerFixture)
        {
            _testServer = testServerFixture.Server;
            flurlClient = new FlurlClient(testServerFixture.CreateDefaultClient(new Uri("http://localhost:5204")));
        }

    }
}
