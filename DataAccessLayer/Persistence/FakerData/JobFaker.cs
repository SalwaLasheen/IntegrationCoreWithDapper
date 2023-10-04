using Bogus;
using DataAccessLayer.Models;

namespace DataAccessLayer.Persistence.FakerData
{
    public class JobFaker : Faker<Job>
    {

        public  List<Job> GenerateFakeJobs(int count)
        {
            var id = 3;
            Random random = new ();
            List<Job> jobs = new ();
            var fakerJobs = new Faker<Job>()
                 .RuleFor(c => c.JobId, _ => id++)
               .RuleFor(a => a.JobTitle, f => Enum.GetName(f.PickRandom<JobType>()))
               .RuleFor(a => a.Salary, f => random.Next(1000, 5000));

            jobs.AddRange(fakerJobs.Generate(count));
            return jobs;

        }

    }

    public enum JobType
    {
        Software,
        Finance,
        Accountant,
        Engnieering,
        HelpDisk,
        CustomerService,
        CallCenter,
        Sales,
        Marketing
    }

}