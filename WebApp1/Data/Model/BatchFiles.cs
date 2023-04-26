using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApp1.Data.Model
{
    [ExcludeFromCodeCoverage]
    public class BatchFiles
    {
        [Key]
        public int Id { get; set; }
        public string filepath { get; set; }
        public string FileName { get; set; }
        public string BatchGuid { get; set; }
    }
}
