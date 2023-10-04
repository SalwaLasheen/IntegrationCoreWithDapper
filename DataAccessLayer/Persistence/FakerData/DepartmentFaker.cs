using Bogus;
using DataAccessLayer.Models;

namespace DataAccessLayer.Persistence.FakerData
{
    public class DepartmentFaker : Faker<Department>
    {
        public  List<Department> GenerateFakeDepartments(int count)
        {
            var id = 300;
            List<Department> departments = new ();
            var fakerdepartment = new Faker<Department>()
                 .RuleFor(c => c.DepartmentId, f => ++id)
              .RuleFor(c => c.DepartmentName, f => f.Commerce.Department().Length>30?"no Department": f.Commerce.Department());


            departments.AddRange(fakerdepartment.Generate(count));
            return departments;

        }
    }
}
