using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace Repositories
{
    public class VisitorsRepository : BaseRepository<Visitor>
    {
        public VisitorsRepository(HotelsEntities1 context)
            : base(context)
        {
        }
        public override void Save(Visitor visitor)
        {
            if (visitor.ID == 0)
            {
                base.Create(visitor);
            }
            else
            {
                base.Update(visitor, item => item.ID == visitor.ID);
            }
        }
    }
}
