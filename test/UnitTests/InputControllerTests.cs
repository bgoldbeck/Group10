using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HealthcareClientSystem.IO;

namespace UnitTests
{
    [TestClass]
    public class InputControllerTests
    {
        [TestMethod]
        public void ReadText_InputStringGreaterThanLengthMax_ReturnsEmptyString()
        {
            InputController.EnableMock();
            InputController.AddMockInput("abcdefg");

            // Simulating the user typing in 7 characters, 
            // when valid characters are only between 1 - 5 characters.
            Assert.AreEqual(InputController.ReadText(1, 5), "");

            return;
        }

    }
}
