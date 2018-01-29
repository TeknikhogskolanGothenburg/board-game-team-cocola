using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public static class  StringController
    {
        public static bool IsEmpty(string StringValue)
        {
            return string.IsNullOrWhiteSpace(StringValue) || string.IsNullOrEmpty(StringValue);
        }

        }
} //
