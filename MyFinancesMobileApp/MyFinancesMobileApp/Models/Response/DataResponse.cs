using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinancesMobileApp.Models.Response
{
    public class DataResponse<T> : Response
    {

        public T Data { get; set; }


    }
}
