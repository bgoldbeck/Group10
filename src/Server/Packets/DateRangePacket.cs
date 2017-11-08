//This comment should talk about this class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChocAnServer.Packets
{
    public class DateRangePacket : BasePacket
    {
        private string dateStart;
        private string dateEnd;
        int id;

        /// <summary>
        /// This is the default constructor for the base packet class to set the
        /// fields in the class to a default value. It's also protected so only this
        /// class has access to invoke this constructor.
        /// </summary>
        protected DateRangePacket()
        {
            this.dateStart = null;
            this.dateEnd = null;
            this.id = 0;
        }

        /// <summary>
        /// This is the constructor takes in inputs for each data member and sets
        /// them.
        /// </summary>
        /// <param name="newDateStart"></param>
        /// <param name="newDateEnd"></param>
        /// <param name="newID"></param>
        public DateRangePacket(string newDateStart, string newDateEnd, int newID)
        {
            this.dateStart = newDateStart;
            this.dateEnd = newDateEnd;
            this.id = newID;
        }

        /// <summary>
        /// This method returns the string stored in the dateStart data member.
        /// </summary>
        /// <returns></returns>
        public string DateStart()
        {
            return this.dateStart;
        }

        /// <summary>
        /// This method returns the string stored in the dateEnd data member.
        /// </summary>
        /// <returns></returns>
        public string DateEnd()
        {
            return this.dateEnd;
        }

        /// <summary>
        /// This method returns the string stored in the id data member.
        /// </summary>
        /// <returns></returns>
        public int ID()
        {
            return this.id;
        }
    }
}
