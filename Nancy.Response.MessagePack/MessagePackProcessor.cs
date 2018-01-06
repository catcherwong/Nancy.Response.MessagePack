namespace Nancy.Responses
{
    using Nancy.Responses.Negotiation;
    using System;
    using System.Collections.Generic;

    public class MessagePackProcessor : IResponseProcessor
    {         
        public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
        {
            get
            {
                return new[] { new Tuple<string, MediaRange>("x-msgpack", new MediaRange(ConstValue.MessagePackContentType)) }; ;
            }
        }

        public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {           
            if (IsWildcardXMsgPackContentType(requestedMediaRange) || requestedMediaRange.Matches(ConstValue.MessagePackContentType))
            {
                return new ProcessorMatch
                {
                    ModelResult = MatchResult.DontCare,
                    RequestedContentTypeResult = MatchResult.ExactMatch
                };
            }

            return new ProcessorMatch
            {
                ModelResult = MatchResult.DontCare,
                RequestedContentTypeResult = MatchResult.NoMatch
            };
        }

        private bool IsWildcardXMsgPackContentType(MediaRange requestedContentType)
        {
            if (!requestedContentType.Type.IsWildcard && !string.Equals("application", requestedContentType.Type, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (requestedContentType.Subtype.IsWildcard)
            {
                return true;
            }

            var subtypeString = requestedContentType.Subtype.ToString();

            return (subtypeString.StartsWith("vnd", StringComparison.InvariantCultureIgnoreCase) 
                    && subtypeString.EndsWith("+x-msgpack", StringComparison.InvariantCultureIgnoreCase));
        }
             

        public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {            
            return new MessagePackResponse(model);
        }
    }
}
