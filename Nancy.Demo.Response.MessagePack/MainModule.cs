namespace Nancy.Demo.Response.MessagePack
{
    using Nancy.Responses;
    using System.Net.Http;
    
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get("/", _ => 
            {
                HttpClient client = new HttpClient();

                var st = client.GetStreamAsync("http://localhost:9999/msg").Result;

                return (string)DefaultMessagePackSerializer.Deserialize(st);
            });

            Get("/msg", _ =>
            {
                return Response.AsMessagePack("123");
            });

        }
    }
}
