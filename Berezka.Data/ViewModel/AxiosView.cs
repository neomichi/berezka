using System;
using System.Collections.Generic;
using System.Text;

namespace Berezka.Data.ViewModel
{
    public struct AxiosView<T> where T:class
    {

        public T Value { get; private set; }
        public bool Status { get; private set; }
        public string Message { get; private set; }

        public AxiosView(T value,bool status=true,string message="")
        {
            Value = value;
            Status = status;
            Message = message;
        }

    }  
}
