using Nancy.Configuration;
using System;
using System.IO;

namespace Nancy.Responses
{
    public class MessagePackResponse: Response
    {
        public MessagePackResponse(object model)
        {
            this.Contents = stream => DefaultMessagePackSerializer.Serialize(stream,model);
            this.ContentType = ConstValue.MessagePackContentType;
            this.StatusCode = HttpStatusCode.OK;
        }

        public MessagePackResponse WithStatusCode(HttpStatusCode httpStatusCode)
        {
            this.StatusCode = httpStatusCode;
            return this;
        }
    }
}
