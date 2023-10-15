using Application.IServices;
using Application.Models.Group;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupResponse>>> GetAllGroups()
        {
            var groups = await _groupService.GetAllGroups();
            return StatusCode(StatusCodes.Status200OK, groups);
        }
    }
}