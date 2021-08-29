using Flurl.Http;
using Framecad.Nexa.MyFramecad.Tests;
using Newtonsoft.Json;
using Product.Core.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Product.Test
{
    [Collection(nameof(TestCollection))]
    public class ProductUnitTest: BaseTest
    {
        private readonly IFlurlClient _client;
        private readonly string _writeKey = "dJL*(Dkfje89345";
        private readonly string _readKey = "49fkJfi0e!9d";

        public ProductUnitTest(TestServerFixture testServerFixture) : base(testServerFixture)
        {
            _client = flurlClient; 
        }
        [Fact]
        public void TestAdd()
        {

            var result = _client.Request("api", "Product?writeKey=" + _writeKey).PostJsonAsync(new
            {
                Name = "MyProduct 01"
            }).Result;

            Assert.Equal(200,result.StatusCode);

        }
        [Fact]
        public void TestGetAsync()
        {

            IFlurlResponse result = _client.Request("api", $"Product?pageSize=2&pageIndex=0&readKey={_readKey}").GetAsync().Result;
            Assert.Equal(200, result.StatusCode);

        }
        [Fact]
        public void TestDelete()
        {
            var id = Guid.NewGuid();
            var resultAdd = _client.Request("api", "Product?writeKey=" + _writeKey).PostJsonAsync(new
            {
                Name = "MyProduct 01",
                Id = id
            }).Result;
            string v = $"Product/{id}?writeKey=";
            var result =  _client.Request("api", v + _writeKey).DeleteAsync().Result;

            Assert.Equal(200, result.StatusCode);

        }
        [Fact]
        public async void TestSearch()
        {
            var id = Guid.NewGuid();
            var resultAdd = _client.Request("api", "Product?writeKey=" + _writeKey).PostJsonAsync(new
            {
                Name = "MyProduct 01",
                Id = id
            }).Result;
            List<Core.Domain.Product> list = new List<Core.Domain.Product>();
            string v = $"Product/search/MyProduct%2001/pageSize/1/pageIndex/0?readKey=" +_readKey;
            var result = await _client.Request("api", v).GetJsonAsync<List<Core.Domain.Product>>();
            Core.Domain.Product product = result[0];
            Assert.Equal("MyProduct 01", product.Name);

        }
    }
}
