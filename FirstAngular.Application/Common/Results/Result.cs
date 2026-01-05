using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Common.Results
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }   
        public string? Error { get; set; }

        public static Result<T> Ok(T data, string message = "") => new() { Success = true, Data = data, Message = message };
        public static Result<T> Fail(string error) => new() { Success = false, Error = error };
    }

}
