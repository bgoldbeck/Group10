using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChocAnServer.Packets
{
    public class ResponsePacket : BasePacket
    {
        private object[][] data;
        private string error;

        /// <summary>
        /// This is the default constructor for the base packet class to set the
        /// fields in the class to a default value. It's also protected so only this
        /// class has access to invoke this constructor.
        /// </summary>
        protected ResponsePacket()
        {
            this.data = null;
            this.error = null;
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
            object[][] newData, string newError):base(newAction,newSessionID)
        {
            this.data = newData;
            this.error = newError;
        }
        /// <summary>
        /// This functions returns the array of data stored in this class.
        /// </summary>
        /// <returns></returns>
        public object[][] Data()
        {
            return this.data;
        }
        /// <summary>
        /// This functions returns the error string stored in this class.
        /// </summary>
        /// <returns></returns>
        public string Error()
        {
            return this.error;
        }
    }
}
