using DataAccess.Interfaces;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebConnection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController(IGenericRepository<Family> repository) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> Post(Family entity)
        {
            try
            {
                await repository.Add(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(Family entity)
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
        public async Task<ActionResult> Delete(Family entity)
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
        public async Task<ActionResult<IEnumerable<Family>>> GetAll()
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
