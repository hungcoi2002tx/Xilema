using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DTO
{
    public class RoomDTO
    {
        public string RoomId { get; set; } = null!;

        public int NumberRows { get; set; }

        public int NumberCols { get; set; }
        public string? Name { get; set; }
    }
}
