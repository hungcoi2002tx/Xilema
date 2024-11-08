using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share
{
    public enum RoleEnum
    {
        Admin = 0,
        Staff = 1
    }

    public enum TimeStart
    {
        Morning_8AM = 1,
        Morning_10AM = 2,
        Afternoon_1PM = 3,
        Afternoon_3PM = 4,
        Evening_5PM = 5,
        Evening_7PM = 6,
        Night_9PM = 7,
        Night_11PM = 8,
        Late_Night_1AM = 9
    }

    public enum ShowStaus
    {
        Available = 0,
        SoldOut = 1,
    }
}
