using System.Collections.Generic;
using System.Linq;
using MediatR;
using Money.Core.Models;
using Money.Core.Requests;

namespace Money.Core.Handlers
{
  public class GetFiltersHandler : RequestHandler<GetFiltersRequest, ICollection<Filter>>
  {
    private readonly IDbService _dbService;

    public GetFiltersHandler(IDbService dbService)
    {
      _dbService = dbService;
    }
    protected override ICollection<Filter> Handle(GetFiltersRequest request)
    {
      return _dbService.GetFilters().ToList();
    }
  }
}