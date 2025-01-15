using xpanse.sdk.Models;

namespace xpanse.sdk.Helpers
{
    public static class ErrorCodeTypeTranslator
    {
        private const string TypeBaseUrl = "https://docs.xpanse.one/errorcodes#";
        
        public static string GetErrorCodeUrlByCode(ErrorCodeType code)
        {
            return TypeBaseUrl + (int)code;
        }
    }
}