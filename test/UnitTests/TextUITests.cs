using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HealthcareClientSystem;

namespace UnitTests
{
    [TestClass]
    public class TextUITests
    {
        [TestMethod]
        public void TestWriteLine()
        {
            int nRows = 10;
            int nCols = 40;

            TextUI ui = new TextUI(nRows, nCols);

            // The first Writeline should return the cursor position : 2.
            Assert.AreEqual(ui.WriteLine("first line added."), 2);
            Assert.AreEqual(ui.CurrentCursorPosition(), 2);

            return;
        }
    }
}
