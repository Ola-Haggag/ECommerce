using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Common
{
    public record Error(string code,string Description,ErrorType Type = ErrorType.Failure )
    {
        public static Error Failure(string code = "General.Failure", string Description = "General failure description")
            => new Error(code, Description, ErrorType.Failure);

        public static Error vValidation(string code = "General.Validation", string Description = "General Validation description")
            => new Error(code, Description, ErrorType.Validation);

        public static Error NotFound(string code = "General.NotFound", string Description = "General NotFound description")
            => new Error(code, Description, ErrorType.NotFound);

        public static Error Conflict(string code = "General.Conflict", string Description = "General Conflict description")
            => new Error(code, Description, ErrorType.Conflict);

        public static Error UnAuthorized(string code = "General.UnAuthorized", string Description = "General UnAuthorized description")
            => new Error(code, Description, ErrorType.UnAuthorized);

        public static Error Forbidden(string code = "General.Forbidden", string Description = "General Forbidden description")
            => new Error(code, Description, ErrorType.Forbidden);

        public static Error InvalidCredentails(string code = "General.InvalidCredentails", string Description = "General InvalidCredentails description")
            => new Error(code, Description, ErrorType.InvalidCredentails);
    }
}
