using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserServiceHub.Models;

namespace UserServiceHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly DatabaseContext _dbContext;

        public UsersController(DatabaseContext context)
        {
            _dbContext = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GET()
        {
            return await _dbContext.UserModel.ToListAsync();
        }

        // GET
        [HttpGet("GetAllUser")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            return await _dbContext.UserModel.ToListAsync();
        }

        //[HttpGet("GetAllUser")]
        //public ActionResult<userdetails> GetUsers()
        //{
        //    userdetails obj = new userdetails();
        //    obj.userlist = _dbContext.UserModel.ToArray();
        //    return obj;
        //}


        //Post
        [HttpPost("CreateNewUser")] 
        public async Task<ActionResult<IEnumerable<UserModel>>> PostUsers([FromBody]UserModel usermodal)
        {
            _dbContext.UserModel.Add(usermodal);
            await _dbContext.SaveChangesAsync();

            // return CreatedAtAction("GetUsers", new { id = usermodal.User_Id }, usermodal);

            return await _dbContext.UserModel.ToListAsync();
        }

        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> EditRow(int? id)
        {
            // var usermodal = await _dbContext.UserModel.Any(t=>t.User_Id== id);
            UserModel usermodal = _dbContext.UserModel.Find(id);

            if (usermodal == null)
            {
                return NotFound();
            }

            return usermodal;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int? id, UserModel usermodal)
        {
            if (id != usermodal.UserId)
            {
                return BadRequest();
            }

            _dbContext.Entry(usermodal).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> DeleteUsers(int id)
        {
            var users = await _dbContext.UserModel.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _dbContext.UserModel.Remove(users);
            await _dbContext.SaveChangesAsync();

            return users;
        }

        private bool UserExists(int? id)
        {
            return _dbContext.UserModel.Any(e => e.UserId == id);
        }

        //Post
        [HttpPost("SearchUser")]
        public List<UserModel> SearchUser([FromBody] SearchData data)
        {
            List <UserModel> UserModel = new List<UserModel>();

            if (!string.IsNullOrEmpty(data.searchuser))
            {
                UserModel = (from a in _dbContext.UserModel
                        where (a.UserName.Contains(data.searchuser)
                        ) select a).OrderBy(t=>t.UserName).ToList();
            }

            return UserModel;        
        }

        [HttpPost("SearchByEmail")]
        public List<UserModel> SearchByEmail([FromBody] SearchData data)
        {
            List<UserModel> usermodel = new List<UserModel>();

            if (!string.IsNullOrEmpty(data.searcbyemail))
            {
                usermodel = (from a in _dbContext.UserModel
                             where (a.Email.Contains(data.searcbyemail))
                             select a).OrderBy(t=>t.Email).ToList();
            }
            return usermodel;
        }

    }

}

