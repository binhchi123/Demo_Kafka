using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerDatabase.Entities
{
    public class EmployeeReport
    {
        public Guid Id { get; init; }
        public Guid EmployeeId { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }

        public EmployeeReport(Guid id, Guid employeeId, string name, string surname)
        {
            Id = id;
            EmployeeId = employeeId;
            Name = name;
            Surname = surname;
        }
    }
}
