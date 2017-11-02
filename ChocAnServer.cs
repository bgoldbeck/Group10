using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class ChocAnServer
{
    private DatabaseCenter database;

    public ChocAnServer()
    {
        database = new DatabaseCenter();
        database.Initialize();
    }

    ~ChocAnServer()
    {
    }
}


