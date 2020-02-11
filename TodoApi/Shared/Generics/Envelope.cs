using System.Linq;
using System;
using TodoApi.Shared.APIResponse;
namespace TodoApi.Shared.Generics
{
   public class Envelope<T>
    {


        public bool Success { get; set; }
        public string key { get; set; }
        public string Response { get; set; }        
        public T Data { get; set; }
        public string ExceptionMessage { get; set; }
        public string ErrorType { get; set; }
        public Envelope(bool success, string key)
        {
            this.Success = success;
            this.key = key;
            string response;          
            ResponseMessage.Get(key, out response);
            this.Response = response;
           
            if (success && !ResponseMessage.SuccessKeys.Contains(key))
            {
                this.Success = false;
            }
        }
        public Envelope(bool success, string key, T data) : this(success, key)
        {
            this.Data = data;
        }
        public Envelope(bool success, string key, Exception exception) : this(success, key)
        {
            this.ExceptionMessage = exception.Message;
            //log exception

        }

        public Envelope()
        {
        }

        public Envelope(bool success, T data)
        {
            this.Success = success;
            this.Data = data;
        }

    }
}