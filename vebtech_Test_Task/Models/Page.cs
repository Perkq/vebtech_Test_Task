using vebtech_Test_Task.DTO;

namespace vebtech_Test_Task.Models
{
    public class PageInformation
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; } 
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }

        public PageInformation(int pageNumber, int pageSize, int totalItems)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalItems = totalItems;
        }
    }

    public class PageUsers
    {
        public ICollection<UserDTO> Users { get; set; }
        public PageInformation PageInfo { get; set; }
        public PageUsers(ICollection<UserDTO> users, PageInformation pageInfo)
        {
            this.Users = users;
            this.PageInfo = pageInfo;
        }

    }


}
