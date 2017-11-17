/*The BasePacket class is the base class of all the other packets and uses polymorphism
to pass in the base packet into the ChocAnServer class. This class contains two 
data members: a string for the name of the action this packet and a string for the
sessionID of the user. This class also contains the check functions to check that the
inputs in the strings is legal.
*/
using System;

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
        protected void CheckInt(string inputString, int lower, int upper)
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
        /// <summary>
        /// Checks whether an inputted string is a legal date.
        /// </summary>
        /// <param name="inputString"></param>
        public void CheckDate(string inputString)
        {
            System.DateTime temp;
            if(!System.DateTime.TryParse(inputString,out temp))
            {
                throw new ArgumentException("Date");
            }
        }
    }
}
