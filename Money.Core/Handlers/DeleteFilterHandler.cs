using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Money.Core.Requests;

namespace Money.Core.Handlers
{
  public class DeleteFilterHandler : AsyncRequestHandler<DeleteFilterRequest>
  {
    private readonly IDbService _dbService;

    public DeleteFilterHandler(IDbService dbService)
    {
      _dbService = dbService;
    }

    protected override Task Handle(DeleteFilterRequest request, CancellationToken cancellationToken)
    {
      return Task.Run(() => _dbService.DeleteFilter(request.Id));
    }
  }
}