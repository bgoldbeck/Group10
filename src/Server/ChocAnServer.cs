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

    }
}


