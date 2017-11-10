using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChocAnServer.Packets
{
    public class LoginPacket : BasePacket
    {
        private string id;
        private string password;

        /// <summary>
        /// This is the default constructor for the base packet class to set the
        /// fields in the class to a default value. It's also protected so only this
        /// class has access to invoke this constructor.
        /// </summary>
        protected LoginPacket()
        {
            this.id = null;
            this.password = null;
        }

        /// <summary>
        /// This is the constructor takes in inputs for each data member and sets
        /// them.
        /// </summary>
        /// <param name="newAction"></param>
        /// <param name="newSessionID"></param>
        /// <param name="newID"></param>
        /// <param name="newPassword"></param>
        public LoginPacket(string newAction, string newSessionID, 
            string newID, string newPassword) :base(newAction,newSessionID)
        {
            base.CheckInt(newID, 100000000, 999999999);
            this.id = newID ??
                throw new NullReferenceException("ID");
            this.password = newPassword ??
                throw new NullReferenceException("Password");
        }

        /// <summary>
        /// This method returns the string stored in the id data member.
        /// </summary>
        /// <returns></returns>
        public string ID()
        {
            return this.id;
        }
        /// <summary>
        /// This method returns the string stored in the password data member.
        /// </summary>
        /// <returns></returns>
        public string Password()
        {
            return this.password;
        }
    }
}
