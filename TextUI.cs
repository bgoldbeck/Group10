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
    private string[] rows;

    public TextUI()
    {
        nCols = 80;
        nRows = 20;
        cursorLinePosition = 1;
        rows = new string[nRows];
        ClearBuffer();
    }

    public TextUI(int row, int col)
    {
        nCols = col;
        nRows = row;
        cursorLinePosition = 1;
        rows = new string[nRows];
        ClearBuffer();
    }

    ~TextUI()
    {
    }

    public void ClearBuffer()
    {
        // Reset cursor position to the top.
        cursorLinePosition = 1;

        rows[0] = "";
        for (int i = 0; i < nCols; ++i)
        {
            rows[0] += "#";
        }

        for (int i = 1; i < nRows - 1; ++i)
        {
            rows[i] = "#";
            for (int j = 1; j < nCols - 1; ++j)
            {
                rows[i] += " ";
            }
            rows[i] += "#";
        }

        rows[nRows - 1] = "";
        for (int i = 0; i < nCols; ++i)
        {
            rows[nRows - 1] += "#";
        }
        return;
    }

    public void Render()
    {
        Console.Clear();
        for (int i = 0; i < nRows; ++i)
        {
            //Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(rows[i]);
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

        rows[cursorLinePosition] = "#";

        // Any justification must be accounted for after the string is output.
        int j = 0;

        switch (justify)
        {
            case TextUIJustify.CENTER:
                int q = (int)((nCols - 2) * .5);
                int r = (int)(output.Length * .5);
                for (int i = 0; i < (q - r); ++i)
                {
                    rows[cursorLinePosition] += " ";
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

        rows[cursorLinePosition] += output;
   
        for (int i = 0; i < (nCols - output.Length - 2 - j); ++i)
        {
            rows[cursorLinePosition] += " ";
        }

        rows[cursorLinePosition++] += "#";
            
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

