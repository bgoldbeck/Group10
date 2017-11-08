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
        private int providerID;
        private int memberID;
        private int serviceCode;
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
            this.providerID = 0;
            this.memberID = 0;
            this.serviceCode = 0;
            this.comments = null;
        }

        /// <summary>
        /// This is the constructor takes in inputs for each data member and sets
        /// them.
        /// </summary>
        /// <param name="newCurrentDateTime"></param>
        /// <param name="newDateServiceProvided"></param>
        /// <param name="newProviderID"></param>
        /// <param name="newMemberID"></param>
        /// <param name="newServiceCode"></param>
        /// <param name="newComments"></param>
        public InvoicePacket(string newCurrentDateTime, string newDateServiceProvided,
            int newProviderID, int newMemberID, int newServiceCode, string newComments)
        {
            this.currentDateTime = newCurrentDateTime;
            this.dateServiceProvided = newDateServiceProvided;
            this.providerID = newProviderID;
            this.memberID = newMemberID;
            this.serviceCode = newServiceCode;
            this.comments = newComments;
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
        public int ProviderID()
        {
            return this.providerID;
        }
        /// <summary>
        /// This method returns the integer stored in the memberID data member.
        /// </summary>
        /// <returns></returns>
        public int MemberID()
        {
            return this.memberID;
        }
        /// <summary>
        /// This method returns the integer stored in the serviceCode data member.
        /// </summary>
        /// <returns></returns>
        public int ServiceCode()
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
