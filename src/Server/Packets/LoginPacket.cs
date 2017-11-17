/*The LoginPacket class is the packet that is passed in to pass the information
 * in to allow the member and provider to login and get a new session. This class is
 * derived from the BasePacket. The data members in this class are: the string for
 * the id, the string for the password, and the integer for the access level.
 */
using System;


namespace ChocAnServer.Packets
{
    public class LoginPacket : BasePacket
    {
        private string id;
        private string password;
        private int accessLevel;
        /// <summary>
        /// This is the default constructor for the base packet class to set the
        /// fields in the class to a default value. It's also protected so only this
        /// class has access to invoke this constructor.
        /// </summary>
        protected LoginPacket()
        {
            this.id = null;
            this.password = null;
            this.accessLevel = -1;
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
            string newID, string newPassword, int newLevel) 
            :base(newAction,newSessionID)
        {
            base.CheckInt(newID, 100000000, 999999999);
            this.id = newID ??
                throw new NullReferenceException("ID");
            this.password = newPassword ??
                throw new NullReferenceException("Password");
            this.accessLevel = newLevel;
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
        public int AccessLevel()
        {
            return this.accessLevel;
        }
    }
}
