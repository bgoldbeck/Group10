//TODO:Insert comment about this class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChocAnServer.Packets
{
    public class InvoicePacket : BasePacket
    {
        private string currentDateTime;
        private string dateServiceProvided;
        private string providerID;
        private string memberID;
        private string serviceCode;
        private string comments;

        /// <summary>
        /// This is the default constructor for the base packet class to set the
        /// fields in the class to a default value. It's also protected so only this
        /// class has access to invoke this constructor.
        /// </summary>
        protected InvoicePacket()
        {
            this.currentDateTime = null;
            this.dateServiceProvided = null;
            this.providerID = null;
            this.memberID = null;
            this.serviceCode = null;
            this.comments = null;
        }

        /// <summary>
        /// This is the constructor takes in inputs for each data member and sets
        /// them.
        /// </summary>
        /// <param name="newAction"></param>
        /// <param name="newSessionID"></param>
        /// <param name="newCurrentDateTime"></param>
        /// <param name="newDateServiceProvided"></param>
        /// <param name="newProviderID"></param>
        /// <param name="newMemberID"></param>
        /// <param name="newServiceCode"></param>
        /// <param name="newComments"></param>
        public InvoicePacket(string newAction, string newSessionID,
            string newCurrentDateTime, string newDateServiceProvided,
            string newProviderID, string newMemberID, string newServiceCode,
            string newComments)
            :base(newAction,newSessionID)
        {
            base.CheckInt(newProviderID, 100000000, 999999999);
            base.CheckInt(newMemberID, 100000000, 999999999);
            base.CheckInt(newServiceCode, 100000, 999999);
            this.currentDateTime = newCurrentDateTime ??
                throw new NullReferenceException("Current Date");
            this.dateServiceProvided = newDateServiceProvided ??
                throw new NullReferenceException("Service Date");
            this.providerID = newProviderID ??
                throw new NullReferenceException("Provider ID");
            this.memberID = newMemberID ??
                throw new NullReferenceException("Member ID");
            this.serviceCode = newServiceCode ??
                throw new NullReferenceException("Service Code");
            this.comments = newComments ??
                throw new NullReferenceException("Comments");
        }

        /// <summary>
        /// This method returns the string stored in the currentDateTime data member.
        /// </summary>
        /// <returns></returns>
        public string CurrentDateTime()
        {
            return this.currentDateTime;
        }
        /// <summary>
        /// This method returns the string stored in the dateServiceProvided data member.
        /// </summary>
        /// <returns></returns>
        public string DateServiceProvided()
        {
            return this.dateServiceProvided;
        }
        /// <summary>
        /// This method returns the integer stored in the providerID data member.
        /// </summary>
        /// <returns></returns>
        public string ProviderID()
        {
            return this.providerID;
        }
        /// <summary>
        /// This method returns the integer stored in the memberID data member.
        /// </summary>
        /// <returns></returns>
        public string MemberID()
        {
            return this.memberID;
        }
        /// <summary>
        /// This method returns the integer stored in the serviceCode data member.
        /// </summary>
        /// <returns></returns>
        public string ServiceCode()
        {
            return this.serviceCode;
        }
        /// <summary>
        /// This method returns the string stored in the comments data member.
        /// </summary>
        /// <returns></returns>
        public string Comments()
        {
            return this.comments;
        }
    }
}
