using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    namespace ECommerce.Application.Interfaces
    {
        public interface IPasswordHasher
        {
            string Hash(string password);
            bool Verify(string rawPassword, string hashedPassword);
        }
    }

}
