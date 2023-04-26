using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace WebApp1.Data.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class FileVM
    {
        public string filepath { get; set; }

        public string _batchGuid { get; set; }
    }
}
