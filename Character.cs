using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    class Character
    {
        public static bool Compare(char x, char y)
        {
            if (x == y)
                return true;
            else
                return false;
        }

        public static char toLower(char ch)
        {
            if (ch >= 65 && ch <= 90)
                return (Char)(ch + 32);
            else
                return ch;
        }
    }
}
