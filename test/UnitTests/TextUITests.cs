using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthcareClientSystem.IO;


namespace UnitTests
{
    [TestClass]
    public class TextUITests
    {
        // Convention: MethodName_StateUnderTest_ExpectedBehavior
        
        [TestMethod]
        public void TextUI_DefaultConstructor()
        {
            TextUI textUI = new TextUI();
            Assert.IsNotNull(textUI);
        }

        [TestMethod]
        public void TextUI_HeaderFooterTests()
        {
            TextUI textUI = new TextUI();
            textUI.Header = "header";
            Assert.AreEqual("header", textUI.Header);
            textUI.Footer = "footer";
            Assert.AreEqual("footer", textUI.Footer);
            return;
        }

        [TestMethod]
        public void WriteList_ValidList_NotNull()
        { 
            TextUI textUI = new TextUI(true);
            textUI.WriteList(new string[] { "a", "b" });
            Assert.IsNotNull(textUI);
            return;
        }

        [TestMethod]
        public void WriteList_NullList_ExceptionThrown()
        {
            // Arrange.
            TextUI textUI = new TextUI(true);
            string[] sArr = null;

            // Act, Assert
            Assert.ThrowsException<NullReferenceException>( () => textUI.WriteList(sArr) );
            return;
        }

        [TestMethod]
        public void WriteLine_ValidString_IncreaseCursorPosition()
        {
            // Arrange.
            TextUI ui = new TextUI(40, 40);

            int startCursorPosition = ui.CurrentCursorPosition();
            
            // Act.
            int currentCursorPosition = ui.WriteLine("valid string");

            // Assert. The first Writeline should return the cursor position + 1.
            Assert.AreEqual(startCursorPosition + 1, currentCursorPosition);

            return;
        }

        [TestMethod]
        public void WriteLine_OverflowScreenSpace_ResetCursorPosition()
        {
            TextUI ui = new TextUI(40, 40);

            // Cursor line position initially is 1. (The top index is the ### header)
            for (int i = 1; i < ui.MaximumCursorPosition(); ++i)
            {
                ui.WriteLine("write a line added.");

                // Check each time we write a line, the cursor position gets incremented.
                Assert.AreEqual(ui.CurrentCursorPosition(), i + 1);
            }

            ui.WriteLine("write one more line.");

            // By now we should have overflowed the TextUI cursor line index. We check to make
            // sure that happened.
            Assert.AreEqual(ui.CurrentCursorPosition(), 1);

            return;
        }

        [TestMethod]
        public void WriteLine_ValidFooter_IncreaseCursorPosition()
        {
            // Arrange.
            TextUI ui = new TextUI(40, 40);
            ui.Footer = "Test Footer";

            int startCursorPosition = ui.CurrentCursorPosition();

            // Act.
            int currentCursorPosition = ui.WriteLine("valid string");

            // Assert. The first Writeline should return the cursor position + 1.
            Assert.AreEqual(startCursorPosition + 1, currentCursorPosition);
        }

        [TestMethod]
        public void WriteLine_ValidHeader_IncreaseCursorPosition()
        {
            // Arrange.
            TextUI ui = new TextUI(40, 40);
            ui.Header = "Test Header";

            int startCursorPosition = ui.CurrentCursorPosition();

            // Act.
            int currentCursorPosition = ui.WriteLine("valid string");

            // Assert. The first Writeline should return the cursor position + 1.
            Assert.AreEqual(startCursorPosition + 1, currentCursorPosition);
        }
    }
}
