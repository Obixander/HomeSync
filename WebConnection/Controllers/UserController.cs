using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;
using DataAccess.Repositories;
using DataAccess.Interfaces;
namespace WebConnection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepository repository) : ControllerBase
    {

        [HttpPost]
        [Route(nameof(CreateUserBy))]
        public async Task<ActionResult> CreateUserBy(User user)
        {
            try
            {
                var test = await repository.CreateUser(user);
                if(test.Contains("Succes"))
                {
                    return Ok(test);
                }
                return BadRequest(test);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult> Login(User user)
        {
            try
            {
                var test = await repository.Login(user);
                if (test != null)
                {
                    return Ok(test);
                } 
                return BadRequest(test);
            }
            catch
            {
                throw;
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update(User entity)
        {
            try
            {
                await repository.Update(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(User entity)
        {
            try
            {
                await repository.Delete(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete]
        [Route(nameof(DeleteAt))]
        public async Task<ActionResult> DeleteAt(int id)
        {
            try
            {
                await repository.DeleteAt(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                return Ok(await repository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route(nameof(GetBy))]
        public async Task<ActionResult<List<User>>> GetBy(int id) //might have to change this to from query later
        {
            try
            {
                return Ok(await repository.GetBy(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
