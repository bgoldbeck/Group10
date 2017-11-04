using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class TextUI
{
        
    public enum TextUIJustify { LEFT, CENTER, RIGHT, COUNT };
    private int nCols;
    private int nRows;
    private int cursorLinePosition;
    private string[] outputBuffer;

    public TextUI()
    {
        this.nCols = 80;
        this.nRows = 20;
        this.outputBuffer = new string[nRows];
        ClearBuffer();
    }
    /// <summary>
    /// This is my summary for the public constructor.
    /// </summary>
    /// <param name="nRows"></param>
    /// <param name="nCols"></param>
    public TextUI(int nRows, int nCols)
    {
        this.nCols = nCols;
        this.nRows = nRows;
        this.outputBuffer = new string[nRows];
        ClearBuffer();
    }

    ~TextUI()
    {
    }
    /// <summary>
    /// Returns the index of the currentCursorPosition.
    /// </summary>
    /// <returns>cursorLinePosition</returns>
    public int CurrentCursorPosition()
    {
        return cursorLinePosition;
    }

    public int MaximumCursorPosition()
    {
        return nRows;
    }

    public void ClearBuffer()
    {
        // Reset cursor position to the top.
        cursorLinePosition = 1;

        outputBuffer[0] = "";
        for (int i = 0; i < nCols; ++i)
        {
            outputBuffer[0] += "#";
        }

        for (int i = 1; i < nRows - 1; ++i)
        {
            outputBuffer[i] = "#";
            for (int j = 1; j < nCols - 1; ++j)
            {
                outputBuffer[i] += " ";
            }
            outputBuffer[i] += "#";
        }

        outputBuffer[nRows - 1] = "";
        for (int i = 0; i < nCols; ++i)
        {
            outputBuffer[nRows - 1] += "#";
        }
        return;
    }

    public void Render()
    {
        Console.Clear();
        for (int i = 0; i < nRows; ++i)
        {
            //Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(outputBuffer[i]);
        }

        return;
    }

    public void WriteLine(string output, TextUIJustify justify = TextUIJustify.LEFT)
    {
        if (output.Length < 1) return;

        output = output.Replace("\t", "    ");

        string[] split = output.Split('\n');

        if (split.Length > 1)
        { 
            foreach (string sp in split)
            {
                WriteLine(sp, justify);
            }
            return;
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
        return;
    }

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

}

