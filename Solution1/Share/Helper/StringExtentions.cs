﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Helper
{
    public static class StringExtentions
    {
        public static string GetIdByGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}