using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtestHelperBackend
{
    class RequestsDb : DbContext
    {
        public RequestsDb()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<RequestsDb>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<RequestsDb>());
            //Database.SetInitializer(new RequestsDb());
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Medical> Medicals { get; set; }
    }
}
