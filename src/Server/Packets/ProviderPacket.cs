using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChocAnServer.Packets
{
    public class ProviderPacket : MemberPacket
    {
        private string password;

        /// <summary>
        /// This is the default constructor for the base packet class to set the
        /// fields in the class to a default value. It's also protected so only this
        /// class has access to invoke this constructor.
        /// </summary>
        protected ProviderPacket()
        {
            this.password = null;
        }
        /// <summary>
        /// This is the constructor takes in inputs for each data member and sets
        /// them.
        /// </summary>
        /// <param name="newAction"></param>
        /// <param name="newSessionID"></param>
        /// <param name="newID"></param>
        /// <param name="newStatus"></param>
        /// <param name="newName"></param>
        /// <param name="newAddress"></param>
        /// <param name="newCity"></param>
        /// <param name="newState"></param>
        /// <param name="newZip"></param>
        /// <param name="newEmail"></param>
        /// <param name="newPassword"></param>
        public ProviderPacket(string newAction, string newSessionID, string newID, string newStatus, string newName,
           string newAddress, string newCity, string newState, string newZip,
           string newEmail, string newPassword) :base(newAction,newSessionID, newID,
               newStatus, newName, newAddress, newCity, newState, newZip, newEmail)
        {
            this.password = newPassword ??
                throw new NullReferenceException("Password");
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
