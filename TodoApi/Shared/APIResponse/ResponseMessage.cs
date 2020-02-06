using System.Collections.Generic;
namespace TodoApi.Shared.APIResponse
{
    public static class ResponseMessage
    {
         private static Dictionary<string, string> Langs = new Dictionary<string, string>()
        {
            //common messages -2
            { "exception-message","Somthing went wrong" },
            { "validation-error", ""},
            { "data-fetch-success",""},
            { "data-fetch-failure","" },
            { "no-result",""},
            { "application-success",""},
            { "application-failure",""},
        };
        public static string[] SuccessKeys = { "UPDATED", "", "APPROVED", "PROCESSED", "REVERT", "REJECTED", "CANCELLED", "CANCEL_APPROVED", "token-generated", "DELETED" };
        public static void Get(string key, out string response)
        {
            string lang;
            Langs.TryGetValue(key, out lang);
            response = lang ?? string.Empty;           
        }
    }
}