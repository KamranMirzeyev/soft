using AutoMapper;
using Core.Model;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using SoftAPI.DTO.UserDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            if (ModelState.IsValid)
            {
                User user = _mapper.Map<CreateUserDto, User>(createUserDto);
                int id = await _userService.Create(user);
                if (id != 0)
                    return Created("", id);
            }
            
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult>Edit(EditUserDto editUserDto)
        {
            if (ModelState.IsValid)
            {
                User user = _mapper.Map<EditUserDto, User>(editUserDto);
                bool isok = await _userService.Update(user);

                if (isok)
                    return Ok();
                return BadRequest("Error");
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            if (id>0)
            {
                User user = await _userService.GetUser(id);
                var userdto = _mapper.Map<User, UserDto>(user);
                return Ok(userdto);
            }
            return BadRequest("id greate than 0");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                bool isok = await _userService.Delete(id);
                if (isok)
                    return NoContent();
                return BadRequest("error");
            }
            return BadRequest("id greate than 0");
        }

        [HttpGet]
        [Route("AllList")]
        public async Task<IActionResult>GetAll(int page=1,int count=10,string key=null)
        {

            int modelcount = await _userService.GetAllUserCount(key);
            

            var users = await _userService.GetAllUser(page, count, key);
            var userdto = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);

            var result = new ResultUserDto
            {
                Users = userdto,
                TotalCount = modelcount
            };
            return Ok(result);

        }


        [HttpGet]
        [Route("Task")]
        public IActionResult Task()
        {
            int[] arr = { -1, 0, 1, 2, -1, -4 };
            List<int[]> vs = new List<int[]>();

            int c = 0;
         
            for (int i = 0; i <arr.Length; i++)
            {

                
                c += arr[i];
                if (c==0)
                {
                    
                    int[] ars = { arr[i], arr[i - 1], arr[i - 2] };
                    vs.Add(ars);
                       
                    

                   
                }

               
                
            }

            return Ok(vs);
        }
    }
}
