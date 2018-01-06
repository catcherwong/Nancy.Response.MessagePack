# Nancy.Responses.MessagePack

There are many response type we can choose when we use Nancy to build our Web API . For example , JSON , XML , ProtBuf . etc .

JSON is the most popular , but there are a few pros in replacing the default JSON serializator in Nancy with MessagePack:

- JSON uses 4 bytes to represent null, MessagePack only requires 1 byte;
- JSON uses 2 bytes to represent a typical int, MessagePack requires 1 byte and so on;
- MessagePack is faster to read and write than JSON.

ProtBuf is also a good choose , but when we use ProtBuf , we need to mark our class as `[ProtoContract]` and public members(property or field) mark as `[ProtoMember]`. This is a little complex!

So `Nancy.Responses.MessagePack` is come ! `Nancy.Responses.MessagePack` use [MessagePack-CSharp](https://github.com/neuecc/MessagePack-CSharp) as the default Serializer.

## Quick Start

### Instatll the package at first

```
Install-Package Nancy.Response.MessagePack
```

### Define A Class

```csharp
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

### Specify MessagePack Response

```csharp
using Nancy.Responses;

public class MainModule : NancyModule
{
    public MainModule()
    {
        Get("/{name}/{age}", _ =>
        {
            var data = new User { Name = args.name, Age = args.age };
            return Response.AsMessagePack(data);
        });

        Get("/other", _ =>
        {            
            return Response.AsMessagePack("your value");
        });
    }
}
```

### Based On User's Request

```csharp
public class MainModule : NancyModule
{
    public MainModule()
    {
        Get("/{name}/{age}", args =>
        {
            var data = new User { Name = args.name, Age = args.age };
            return Negotiate.WithModel(data);
        });
    }
}
```