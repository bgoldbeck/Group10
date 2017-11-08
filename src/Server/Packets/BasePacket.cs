/// TODO: Please add a description ;_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChocAnServer.Packets
{
    public class BasePacket
    {
        private string action;
        private string sessionID;

        /// <summary>
        /// This is the default constructor for the base packet class to set the
        /// fields in the class to a default value. It's also protected so only this
        /// class has access to invoke this constructor.
        /// </summary>
        protected BasePacket()
        {
            this.action = null;
            this.sessionID = null;
        }
        /// <summary>
        /// This is the constructor which takes in a string for the action and the
        /// string for the session ID and it sets the fields.
        /// </summary>
        /// <param name="newAction"></param>
        /// <param name="newSessionID"></param>
        public BasePacket(string newAction, string newSessionID)
        {
            this.action = newAction ?? throw new NullReferenceException("Action");
            this.sessionID = newSessionID ?? throw new NullReferenceException
                ("Session ID");
        }
        /// <summary>
        /// This method returns the string stored in the action data member.
        /// </summary>
        /// <returns></returns>
        public string Action()
        {
            return this.action;
        }
        /// <summary>
        /// This method returns the string stored in the session ID data member.
        /// </summary>
        /// <returns></returns>
        public string SessionID()
        {
            return this.sessionID;
        }
        /// <summary>
        /// Checks whether an inputted string is an integer and the right size.
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        protected void CheckInt(string inputString,int lower, int upper)
        {
            int temp;
            if (!int.TryParse(inputString, out temp))
            {
                throw new ArgumentException("ID");
            }
            else if (temp < lower || temp > upper)
            {
                throw new ArgumentOutOfRangeException("ID");
            }
        }
    }
}
