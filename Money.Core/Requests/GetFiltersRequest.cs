using System.Collections.Generic;
using MediatR;
using Money.Core.Models;

namespace Money.Core.Requests
{
  public class GetFiltersRequest : IRequest<ICollection<Filter>>
  {
  }
}