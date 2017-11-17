/*The MemberPacket class is the packet that is passed in to pass the information
 * in to allow the member's account to be added and updated. This class is
 * derived from the BasePacket. The data members in this class are: the string for
 * the member id, string for the member status, the string for the name, the string
 * for the address, the string for the city, the string for the state, the string for
 * the zip, and a string for the email.
 */
using System;


namespace ChocAnServer.Packets
{
    public class MemberPacket : BasePacket
    {
        private string id;
        private string status;
        private string name;
        private string address;
        private string city;
        private string state;
        private string zip;
        private string email;

        /// <summary>
        /// This is the default constructor for the base packet class to set the
        /// fields in the class to a default value. It's also protected so only this
        /// class has access to invoke this constructor.
        /// </summary>
        protected MemberPacket()
        {
            this.id = null;
            this.status = null;
            this.name = null;
            this.address = null;
            this.city = null;
            this.state = null;
            this.zip = null;
            this.email = null;
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
        public MemberPacket(string newAction, string newSessionID,string newID,string newStatus, string newName,
            string newAddress, string newCity, string newState, string newZip,
            string newEmail) :base(newAction,newSessionID)
        {
            base.CheckInt(newID, 100000000, 999999999);
            this.id = newID ??
                throw new NullReferenceException("ID");
            this.status = newStatus ??
                throw new NullReferenceException("Status");
            this.name = newName ??
                throw new NullReferenceException("Name");
            this.address = newAddress ??
                throw new NullReferenceException("Address");
            this.city = newCity ??
                throw new NullReferenceException("City");
            this.state = newState ??
                throw new NullReferenceException("State");
            this.zip = newZip ??
                throw new NullReferenceException("Zip");
            this.email = newEmail ??
                throw new NullReferenceException("Email");
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
        /// This method returns the string stored in the status data member.
        /// </summary>
        /// <returns></returns>
        public string Status()
        {
            return this.status;
        }
        /// <summary>
        /// This method returns the string stored in the name data member.
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return this.name;
        }
        /// <summary>
        /// This method returns the string stored in the address data member.
        /// </summary>
        /// <returns></returns>
        public string Address()
        {
            return this.address;
        }
        /// <summary>
        /// This method returns the string stored in the city data member.
        /// </summary>
        /// <returns></returns>
        public string City()
        {
            return this.city;
        }
        /// <summary>
        /// This method returns the string stored in the state data member.
        /// </summary>
        /// <returns></returns>
        public string State()
        {
            return this.state;
        }
        /// <summary>
        /// This method returns the string stored in the zip data member.
        /// </summary>
        /// <returns></returns>
        public string Zip()
        {
            return this.zip;
        }
        /// <summary>
        /// This method returns the string stored in the email data member.
        /// </summary>
        /// <returns></returns>
        public string Email()
        {
            return this.email;
        }
    }
}
