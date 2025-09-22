using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ErrorToReturn
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; } = default!;
        // Optional list of detailed errors, useful for validation errors [bad request] 
        public List<string>? Errors { get; set; }
    }
}
