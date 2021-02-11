using System.Collections.Generic;

namespace SoftAPI.DTO.UserDto
{
    public class ResultUserDto
    {
        public IEnumerable<UserDto> Users;
        public int TotalCount { get; set; }
    }
}
