using AutoMapper;
using Data.Models;
using System.Runtime.CompilerServices;

namespace Logic.Models.Mapper
{
    public class StudentsChallengeMapper: Profile
    {
        public StudentsChallengeMapper()
        {
            this.CreateMap<Student, StudentDto>()
                .ReverseMap();
            this.CreateMap<Class, ClassDto>().ForMember(item => item.Students, opt => opt.Ignore()).ReverseMap();
        }
    }
}
