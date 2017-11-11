using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChocAnServer.Packets
{
    public class ResponsePacket : BasePacket
    {
        private string data;
        private string response;

        /// <summary>
        /// This is the default constructor for the base packet class to set the
        /// fields in the class to a default value. It's also protected so only this
        /// class has access to invoke this constructor.
        /// </summary>
        protected ResponsePacket()
        {
            this.data = null;
            this.response = null;
        }
        /// <summary>
        /// This is the constructor takes in inputs for each data member and sets
        /// them.
        /// </summary>
        /// <param name="newAction"></param>
        /// <param name="newSessionID"></param>
        /// <param name="newData"></param>
        /// <param name="newError"></param>
        public ResponsePacket(string newAction, string newSessionID, 
            string newData, string newResponse):base(newAction,newSessionID)
        {
            this.data = newData;
            this.response = newResponse;
        }
        /// <summary>
        /// This functions returns the array of data stored in this class.
        /// </summary>
        /// <returns></returns>
        public string Data()
        {
            return this.data;
        }
        /// <summary>
        /// This functions returns the error string stored in this class.
        /// </summary>
        /// <returns></returns>
        public string Response()
        {
            return this.response;
        }

        public override string ToString()
        {
            return String.Format(
                "ACTION: {0}\n" +
                "SESSION_ID: {1}\n" +
                "DATA: {2}\n" +
                "RESPONSE: {3}\n",
                Action(), SessionID(), Data(), Response());
        }
    }
}
