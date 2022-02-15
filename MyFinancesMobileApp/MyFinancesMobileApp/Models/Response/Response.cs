using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFinancesMobileApp.Models.Response
{
    public class Response
    {
        public Response()
        {
            Errors = new List<Error>();
        }

        public List<Error> Errors { get; set; }
        public bool IsSuccess
        {
            get { return Errors == null || !Errors.Any(); }
        }

        // => Errors != null || !Errors.Any();

    }
}
