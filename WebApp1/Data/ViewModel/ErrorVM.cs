using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WebApp1.Data.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class ErrorVM
    {
        public string CorelationalId { get; set; }
        public IList<Errors> Errors { set; get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }

    [ExcludeFromCodeCoverage]
    public class Errors
    {
        public string Source { get; set; }
        public string Description { get; set; }

        public Errors() { }
        public Errors(string key, string errorMessage)
        {
            this.Source = key;
            this.Description = errorMessage;
        }

    }
}
