using System.Diagnostics.CodeAnalysis;

namespace WebApp1.Data.Common
{
    [ExcludeFromCodeCoverage]
    public static class HeaderKey
    {
        public static string CorrelationId => "X-Correlation-ID";

    }
}
