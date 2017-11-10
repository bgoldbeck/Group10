using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            this.id = newID;
            this.status = newStatus;
            this.name = newName;
            this.address = newAddress;
            this.city = newCity;
            this.state = newState;
            this.zip = newZip;
            this.email = newEmail;
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
