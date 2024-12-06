using DataAccess.Interfaces;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebConnection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomListItemController(IGenericRepository<CustomListItem> repository) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> Post(CustomListItem entity)
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
        public async Task<ActionResult> Update(CustomListItem entity)
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
        public async Task<ActionResult> Delete(CustomListItem entity)
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
        public async Task<ActionResult<IEnumerable<CustomListItem>>> GetAll()
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
        public async Task<ActionResult<List<User>>> GetBy(int id)
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
