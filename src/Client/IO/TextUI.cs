using System;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace HealthcareClientSystem.IO
{
    public class TextUI
    {
        public enum TextUIJustify { LEFT, CENTER, RIGHT, COUNT };
        private int nCols;
        private int nRows;
        private int cursorLinePosition;
        private string[] outputBuffer;
        private string header;
        private string footer;
        private Boolean isFake;

        public TextUI(Boolean isFake = false)
        {
            this.isFake = isFake;
            header = " TextUI ";
            footer = "";
            Resize(80, 25);
            ClearBuffer();
        }

        /// <summary>
        /// This is my summary for the public constructor.
        /// </summary>
        /// <param name="nRows"></param>
        /// <param name="nCols"></param>
        public TextUI(int nRows, int nCols, Boolean isFake = false)
        {
            this.isFake = isFake;
            header = " TextUI ";
            footer = "";
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
            if (isFake)
                return;

            // Reset cursor position to the top.
            cursorLinePosition = 1;

            for (int i = 0; i < nRows; ++i)
            {
                outputBuffer[i] = " ";
            }

            // Fill the entire top row with #'s
            FillRow(0, '#');

            if (header != null)
            { 
                // Replace the header text.
                StringBuilder topRow = new StringBuilder(outputBuffer[0]);
                topRow.Remove(0, header.Length);
                topRow.Insert((int)(outputBuffer[0].Length * 0.5f) - (int)(header.Length * 0.5f), header);

                outputBuffer[0] = topRow.ToString();
            }

            if (footer != null)
                { 
                // Fill the entire bottom row with #'s
                FillRow(nRows - 1, '#');

                // Replace the header text.
                StringBuilder bottomRow = new StringBuilder(outputBuffer[nRows - 1]);
                bottomRow.Remove(0, footer.Length);
                bottomRow.Insert((int)(outputBuffer[nRows-1].Length * 0.5f) - (int)(footer.Length * 0.5f), footer);

                outputBuffer[nRows - 1] = bottomRow.ToString();
            }

            // Fill the left side of the UI with #'s
            FillColumn(0, '#');

            // Fill the right side of the UI with #'s
            FillColumn(nCols - 1, '#');

            return;
        }

        /// <summary>
        /// Draw everything from the buffer to the console.
        /// </summary>
        public void Render()
        {
            if (isFake)
                return;

            Console.Clear();
            for (int i = 0; i < nRows; ++i)
            {
                //Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(outputBuffer[i]);
            }
            
            // Clear the contents in the buffer.
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
            // Just exit on null or empty string.
            if (output == null || output == "")
            {
                return CurrentCursorPosition();
            }

            if (isFake)
            {
                Debug.WriteLine(output);
                return 1;
            }

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
                    j = 0;
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
                // Overflow, reset cursor to index 0 + 1.
                cursorLinePosition = 1;
            }
            return CurrentCursorPosition();
        }

        /// <summary>
        /// Write a string array to the buffer, breaking it up into groups.
        /// The user will need to cycle through the list.
        /// </summary>
        /// <param name="outputs">
        /// An array of strings to output as groups to the screen frame.
        /// </param>
        /// <param name="groupSize">
        /// The number of strings to print on each screen frame.
        /// </param>
        public void WriteList(string[] outputs, int groupSize = 10)
        {
            if (outputs == null)
            {
                // Throw exception.
                throw new NullReferenceException("String array 's' was null");
            }

            // Do this after exception cases.
            if (isFake)
                outputs.ToList().ForEach(x => Debug.WriteLine(x));

            // Correct any bad group sizes.
            if (groupSize <= 0)
            {
                // Auto-correct the stupid user.
                groupSize = 2;
            }
            int numerator = outputs.Length;

            int denominator = groupSize; // n things at a time.
            int quotient = numerator / denominator;
            int remainder = numerator % denominator;

            // Loop through and display all groups.
            for (int i = 0; i < quotient; ++i)
            {
                for (int j = 0; j < denominator; ++j)
                {
                    this.WriteLine(outputs[i * denominator + j]);
                }
                this.WriteLine("Please type 'any' key for next grouping.", TextUIJustify.CENTER);
                this.Render();
                if (!isFake)
                {
                    InputController.PressAnyKey();
                }
                this.ClearBuffer();
            }
            // Display the last group.
            if (remainder > 0)
            { 
                for (int i = 0; i < remainder; ++i)
                {
                    this.WriteLine(outputs[denominator * quotient + i]);
                }
                this.Render();
            }
            if (!isFake)
            {
                InputController.PressAnyKey();
            }
            return;
        }

        /// <summary>
        /// Write a specified character to a single row in the buffer.
        /// </summary>
        /// <param name="row">
        /// The row in the screen space to render the characters to.
        /// </param>
        /// <param name="ch">
        /// The character to render to the output terminal screen space.
        /// </param>
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

        /// <summary>
        /// Wipes the window clean to blank slate. (Does NOT clear buffer).
        /// Then writes the buffered output to the console once again.
        /// </summary>
        public void Refresh()
        {
            if (isFake)
                return;

            Console.Clear();
            for (int i = 0; i < nRows; ++i)
            {
                Console.WriteLine(outputBuffer[i]);
            }

            return;
        }

        public string Footer
        {
            get { return footer; }
            set { footer = value; }
        }

        public string Header
        {
            get { return header; }
            set { header = value; }
        }
    }
}
