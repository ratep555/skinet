using System;

namespace API.Errors
{
    public class ApiResponse
    {
        //we may not have message that is attached to the response
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            //we will either use default message or create our own
            // ?? nullcoalescing, if this is null, execute what is on the right to that
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        
        //every response is going to have at least these two properties
        public int StatusCode { get; set; }
        public string Message { get; set; }

        //our wn messages
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
           //this switch is available from c# 8, ne moraš više koristiti break itd...
           //null is the default case if no other matches
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger.  Anger leads to hate.  Hate leads to career change",
                _ => null
            };
        }
    }
}