using Core.DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class EfEmployeeDal : EfEntityRepositoryBase<Employee,EmployeeContext> , IEmployeeDal
    {
    }
}
