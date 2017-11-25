/*The ResponsePacket class is the packet that is passed back from the ChocAnServer
 * class which contains the response to the terminal. This class is derived from the BasePacket. 
 * The data members in this class are: the string any data being returned and a string
 * for the response.
 */
using System;


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
            string newData, string newResponse) : base(newAction , newSessionID)
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
        /// This function returns the error string stored in this class.
        /// </summary>
        /// <returns></returns>
        public string Response()
        {
            return this.response;
        }
        /// <summary>
        /// This function override the ToString function to take the data stored in
        /// this class and outputs it as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format(
                "\tACTION: {0}\n" +
                "\tSESSION_ID: {1}\n" +
                "\tDATA: {2}\n" +
                "\tRESPONSE: {3}\n",
                Action(), SessionID(), Data(), Response());
        }
    }
}
