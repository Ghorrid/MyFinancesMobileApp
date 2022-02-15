using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinancesMobileApp.Models.Response
{
    public class Error
    {
        public Error(string source, string message)
        {
            Source = source;
            Message = message;
        }

        public string Source { get; set; }
        public string Message { get; set; }
    }
}
