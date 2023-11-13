using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Exceptions
{
    public class PasswordChangeFailedException : Exception
    {
        public PasswordChangeFailedException() : base("Unknown error occured when updating password.")
        {
        }

        public PasswordChangeFailedException(string? message) : base(message)
        {
        }

        protected PasswordChangeFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
