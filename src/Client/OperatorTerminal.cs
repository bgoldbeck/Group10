using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareClientSystem
{
    class OperatorTerminal
    {
        protected TextUI tui;

        protected enum TerminalState { LOGIN, MENU, VIEW_PROVIDER_DIRECTORY, COUNT };

        protected TerminalState currentState;

        protected delegate bool UpdateDelegate();

        protected UpdateDelegate[] updateDelegates;

        protected int columnSize;
        protected int rowSize;

        public OperatorTerminal()
        {
            columnSize = 100;
            rowSize = 40;

            Console.SetWindowSize(columnSize + 1, rowSize + 2);

            tui = new TextUI(rowSize, columnSize);

            currentState = TerminalState.LOGIN;

            // Setup the array of updateDelegates.
            updateDelegates = new UpdateDelegate[(int)TerminalState.COUNT];

            // Set each updateDelegate.
            updateDelegates[(int)TerminalState.LOGIN] = LoginUpdate;
            updateDelegates[(int)TerminalState.MENU] = MenuUpdate;

        }

        public void Loop()
        {
            //string userInput = Console.ReadLine();
            bool running = true;
            while (running)
            {
                if (tui != null)
                {
                    tui.ClearBuffer();
                    if (updateDelegates[(int)currentState] != null)
                    {
                        running = updateDelegates[(int)currentState]();
                    }
                }

            }
            return;
        }

        private bool LoginUpdate()
        {
            if (tui == null)
            {
                // Exception.
            }
            tui.WriteLine("Terminal [Login]", TextUI.TextUIJustify.CENTER);
            //tui.WriteLine("        " + (frame++).ToString());
            tui.WriteLine("");
            tui.WriteLine("");
            tui.WriteLine("");
            tui.WriteLine("    Login ");

            tui.WriteLine("");
            tui.WriteLine("");
            tui.WriteLine("Please \ntype \nanything \nto \ncontinue", TextUI.TextUIJustify.CENTER);

            tui.Render();
            //tui.ClearBuffer();

            int userInput = Console.Read();
            tui.WriteLine(userInput.ToString(), TextUI.TextUIJustify.CENTER);
            // Handle state transition here?
            currentState = TerminalState.MENU;

            return true;
        }



        protected virtual bool MenuUpdate()
        {
            return true;
        }

    }
}
