using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    class InvalidInputException : Exception
    {
        public InvalidInputException()
        {
            Console.WriteLine("Invalid input character");
        }

    }
}
