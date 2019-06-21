using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace Repositories
{
    public class EmployeesRepository : BaseRepository<Employee>
    {
        public EmployeesRepository(HotelsEntities1 context)
               : base(context)
        {
        }
        public override void Save(Employee employee)
        {
            
            if (employee.ID == 0)
            {
                base.Create(employee);
            }
            else
            {
                base.Update(employee, item => item.ID == employee.ID);
            }
        }
    }
}
