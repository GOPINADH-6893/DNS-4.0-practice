using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;  
            var logPath=Path.Combine(Directory.GetCurrentDirectory(), "logs", "error.log"); 

            if(!Directory.Exists(Path.GetDirectoryName(logPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));
            }   

            var filePath = Path.Combine(logPath, "exceptions.log");
            File.AppendAllText(filePath,
                $"[{DateTime.Now}] {exception.Message}\n{exception.StackTrace}\n\n");

         
            context.Result = new ObjectResult("An unexpected error occurred.")
            {
                StatusCode = 500
            };
        }
    }
}
