using Application.Models.Backup;

namespace Application.Repository;

public interface IGroupService
{
    List<GroupResponse> GetAllGroups();

    int CreateGroup(int groupId);

    Domain.Backup CreateBackup(int groupId, bool isSimple);
}