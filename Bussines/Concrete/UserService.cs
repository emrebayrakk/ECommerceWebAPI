using System;
using Bussines.Abstract;
using DataAccess.Abstract;
using Entities.Dtos.UserDtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Bussines.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IEnumerable<UserDetailDto>> GetListAsync()
        {
            List<UserDetailDto> userDetailDtos = new List<UserDetailDto>();
            var response = await _userDal.GetListAsync();
            foreach (var item in response.ToList())
            {
                userDetailDtos.Add(new UserDetailDto()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Gender = item.Gender == true ? "Erkek" : "Kadın",
                    DateOfBirth = item.DateOfBirth,
                    Address = item.Address,
                    Email = item.Email,
                    Id = item.Id,


                });
            }

            return userDetailDtos;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(x => x.Id == id);
            UserDto userDto = new UserDto()
            {
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Id = user.Id,
                UserName = user.UserName,
            };
            return userDto;
        }

        public async Task<UserDto> AddAsync(UserAddDto userAddDto)
        {
            User user = new User()
            {
                FirstName = userAddDto.FirstName,
                LastName = userAddDto.LastName,
                Address = userAddDto.Address,
                Email = userAddDto.Email,
                Gender = userAddDto.Gender,
                UserName = userAddDto.UserName,
                //Todo: CreatedDate ve CreatedUserId düzenlenecek.
                CreatedDate = DateTime.Now,
                CreatedUserId = 1,
                DateOfBirth = userAddDto.DateOfBirth,
                Password = userAddDto.Password,
            };

            var userAdd = await _userDal.AddAsync(user);
            UserDto userDto = new UserDto()
            {
                FirstName = userAdd.FirstName,
                LastName = userAdd.LastName,
                Address = userAdd.Address,
                Email = userAdd.Email,
                Gender = userAdd.Gender,
                UserName = userAdd.UserName,
                DateOfBirth = userAdd.DateOfBirth,
                Id = userAdd.Id,


            };
            return userDto;
        }

        public async Task<UserUpdateDto> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var getUser = await _userDal.GetAsync(x => x.Id == userUpdateDto.Id);
            User user = new User()
            {
                FirstName = userUpdateDto.FirstName,
                LastName = userUpdateDto.LastName,
                Address = userUpdateDto.Address,
                Email = userUpdateDto.Email,
                Gender = userUpdateDto.Gender,
                UserName = userUpdateDto.UserName,
                DateOfBirth = userUpdateDto.DateOfBirth,
                Id = userUpdateDto.Id,
                Password = userUpdateDto.Password,
                CreatedDate = getUser.CreatedDate,
                CreatedUserId = getUser.CreatedUserId,
                UpdatedDate = DateTime.Now,
                UpdatedUserId = 1,
            };
            var userUpdate = await _userDal.UpdateAsync(user);
            UserUpdateDto newUserUpdateDto = new UserUpdateDto()
            {
                FirstName = userUpdate.FirstName,
                LastName = userUpdate.LastName,
                Address = userUpdate.Address,
                Email = userUpdate.Email,
                Gender = userUpdate.Gender,
                UserName = userUpdate.UserName,
                DateOfBirth = userUpdate.DateOfBirth,
                Id = userUpdate.Id,
                Password = userUpdate.Password,

            };
            return newUserUpdateDto;

        }

        public async Task<bool> DeleteAsync(int id)
        {

            return await _userDal.DeleteAsync(id);

        }
    }
}
