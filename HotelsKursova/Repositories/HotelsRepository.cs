using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace Repositories
{   
    public class HotelsRepository : BaseRepository<Hotel>
    {
        public HotelsRepository(HotelsEntities1 context)
            : base(context)
        {
        }

        public override void Save(Hotel hotel)
        {
            if (hotel.HotelID == 0)
            {
                base.Create(hotel);
            }
            else
            {
                base.Update(hotel, item => item.HotelID == hotel.HotelID);
            }
        }
    }
}
