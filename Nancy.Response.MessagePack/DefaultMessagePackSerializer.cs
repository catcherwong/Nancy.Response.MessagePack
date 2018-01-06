namespace Nancy.Responses
{
    using System.IO;

    public class DefaultMessagePackSerializer
    {
        public static void Serialize(Stream stream, object model)
        {
            MessagePack.MessagePackSerializer.Serialize(stream, model, MessagePack.Resolvers.ContractlessStandardResolver.Instance);
        }

        public static object Deserialize(Stream stream)
        {
            return MessagePack.MessagePackSerializer.Deserialize<object>(stream, MessagePack.Resolvers.ContractlessStandardResolver.Instance);
        }

        public static T Deserialize<T>(Stream stream)
        {
            return MessagePack.MessagePackSerializer.Deserialize<T>(stream, MessagePack.Resolvers.ContractlessStandardResolver.Instance);
        }
    }
}
