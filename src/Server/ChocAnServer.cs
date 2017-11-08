using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChocAnServer.Packets;
using SQLLiteDatabaseCenter;

namespace ChocAnServer
{
    public class ChocAnServer
    {
        DatabaseCenter database;

        public ChocAnServer()
        {
            database = DatabaseCenter.Singelton;
            database.Initialize();
        }

        ~ChocAnServer()
        {
        }

        ResponsePacket ProcessAction(BasePacket basePacket)
        {
            ResponsePacket responsePacket = null;
            switch (basePacket.Action())
            {
                case "AddMember":
                    if (basePacket is MemberPacket)
                    { 
                        responsePacket = RequestAddMember((MemberPacket)basePacket);
                    }
                    else
                    {
                        throw new ArgumentException(String.Format("{0} BasePacket is wrong type ",
                            basePacket.Action()), "basePacket");
                    }
                    break;
                default:
                    break;

            }

            return responsePacket;
        }

        ResponsePacket RequestAddMember(MemberPacket packet)
        {
            ResponsePacket responsePacket = null;
            
            database.ExecuteQuery("INSERT INTO members(memberID, memberName, memberAddress, memberCity, " +
                "memberState, memberZip, memberValid, memberEmail, memberStatus) VALUES( " +
                "123456789, 'Brandon Goldbeck', '1055 NW Gravel Road', 'Hillsboro', 'OR', " +
                "'97124', 1, 'bg@psu.edu', 'ACTIVE'" +
                ");");
            return responsePacket;
        }

    }
}


