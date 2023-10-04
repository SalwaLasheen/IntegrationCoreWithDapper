using AutoMapper;
using DataAccessLayer.Application.Mapping;
using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Application.Dtos
{
    public record class EmployeeInfo:IMapFrom<Employee>
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitle { get; set; }
        public decimal? Salary { get; set; }
        public string PhoneNumber { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeInfo>().ReverseMap()
                .ForPath(x => x.Department.DepartmentName, opt => opt.MapFrom(src => src.DepartmentName))
                .ForPath(x => x.Job.JobTitle, opt => opt.MapFrom(src => src.JobTitle))
                .ForPath(x => x.Job.Salary, opt => opt.MapFrom(src => src.Salary));
        }
                

        }
    }


