using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos.UserDtos;

namespace Bussines.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserDetailDto>> GetListAsync();

        Task<UserDto> GetByIdAsync(int id);

        Task<UserDto> AddAsync(UserAddDto userAddDto);

        Task<UserUpdateDto> UpdateAsync(UserUpdateDto userUpdateDto);

        Task<bool> DeleteAsync(int id);
    }
}
