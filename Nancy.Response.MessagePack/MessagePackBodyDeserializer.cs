namespace Nancy.Responses
{
    using System.IO;
    using Nancy.ModelBinding;
    using Nancy.Responses.Negotiation;
    
    public class MessagePackBodyDeserializer : IBodyDeserializer
    {        
        public bool CanDeserialize(MediaRange mediaRange, BindingContext context)
        {
            if(string.IsNullOrWhiteSpace(mediaRange))
            {
                return false;
            }

            if(mediaRange.Type.Matches(ConstValue.MessagePackContentType))
            {
                return true;
            }

            return false;
        }

        public object Deserialize(MediaRange mediaRange, Stream bodyStream, BindingContext context)
        {
            return DefaultMessagePackSerializer.Deserialize(bodyStream);
        }
    }
}
