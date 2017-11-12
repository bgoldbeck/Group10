﻿using System;
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
        private static string ReadLine(bool isDigit, bool isPositive, int lengthMin, int lengthMax)
        {
            if (lengthMin <= 0 || lengthMax <= 0 || lengthMin > lengthMax)
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
                    if (result < 0)
                    {
                        // Number should be positive, but isn't
                        line = "";
                    }
                }
            }

            if (line.Length < lengthMin || line.Length > lengthMax)
            {
                // String input length doesn't match paramater length.
                line = "";
            }

            return line;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static int ReadNumeric(int digitsMin, int digitsMax, bool isPositive, string context = "ID")
        { 
            string id = "";

            while (id == "")
            {
                Console.Write("Enter " + context + ": ");
                id = InputController.ReadLine(true, isPositive, digitsMin, digitsMax);
                if (id == "")
                { 
                    Console.WriteLine("\n\tBad ID! Please try again.\n");
                }
            }
            
            return Convert.ToInt32(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReadText(int lengthMin, int lengthMax, string context = "Text")
        {
            string text = "";

            while (text == "")
            {
                Console.Write("Enter " + context + ": ");
                text = InputController.ReadLine(false, false, lengthMin, lengthMax);
                if (text == "")
                {
                    Console.WriteLine("\n\tBad Text! Please try again.\n");
                }
            }
            
            return text;
        }

 
    }
}
