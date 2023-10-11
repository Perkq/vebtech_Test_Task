using vebtech_Test_Task.Enums;

namespace vebtech_Test_Task.DTO
{
    public class RoleDTO
    {
        public RoleEnum RoleId { get; set; }

        public RoleDTO(RoleEnum roleId)
        {
            RoleId = roleId;
        }
    }
}
