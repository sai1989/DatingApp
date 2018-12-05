using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public static class Extensions
    {
        public static void AddAplicationError ( this HttpResponse response,string errorMessage)
        {
            response.Headers.Add("Application-Error",errorMessage);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }
    }
}