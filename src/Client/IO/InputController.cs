using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareClientSystem.IO
{
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
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static int ReadInteger(int digitsMin, int digitsMax, bool isPositive, string context = "ID")
        { 
            string num = "";

            // We can't allow for less than 1 digit.
            if (digitsMin < 1)
                digitsMin = 1;

            while (num == "")
            {
                Console.Write("Enter " + context + ": ");
                num = InputController.ReadLine(true, isPositive);
                if (num == "" || num.Length < digitsMin || num.Length > digitsMax)
                { 
                    Console.WriteLine("\n\tBad " + context + "!" + " Please try again.\n");
                }
            }
            return Convert.ToInt32(num);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReadText(int lengthMin, int lengthMax, string context = "Text")
        {
            if (lengthMin < 0)
            {
                lengthMin = 1;
            }

            if (lengthMin > lengthMax)
            {
                lengthMin = lengthMax;
            }

            string text = "";

            while (text == "")
            {
                Console.Write("Enter " + context + ": ");
                text = InputController.ReadLine(false, false);
                if ((text == "" && lengthMin > 0) || 
                    text.Length < lengthMin || text.Length > lengthMax)
                {
                    text = "";
                    Console.WriteLine("\n\tBad " + context + "!" + " Please try again.\n");
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
