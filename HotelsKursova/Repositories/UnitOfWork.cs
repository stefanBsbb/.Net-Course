using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace Repositories
{
    public class UnitOfWork //: IDisposable
    {
        private HotelsEntities1 context = new HotelsEntities1();
        private HotelsRepository hotelRepository;
        private EmployeesRepository employeesRepository;
        private VisitorsRepository visitorsRepository;
        //private UserRepository userRepository;

        public HotelsRepository HotelsRepository
        {
            get
            {
                if (this.hotelRepository == null)
                {
                    this.hotelRepository = new HotelsRepository(context);
                }
                return hotelRepository;
            }
        }

        public EmployeesRepository EmployeesRepository
        {
            get
            {
                if (this.employeesRepository == null)
                {
                    this.employeesRepository = new EmployeesRepository(context);
                }
                return employeesRepository;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public VisitorsRepository VisitorsRepository
        {
            get
            {
                if (this.visitorsRepository == null)
                {
                    this.visitorsRepository = new VisitorsRepository(context);
                }
                return visitorsRepository;
            }
        }
        //public UserRepository UserRepository
        //{
        //    get
        //    {
        //        if (this.userRepository == null)
        //        {
        //            this.userRepository = new UserRepository(context);
        //        }
        //        return userRepository;
        //    }
        //}
    }
}
