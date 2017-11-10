using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChocAnServer.Packets
{
    public class ServiceCodePacket : BasePacket
    {
        private float fee;
        private string id;
        private string name;

        /// <summary>
        /// This is the default constructor for the base packet class to set the
        /// fields in the class to a default value. It's also protected so only this
        /// class has access to invoke this constructor.
        /// </summary>
        protected ServiceCodePacket()
        {
            this.fee = 0;
            this.id = null;
            this.name = null;
        }
        /// <summary>
        /// This is the constructor takes in inputs for each data member and sets
        /// them.
        /// </summary>
        /// <param name="newAction"></param>
        /// <param name="newSessionID"></param>
        /// <param name="newFee"></param>
        /// <param name="newID"></param>
        /// <param name="newName"></param>
        public ServiceCodePacket(string newAction, string newSessionID, float newFee,
            string newID, string newName) :base(newAction,newSessionID)
        {
            if(newFee < 0)
            {
                throw new ArgumentOutOfRangeException("Fee is negative");
            }
            base.CheckInt(newID,99999,1000000);
            this.fee = newFee;
            this.id = newID ??
                throw new NullReferenceException("ID");
            this.name = newName ??
                throw new NullReferenceException("Name");
        }
        /// <summary>
        /// This method returns the string stored in the fee data member.
        /// </summary>
        /// <returns></returns>
        public float Fee()
        {
            return this.fee;
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
        /// This method returns the string stored in the name data member.
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return this.name;
        }
    }
}
