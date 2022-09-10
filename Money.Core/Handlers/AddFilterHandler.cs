using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Money.Core.Requests;

namespace Money.Core.Handlers
{
  public class AddFilterHandler : AsyncRequestHandler<AddFilterRequest>
  {
    private readonly IDbService _dbService;

    public AddFilterHandler(IDbService dbService)
    {
      _dbService = dbService;
    }

    protected override Task Handle(AddFilterRequest request, CancellationToken cancellationToken)
    {
      return Task.Run(() => _dbService.AddFilter(new Models.Filter { Text = request.Text }));
    }
  }
}