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
            OperatorTerminal operatorTerminal = new OperatorTerminal();
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

            OperatorTerminal operatorTerminal = new OperatorTerminal();
            operatorTerminal.Loop();

            // I set the MockPacket to a response pack when mock mode is enabled.
            ResponsePacket response = OperatorTerminal.MockPacket as ResponsePacket;


            Assert.AreEqual(response.Response(), "Login Successful");
            OperatorTerminal.DisableMock();
            return;
        }
    }
}
