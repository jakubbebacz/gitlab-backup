using Application.IServices;
using Application.Models.Backup;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("backups")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly IBackupService _backupService;

        public BackupController(IBackupService backupService)
        {
            _backupService = backupService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BackupResponse>>> GetAllBackups()
        {
            var backups = await _backupService.GetAllBackups();
            return StatusCode(StatusCodes.Status200OK, backups);
        }

        [HttpPost("/{groupId:int}")]
        public async Task<ActionResult<Backup>> CreateBackup([FromRoute] int groupId, [FromQuery] bool isSimple)
        {
            var backup = await _backupService.CreateBackup(groupId, isSimple);
            return StatusCode(StatusCodes.Status201Created, backup);
        }

        [HttpPost("/{groupId:int}/restore")]
        public async Task<ActionResult<Backup>> RestoreBackup([FromRoute] int groupId)
        {
            var backup = await _backupService.RestoreBackup(groupId);
            return StatusCode(StatusCodes.Status201Created, backup);
        }
    }
}