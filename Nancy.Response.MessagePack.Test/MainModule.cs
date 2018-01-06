namespace Nancy.Response.MessagePack.Test
{
    using Nancy.ModelBinding;

    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Post("/postmsgpack", args =>
            {
                User data = this.Bind();
                return Negotiate.WithModel(data);
            });

            Get("/getmsgpack/{name}/{age}", args =>
            {
                var data = new User { Name = args.name, Age = args.age };
                return Negotiate.WithModel(data);
            });
        }
    }
}
