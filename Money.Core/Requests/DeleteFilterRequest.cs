using MediatR;

namespace Money.Core.Requests
{
  public class DeleteFilterRequest : IRequest
  {
    public string Id { get; set; }
  }
}