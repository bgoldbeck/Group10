using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthcareClientSystem
{ 
    public class TextUI
    {
        public enum TextUIJustify { LEFT, CENTER, RIGHT, COUNT };
        private int nCols;
        private int nRows;
        private int cursorLinePosition;
        private string[] outputBuffer;
        
        public TextUI()
        {
            Resize(80, 25);
            ClearBuffer();
        }

        /// <summary>
        /// This is my summary for the public constructor.
        /// </summary>
        /// <param name="nRows"></param>
        /// <param name="nCols"></param>
        public TextUI(int nRows, int nCols)
        {
            Resize(nRows, nCols);
            ClearBuffer();
        }


        /// <summary>
        /// Returns the index of the current cursor position.
        /// </summary>
        /// <returns>cursorLinePosition</returns>
        public int CurrentCursorPosition()
        {
            return cursorLinePosition;
        }

        /// <summary>
        /// Return the index of the maximum cursor position.
        /// </summary>
        /// <returns></returns>
        public int MaximumCursorPosition()
        {
            return nRows - 1;
        }

        /// <summary>
        /// Resize the TextUI draw window.
        /// </summary>
        /// <param name="nRows"></param>
        /// <param name="nCols"></param>
        public void Resize(int nRows, int nCols)
        {
            this.nCols = nCols;
            this.nRows = nRows;
            this.outputBuffer = new string[nRows];
            ClearBuffer();
            return;
        }

        /// <summary>
        /// Clear the contents of the buffer string table. Reset the 
        /// screen contents with default draw box.
        /// </summary>
        public void ClearBuffer()
        {
            // Reset cursor position to the top.
            cursorLinePosition = 1;

            for (int i = 0; i < nRows; ++i)
            { 
                outputBuffer[i] = " ";
            }

            // Fill the entire top row with #'s
            FillRow(0, '#');

            // Fill the entire bottom row with #'s
            FillRow(nRows - 1, '#');

            // Fill the left side of the UI with #'s
            FillColumn(0, '#');

            // Fill the right side of the UI with #'s
            FillColumn(nCols - 1, '#');
            
            return;
        }

        /// <summary>
        /// Draw everything from the buffer to the console.
        /// </summary>
        public void Render(Boolean clearBuffer = false)
        {
            Console.Clear();
            for (int i = 0; i < nRows; ++i)
            {
                //Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(outputBuffer[i]);
            }

            if(clearBuffer)
                ClearBuffer();

            return;
        }

        /// <summary>
        /// Write output text to the current cursor index position in the buffer.
        /// </summary>
        /// <param name="output"></param>
        /// <param name="justify"></param>
        /// <returns></returns>
        public int WriteLine(string output, TextUIJustify justify = TextUIJustify.LEFT)
        {
            if (output.Length < 1) return CurrentCursorPosition();

            output = output.Replace("\t", "    ");

            string[] split = output.Split('\n');

            if (split.Length > 1)
            { 
                foreach (string sp in split)
                {
                    WriteLine(sp, justify);
                }
                return CurrentCursorPosition();
            }


            if (output.Length > (nCols - 2))
            { 
                    output = output.Substring(0, nCols - 2);
            }

            outputBuffer[cursorLinePosition] = "#";

            // Any justification must be accounted for after the string is output.
            int j = 0;

            switch (justify)
            {
                case TextUIJustify.CENTER:
                    int q = (int)((nCols - 2) * .5);
                    int r = (int)(output.Length * .5);
                    for (int i = 0; i < (q - r); ++i)
                    {
                        outputBuffer[cursorLinePosition] += " ";
                    }
                    // We justified our text.
                    j = q - r;
                    break;
                case TextUIJustify.LEFT:
                    break;
                case TextUIJustify.RIGHT:
                    break;
                default:
                    break;
            }

            outputBuffer[cursorLinePosition] += output;
   
            for (int i = 0; i < (nCols - output.Length - 2 - j); ++i)
            {
                outputBuffer[cursorLinePosition] += " ";
            }

            outputBuffer[cursorLinePosition++] += "#";
            
            if (cursorLinePosition >= nRows)
            {
                // Overflow
            }
            return CurrentCursorPosition();
        }

        /// <summary>
        /// Write a string array to the buffer, breaking it up into groups.
        /// The user will need to cycle through the list.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="groupSize"></param>
        public void WriteList(string[] s, int groupSize = 10)
        {

            int n = s.Length;

            int d = groupSize; // n things at a time.
            int q = n / d;
            int r = n % d;
            
            //C
            for (int i = 0; i < q; ++i)
            {
                for (int j = 0; j < d; ++j)
                {
                    this.WriteLine(s[i * d + j], TextUIJustify.CENTER);
                }
                this.WriteLine("Please type 'n' for next grouping.", TextUIJustify.CENTER);
                this.Render();
                string t = Console.ReadLine();
                this.ClearBuffer();
            }
            this.WriteLine("Remainder.", TextUIJustify.CENTER);
            for (int i = 0; i < r; ++i)
            {
                this.WriteLine(s[d * q + i], TextUIJustify.CENTER);
            }
            this.Render();
            this.ClearBuffer();
            Console.ReadLine();
        }
        
        /// <summary>
        /// Write a specified character to a single row in the buffer.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="ch"></param>
        private void FillRow(int row, char ch)
        {
            if (row < 0 || row > nRows)
            {
                // Exception.
            }
            for (int i = 0; i < this.nCols; ++i)
            { 
                outputBuffer[row] += ch;
            }
            return;
        }

        /// <summary>
        /// Write a specified character to a single col in the buffer.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="ch"></param>
        private void FillColumn(int col, char ch)
        {
            if (col < 0 || col > nCols)
            {
                // Exception.
                throw new IndexOutOfRangeException();
            }
            

            for (int i = 0; i < this.nRows; ++i)
            {
                string row = outputBuffer[i];
                if (row == null)
                {
                    row = " ";
                }

                StringBuilder stringBuilder = new StringBuilder(row);

                if (row.Length < col)
                {
                    stringBuilder.Length = col + 1;
                }

                stringBuilder[col] = '#';

                row = stringBuilder.ToString();
                outputBuffer[i] = row;

            }
            return;
        }
    }
}
