using System;
using System.Collections.Generic;

namespace HealthcareClientSystem.IO
{
    /// <summary>
    /// This class handles input from the user and is used as a static class throughout the
    /// program. Its job is to make sure the use types in specific input matching certain 
    /// parameters.
    /// </summary>
    public class InputController
    {
        /// <summary>
        /// Mock variables
        /// </summary>
        private static Boolean MockInput = false;
        private static Queue<String> MockInputQueue = new Queue<string>();

        /// <summary>
        /// Places the InputController in mock mode
        /// This does not take any input from the user
        /// </summary>
        public static void EnableMock()
        {
            MockInput = true;
            MockInputQueue.Clear();
        }

        /// <summary>
        /// Takes the InputController out of mock mode
        /// This switches it back to accepting input from the user
        /// </summary>
        public static void DisableMock()
        {
            MockInput = false;
            MockInputQueue.Clear();
        }

        /// <summary>
        /// Adds input to be mocked if the program needs input of any kind
        /// Note that this is added in a queue
        /// The queue is cleared whenever Mock is enabled/disabled
        /// </summary>
        /// <param name="input">user input to queue</param>
        public static void AddMockInput(string input)
        {
            MockInputQueue.Enqueue(input);
        }

        /// <summary>
        /// Simulates pressing any key. This helps prevent
        /// tests from continuing forever in unit tests.
        /// </summary>
        public static void PressAnyKey()
        {
            if (MockInput)
                return;
            Console.ReadKey();
            return;
        }

        /// <summary>
        /// This function reads a line input from the user and takes steps to ensure
        /// the string follows the parameters. If the string input does not follow the
        /// parameters guidelines, the function will return an empty string "".
        /// </summary>
        /// <param name="isDigit"></param>
        /// <param name="isPositive"></param>
        /// <param name="length"></param>
        /// <returns>
        /// The string the user typed or "" if the user typed the wrong parameters.
        /// </returns>
        private static string ReadLine(bool isDigit, bool isPositive)
        {
     
            // Get the input from the user.
            string line = "";
            if (MockInput)
            {
                if (MockInputQueue.Count > 0)
                    line = MockInputQueue.Dequeue();
            }
            else
            {
                line = Console.ReadLine();
            }

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

            return line;
        }

        /// <summary>
        /// Reads an integer value from the user. Takes steps to ensure the length and positive 
        /// nature of the integer are correct according to the parameters as they are set.
        /// </summary>
        /// <param name="digitsMin">
        /// Minimum # of digits the integer can be.
        /// </param>
        /// <param name="digitsMax">
        /// Maximum # of digits the integer can be.
        /// </param>
        /// <param name="isPositive">
        /// Should the number be positive?
        /// </param>
        /// <param name="context">
        /// Context of the input, so the user knows what they should type.
        /// </param>
        /// <returns>
        /// The integer in the specified parameters range.
        /// </returns>
        public static int ReadInteger(int digitsMin, int digitsMax, bool isPositive, string context = "ID")
        { 
            string num = "";

            // We can't allow for less than 1 digit.
            if (digitsMin < 1)
            {
                digitsMin = 1;
            }

            while (num == "")
            {
                Console.Write("Enter " + context + ": ");
                // Get the input from the user.
                num = InputController.ReadLine(true, isPositive);

                // Ensure the input was good.
                if (num == "" || num.Length < digitsMin || num.Length > digitsMax)
                { 
                    Console.WriteLine("\n\tBad " + context + "!" + " Please try again.\n");
                    // Number was bad, reset the input.
                    num = "";
                }
            }
            return Convert.ToInt32(num);
        }

        /// <summary>
        /// Gets input string from the user of a string that matches the parameters set.
        /// </summary>
        /// <param name="lengthMin">
        /// Minimum length of the string.
        /// </param>
        /// <param name="lengthMax">
        /// Maximum length of the string.
        /// </param>
        /// <param name="context">
        /// Context of the input, so the user knows what to type.
        /// </param>
        /// <returns>
        /// The input within the specified range of the parameters.
        /// </returns>
        public static string ReadText(int lengthMin, int lengthMax, string context = "Text")
        {
            // Strings cant be negative in length.
            if (lengthMin < 0)
            {
                lengthMin = 1;
            }

            // The minimum length should not be greater than the maximum.
            if (lengthMin > lengthMax)
            {
                lengthMin = lengthMax;
            }

            string text = "";

            while (text == "")
            {
                Console.Write("Enter " + context + ": ");

                // Get the user input.
                text = InputController.ReadLine(false, false);

                // Ensure the input is in the range of the parameters.
                if ((text == "" && lengthMin > 0) ||
                    text.Length < lengthMin || text.Length > lengthMax)
                {
                    Console.WriteLine("\n\tBad " + context + "!" + " Please try again.\n");
                    // On bad input, reset the input.
                    text = "";
                }
                else
                {
                    break;
                }
            }
            
            return text;
        }

 
    }
}
