using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Learnhub.Domain.Exceptions;
using System.Linq;

namespace LearnHub.Web.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }
        public string? ErrorMessage { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
             Request.Cookies.TryGetValue("ErrorMessage", out string? t);
             Request.Cookies.TryGetValue("StatusCode", out string? StatusCode);
             ErrorMessage = t;

             Response.Cookies.Delete("ErrorMessage");
             Response.Cookies.Delete("StatusCode");


            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}