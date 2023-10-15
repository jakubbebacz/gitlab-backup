using Application.Models.Group;

namespace Application.IServices;

public interface IGroupService
{
    Task<List<GroupResponse>> GetAllGroups();

    Task<GroupResponse> GetGroup(int groupId);

    Task CreateGroup(CreateGroupRequest createGroupRequest);

    
}