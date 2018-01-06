namespace Nancy.Response.MessagePack.Test
{
    using Nancy.Responses;
    using Nancy.Testing;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Xunit;

    public class MessagePackResponseTest
    {
        private const string UserName = "catcher";
        private const int UserAge = 18;
        private const string VendorContentType = "application/vnd.company.product.v1+x-msgpack";

        [Fact]
        public async Task TestGetAsync()
        {            
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);
            var url = string.Format("/getmsgpack/{0}/{1}", UserName, UserAge);
                     
            var response = await browser.Get(url, with =>
            {
                with.HttpRequest();
                with.Accept(ConstValue.MessagePackContentType);
            });

            CheckResponse(response, UserName, UserAge);
        }

        [Fact]
        public async Task TestGetWithVendorContentTypeAsync()
        {            
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);
            var url = string.Format("/getmsgpack/{0}/{1}", UserName, UserAge);

            var response = await browser.Get(url, with =>
            {
                with.HttpRequest();

                with.Accept(VendorContentType);
            });

            CheckResponse(response, UserName, UserAge);
        }

        [Fact]
        public async Task TestPostAsync()
        {            
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);

            var response = await browser.Post("/postmsgpack", with =>
            {
                with.HttpRequest();
                with.FormValue("Name", UserName);
                with.FormValue("Age", UserAge.ToString());
                with.Accept(ConstValue.MessagePackContentType);
            });

            CheckResponse(response, UserName, UserAge);
        }

        [Fact]
        public async Task TestPostWithVendorContentTypeAsync()
        {            
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);

            var response = await browser.Post("/postmsgpack", with =>
            {
                with.HttpRequest();
                with.FormValue("Name", UserName);
                with.FormValue("Age", UserAge.ToString());
                with.Accept(VendorContentType);
            });

            CheckResponse(response, UserName, UserAge);
        }

        private static void CheckResponse(BrowserResponse response, string name, int age)
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ConstValue.MessagePackContentType, response.Context.Response.ContentType);
            Assert.IsType<MessagePackResponse>(response.Context.Response);

            using (var memStream = new MemoryStream())
            {
                response.Context.Response.Contents(memStream);

                var returnedUser = DefaultMessagePackSerializer.Deserialize<User>(memStream);

                Assert.NotNull(returnedUser);
                Assert.Equal(name, returnedUser.Name);
                Assert.Equal(age, returnedUser.Age);
            }
        }
    }
}
