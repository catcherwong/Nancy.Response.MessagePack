namespace Nancy.Responses
{    
    public static class FormatterExtensions
    {        
        public static Response AsMessagePack<TModel>(this IResponseFormatter formatter, TModel model)
        {
            return new MessagePackResponse(model);
        }
    }
}
