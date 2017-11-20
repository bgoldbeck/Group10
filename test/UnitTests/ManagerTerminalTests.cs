using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthcareClientSystem;

namespace UnitTests
{
    [TestClass]
    public class ManagerTerminalTests
    {
        [TestMethod]
        public void ManagerTerminal_Constructor_Tests()
        {
            ManagerTerminal managerTerminal = new ManagerTerminal();
            Assert.IsNotNull(managerTerminal);
            return;
        }

        [TestMethod]
        public void ManagerTerminal_AccessLevel_Tests()
        {
            ManagerTerminal managerTerminal = new ManagerTerminal();
            Assert.AreEqual(1, managerTerminal.AccessLevel());
            return;
        }
    }
}
