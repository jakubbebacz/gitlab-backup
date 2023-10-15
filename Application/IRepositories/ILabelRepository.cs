using Application.Models.Label;

namespace Application.IRepositories;

public interface ILabelRepository
{
    public Task CreateLabels(List<CreateLabelsRequest> request);
}