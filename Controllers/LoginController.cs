using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserServiceHub.Models;

namespace UserServiceHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public DatabaseContext _DatabaseContext;

        public LoginController(DatabaseContext db)
        {
            _DatabaseContext = db;
        }

        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
      
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Route("PostMethod")]
        [HttpPost]
        public string PostMethod(Login id)
        {
            return "value";
        }

        [Route("userlist")]
        public object userlist()
        {          
            return   _DatabaseContext.UserModel.ToList();
        }

        // For User Login
        [Route("userlogin")]
        [HttpPost]
        public Response userlogin([FromBody]Login Lg)
        {
            var obj = _DatabaseContext.UserModel.Where(t => t.UserName == Lg.UserName & t.Passwords == Lg.Password).ToList();
            
                var status = obj.Select(l => l.Status).SingleOrDefault();
            if (obj.Count == 0)
            {
                return new Response { Status = "Invalid", Message = "Invalid User." };
            }
            if (obj.Count == -1)
            {
                return new Response { Status = "Inactive", Message = "User Inactive." };
            }
            else
            {
                var id = _DatabaseContext.UserModel.Where(t => t.UserName == Lg.UserName && t.Passwords==Lg.Password).Select(t=>t.UserId).SingleOrDefault();
                var update = _DatabaseContext.UserModel.Where(t => t.UserId == id).SingleOrDefault();

                try
                {

                    update.IsApporved = true;
                    update.Status = true;

                    _DatabaseContext.Update(update);
                    _DatabaseContext.SaveChanges();

                    Lg.userdata = _DatabaseContext.UserModel.Where(t => t.UserId == id).ToArray();

                    return new Response { Status = "Success", Message = "Successfully Login!" };
                }
                catch(Exception ex)
                {
                    throw ex;
                }

                    return new Response { Status = "Success", Message = Lg.UserName };
            }

        }


        // For New User Registration
       [Route("createcontact")]
        [HttpPost]
        public object createcontact([FromBody]Login lg)
        {
            // CREATING THE OBJECT OF DMO FOR ASSIGN INCOMING DATA.
            EmployeemasterDMO table = new EmployeemasterDMO();
            try
            {
                table.UserName = lg.UserName;
                table.LoginName = lg.LoginName;
                table.Password = lg.Password;
                table.Email = lg.Email;
                table.ContactNo = lg.ContactNo;
                table.Address = lg.Address;
                table.IsApporved = 0;
                table.Status = 0;
                table.TotalCnt = 0;

                _DatabaseContext.Add(table);
                _DatabaseContext.SaveChanges();
                return new Response { Status = "Success", Message = "Successfully Saved!" };

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Response { Status = "Error", Message = "Invalid Data" };
        }


        // For User LogOut
        [Route("userlogout")]
        [HttpPost]
        public Response userlogout([FromBody] Login Lg)
        {
            
            //CHECKING USER LOGOUT 
            try
            {
                var id = _DatabaseContext.UserModel.Where(t => t.UserName == Lg.UserName && t.Passwords == Lg.Password).Select(t => t.UserId).SingleOrDefault();
                var update = _DatabaseContext.UserModel.Where(t => t.UserId == id).SingleOrDefault();

                update.IsApporved = false;
                update.Status = false;

                _DatabaseContext.Update(update);
                _DatabaseContext.SaveChanges();
                Lg.userdata = _DatabaseContext.UserModel.Where(t => t.UserId == id).ToArray();
                return new Response { Status = "Success", Message = "Successfully Logout!" };
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new Response { Status = "Success", Message = Lg.UserName };

        }


    }
}
