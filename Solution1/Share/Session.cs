using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share
{
    public static class Session
    {
        public static string UserId { get; set; }
        public static string Role { get; set; }

        public static void Clear()
        {
            UserId = string.Empty; Role = string.Empty;
        }
    }
}
