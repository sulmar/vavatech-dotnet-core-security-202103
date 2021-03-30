using SqlInjection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlInjection.IServices
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Get(string searchString);
        Employee Get(int id);
        Employee Validate(string username, string password);
        void Add(Employee employee);
    }
}
