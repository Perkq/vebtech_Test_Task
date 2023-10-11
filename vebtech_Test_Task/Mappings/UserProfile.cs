using AutoMapper;
using vebtech_Test_Task.DTO;
using vebtech_Test_Task.Models;

namespace vebtech_Test_Task.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ForMember(x => x.Roles, y => y.MapFrom(src => src.Roles));

        }
    }
}
