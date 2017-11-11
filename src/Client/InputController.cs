using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareClientSystem
{
    public class InputController
    {
        /// <summary>
        /// This function reads a line input from the user and takes steps to ensure
        /// the string follows the parameters. If the string input does not follow the
        /// parameters guidelines, the function will return an empty string "".
        /// </summary>
        /// <param name="isDigit"></param>
        /// <param name="isPositive"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReadLine(bool isDigit, bool isPositive, int length)
        {
            if (length < 0)
            {
                return "";
            }

            // Get the input from the user.
            string line = Console.ReadLine();

            if (isDigit == true)
            {
                bool isActuallyNumber = int.TryParse(line, out int result);

                if (!isActuallyNumber)
                {
                    // A number was expected, but not provided.
                    line = "";
                }

                // Number should be positive.
                if (isPositive)
                {
                    if (result <= 0)
                    {
                        // Number should be positive, but isn't
                        line = "";
                    }
                }
            }

            if (line.Length != length)
            {
                // String input length doesn't match paramater length.
                line = "";
            }

            return line;
        }





    }
}
