using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HealthcareClientSystem.IO;

namespace UnitTests
{
    [TestClass]
    public class InputControllerTests
    {
        [TestMethod]
        public void ReadText_InputStringInValidRange_ReturnsInputString()
        {
            string input = "abcdefg";
            InputController.EnableMock();
            InputController.AddMockInput(input);
            
            Assert.AreEqual(InputController.ReadText(1, 10), input);

            return;
        }

        [TestMethod]
        public void ReadInteger_PositiveIntegerInValidRange_ReturnsInputString()
        {
            string input = "12345";
            InputController.EnableMock();
            InputController.AddMockInput(input);
            
            // Expected 1 - 10 positive digits.
            Assert.AreEqual(InputController.ReadInteger(1, 10, true), Convert.ToInt32(input));

            return;
        }

        [TestMethod]
        public void ReadInteger_NegativeIntegerInValidRange_ReturnsInputString()
        {
            string input = "-12345";
            InputController.EnableMock();
            InputController.AddMockInput(input);

            // Expected 1 - 10 negative digits.
            Assert.AreEqual(InputController.ReadInteger(1, 10, false), Convert.ToInt32(input));

            return;
        }

    }
}
