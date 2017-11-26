using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthcareClientSystem;
using HealthcareClientSystem.IO;

namespace UnitTests
{
    [TestClass]
    public class ManagerTerminalTests
    {
        private String SampleID = "123456789";
        private String SampleCode = "123456";
        private String SampleDate = "12-12-2016";
        private String SampleName = "SampleMember";
        private String SampleAddress = "12345 Address";
        private String SampleCity = "Sample City";
        private String SampleState = "OR";
        private String SampleZip = "12345";
        private String SampleEmail = "12345@1234.com";
        public string SamplePassword = "password";
        private string SampleFee = "45.67";

        [TestMethod]
        public void ManagerTerminal_Constructor_Tests()
        {
            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            Assert.IsNotNull(managerTerminal);
            return;
        }

        [TestMethod]
        public void ManagerTerminal_AccessLevel_Tests()
        {
            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            Assert.AreEqual(1, managerTerminal.AccessLevel());
            return;
        }

        [TestMethod]
        public void ManagerTerminal_AddMemberUpdate()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput("N");
            InputController.AddMockInput(SampleName);
            InputController.AddMockInput(SampleAddress);
            InputController.AddMockInput(SampleCity);
            InputController.AddMockInput(SampleState);
            InputController.AddMockInput(SampleZip);
            InputController.AddMockInput(SampleEmail);

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.ADD_MEMBER);

            InputController.DisableMock();
        }

        [TestMethod]
        public void ManagerTerminal_AddProviderUpdate()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput("N");
            InputController.AddMockInput(SampleName);
            InputController.AddMockInput(SampleAddress);
            InputController.AddMockInput(SampleCity);
            InputController.AddMockInput(SampleState);
            InputController.AddMockInput(SampleZip);
            InputController.AddMockInput(SampleEmail);
            InputController.AddMockInput(SamplePassword);

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.ADD_PROVIDER);

            InputController.DisableMock();
        }

        [TestMethod]
        public void ManagerTerminal_AddServiceCodeUpdate()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleCode);
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput(SampleName);
            InputController.AddMockInput(SampleFee);

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.ADD_SERVICE_CODE);

            InputController.DisableMock();
        }

        [TestMethod]
        public void ManagerTerminal_RemoveMemberUpdate()
        {
            InputController.EnableMock();
            InputController.AddMockInput("123456780");

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.REMOVE_MEMBER);

            InputController.DisableMock();
        }

        [TestMethod]
        public void ManagerTerminal_RemoveProviderUpdate()
        {
            InputController.EnableMock();
            InputController.AddMockInput("123456780");

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.REMOVE_PROVIDER);

            InputController.DisableMock();
        }

        [TestMethod]
        public void ManagerTerminal_CustomMemberReport()
        {
            InputController.EnableMock();
            InputController.AddMockInput("100000001");

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.CUSTOM_MEMBER_REPORT);

            InputController.DisableMock();
        }

        [TestMethod]
        public void ManagerTerminal_CustomProviderReport()
        {
            InputController.EnableMock();
            InputController.AddMockInput("100000001");

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.CUSTOM_PROVIDER_REPORT);

            InputController.DisableMock();
        }

        [TestMethod]
        public void ManagerTerminal_MenuUpdateFunction()
        {
            for (int i = 0; i <= 26; i++)
            {
                char input = Convert.ToChar(97 + i);
                if (i == 26)
                    input = '9';

                InputController.EnableMock();
                InputController.AddMockInput(input.ToString());

                ManagerTerminal managerTerminal = new ManagerTerminal(true);
                managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.MENU);

                InputController.DisableMock();
            }
        }

        [TestMethod]
        public void ManagerTerminal_AddServiceRecord()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleDate);
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput(SampleCode);
            InputController.AddMockInput("comments");

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.ADD_SERVICE_RECORD);

            InputController.DisableMock();
        }

        [TestMethod]
        public void ManagerTerminal_UpdateMemberUpdate()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput("N");
            InputController.AddMockInput(SampleName);
            InputController.AddMockInput(SampleAddress);
            InputController.AddMockInput(SampleCity);
            InputController.AddMockInput(SampleState);
            InputController.AddMockInput(SampleZip);
            InputController.AddMockInput(SampleEmail);

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.UPDATE_MEMBER);

            InputController.DisableMock();
        }

        [TestMethod]
        public void ManagerTerminal_UpdateProviderUpdate()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput("N");
            InputController.AddMockInput(SampleName);
            InputController.AddMockInput(SampleAddress);
            InputController.AddMockInput(SampleCity);
            InputController.AddMockInput(SampleState);
            InputController.AddMockInput(SampleZip);
            InputController.AddMockInput(SampleEmail);
            InputController.AddMockInput(SamplePassword);

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.UPDATE_PROVIDER);

            InputController.DisableMock();
        }

        [TestMethod]
        public void ManagerTerminal_UpdateServiceCodeUpdate()
        {
            InputController.EnableMock();
            InputController.AddMockInput(SampleCode);
            InputController.AddMockInput(SampleID);
            InputController.AddMockInput(SampleName);
            InputController.AddMockInput(SampleFee);

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.UPDATE_SERVICE_CODE);

            InputController.DisableMock();
        }

        [TestMethod]
        public void ManagerTerminal_MainAccountProcedure()
        {
            InputController.EnableMock();

            ManagerTerminal managerTerminal = new ManagerTerminal(true);
            managerTerminal._RunUpdateDelegateOnce(OperatorTerminal.TerminalState.MAIN_ACCOUNTING_PROCEDURE);

            InputController.DisableMock();
        }
    }
}
