using Application.Models.Backup;
using Application.Repository;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        // GET: api/Repository
        [HttpGet]
        public ActionResult<List<GroupResponse>> Get()
        {
            var groups = _groupService.GetAllGroups();
            return Ok(groups);
        }
        
        [HttpPost("/{groupId:int}")]
        public ActionResult<Backup> CreateBackup([FromRoute]int groupId, [FromBody]bool isSimple)
        {
            var backup = _groupService.CreateBackup(groupId, isSimple);
            return Ok(backup);
        }
    }
}
