using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GyrusWebAPI.Models;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace GyrusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMastersController : ControllerBase
    {
        private readonly DemoAPICoreDatabaseContext _context;
        private IConfiguration _config;
           
        public UserMastersController(DemoAPICoreDatabaseContext context, IConfiguration config)
        {
            _context = context;
            _config = config;


        }

        public ActionResult getgitdata()
        {
            return NotFound();
        }

        // GET: api/UserMasters
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<UserMaster>>> GetUserMasters()
        {
            if (_context.UserMasters == null)
            {
                return NotFound();
            }
            return await _context.UserMasters.ToListAsync();
        }

        // GET: api/UserMasters/id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserMaster>> GetUserMaster(int id)
        {
            if (_context.UserMasters == null)
            {
                return NotFound();
            }
            var userMaster = await _context.UserMasters.FindAsync(id);

            if (userMaster == null)
            {
                return NotFound();
            }

            return userMaster;
        }

        // PUT: api/UserMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserMaster(int id, UserMaster userMaster)
        {
            if (id != userMaster.Userid)
            {
                return BadRequest($"URL Id: {id} does not match Userid in request body: {userMaster.Userid}");
            }

            _context.Entry(userMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserMasterExists(id))
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

        //Login api 
        [HttpPost("Login")]
        public async Task<ActionResult<UserMaster>> Login(UserMaster userMaster)
        {
            if (_context.UserMasters == null)
            {
                return NotFound();
            }
            var user = await _context.UserMasters.Where(x => x.Username == userMaster.Username && x.Password == userMaster.Password).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("Invalid Username or Password!! Enter Valid Data");
            }

            return user;
        }


        // POST: api/UserMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserMaster>> PostUserMaster(UserMaster userMaster)
        {
            if (_context.UserMasters == null)
            {
                return Problem("Entity set 'DemoAPICoreDatabaseContext.UserMasters'  is null.");
            }
            _context.UserMasters.Add(userMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserMaster", new { id = userMaster.Userid }, userMaster);
        }

        // DELETE: api/UserMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserMaster(int id)
        {
            if (_context.UserMasters == null)
            {
                return NotFound();
            }
            var userMaster = await _context.UserMasters.FindAsync(id);
            if (userMaster == null)
            {
                return NotFound();
            }

            _context.UserMasters.Remove(userMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserMasterExists(int id)
        {
            return (_context.UserMasters?.Any(e => e.Userid == id)).GetValueOrDefault();
        }
    }
}
