using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthcareClientSystem;
using HealthcareClientSystem.IO;


namespace UnitTests
{
    [TestClass]
    public class TextUITests
    {
        [TestMethod]
        public TextUI BuildTextUIInstance(int nRows, int nCols)
        {
            return new TextUI(nRows, nCols);
        }

        [TestMethod]
        public void TextUI_DefaultConstructor()
        {
            TextUI textUI = new TextUI();
            Assert.IsNotNull(textUI);
        }

        [TestMethod]
        public void TestWriteLine()
        {
            TextUI ui = BuildTextUIInstance(40, 40);

            // The first Writeline should return the cursor position : 2.
            Assert.AreEqual(ui.WriteLine("first line added."), 2);

            return;
        }

        [TestMethod]
        public void TestCurrentCursorPosition()
        {
            TextUI ui = BuildTextUIInstance(40, 40);

            // Cursor line position initially is 1. (The top index is the ### header)
            for (int i = 1; i < ui.MaximumCursorPosition(); ++i)
            {
                ui.WriteLine("write a line added.");

                // Check each time we write a line, the cursor position gets incremented.
                Assert.AreEqual(ui.CurrentCursorPosition(), i + 1);
            }

            ui.WriteLine("write another line added.");

            // By now we should have overflowed the TextUI cursor line index. We check to make
            // sure that happened.
            Assert.AreEqual(ui.CurrentCursorPosition(), 1);

            return;
        }

    }
}
