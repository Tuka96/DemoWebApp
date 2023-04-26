using System;
using System.Net;

namespace WebApp1.Data.Exceptions
{
    public static class ExceptionStatuscode
    {
      
        public static int GetExceptionStatuscode(Type exceptionType) {

            if (exceptionType == typeof(ApplicationException))
            {
                return (int)HttpStatusCode.BadRequest;
            }
            else {
                return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
