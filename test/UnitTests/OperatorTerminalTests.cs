using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HealthcareClientSystem;
using HealthcareClientSystem.IO;
using ChocAnServer;
using ChocAnServer.Packets;

namespace UnitTests
{
    [TestClass]
    public class OperatorTerminalTests
    {
        [TestMethod]
        public void OperatorTerminal_CreateInstance_InstanceIsNotNull()
        {

            OperatorTerminal.EnableMock();
            OperatorTerminal operatorTerminal = new OperatorTerminal(true);
            Assert.IsNotNull(operatorTerminal);
            OperatorTerminal.DisableMock();
            return;
        }

        [TestMethod]
        public void LoginUpdate_GoodUserAndPass_ResponseSuccess()
        {
            OperatorTerminal.EnableMock();
            OperatorTerminal.ChangeMockState(OperatorTerminal.TerminalState.LOGIN);
            OperatorTerminal.ChangeMockPacket(new LoginPacket("LOGIN", "", "123456789", "asdf", 1));

            OperatorTerminal operatorTerminal = new OperatorTerminal(true);
            operatorTerminal.Loop();

            // I set the MockPacket to a response pack when mock mode is enabled.
            ResponsePacket response = OperatorTerminal.MockPacket as ResponsePacket;


            Assert.AreEqual(response.Response(), "Login Successful");
            OperatorTerminal.DisableMock();
            return;
        }

        [TestMethod]
        public void OperatorTerminal_IsLoggedIn_Valid()
        {
            OperatorTerminal operatorTerminal = new OperatorTerminal(true);
            Assert.IsFalse(operatorTerminal.IsLoggedIn());
        }

        [TestMethod]
        public void OperatorTerminal_AccessLevel_Valid()
        {
            OperatorTerminal operatorTerminal = new OperatorTerminal(true);
            Assert.AreEqual(-1, operatorTerminal.AccessLevel());
        }
    }
}
