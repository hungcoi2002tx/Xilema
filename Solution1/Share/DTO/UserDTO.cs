using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DTO
{
    public class UserDTO
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Account { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
