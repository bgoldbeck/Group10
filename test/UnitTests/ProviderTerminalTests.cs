using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthcareClientSystem;
using HealthcareClientSystem.IO;

namespace UnitTests
{
    [TestClass]
    public class ProviderTerminalTests
    {
        /// <summary>
        /// Access level is 0 by default
        /// </summary>
        [TestMethod]
        public void AccessLevel_Tests()
        {
            ProviderTerminal providerTerminal = new ProviderTerminal(true);
            var accessLevel = providerTerminal.AccessLevel();
            Assert.AreEqual(0, accessLevel);
        }
        
        /// <summary>
        /// Run thru possible inputs for this menu
        /// </summary>
        [TestMethod]
        public void MenuUpdate_Tests()
        {
            for (int i = 0; i < 10; i++)
            {
                char input = Convert.ToChar(48 + i);

                InputController.EnableMock();
                InputController.AddMockInput(input.ToString());

                ProviderTerminal managerTerminal = new ProviderTerminal(true);
                managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.MENU);

                InputController.DisableMock();
            }
        }

        /// <summary>
        /// Viewing the provider directory
        /// </summary>
        [TestMethod]
        public void ProviderDirectory_Tests()
        {
            InputController.EnableMock();

            ProviderTerminal terminal = new ProviderTerminal(true);
            terminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.VIEW_PROVIDER_DIRECTORY);

            InputController.DisableMock();
        }

        /// <summary>
        /// Test status of a member
        /// </summary>
        [TestMethod]
        public void MemberStatus_Tests()
        {
            InputController.EnableMock();
            InputController.AddMockInput("100000001");

            ProviderTerminal terminal = new ProviderTerminal(true);
            terminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.CHECK_MEMBER_STATUS);

            InputController.DisableMock();
        }
    }
}
