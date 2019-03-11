using MediatR;

namespace Money.Core.Requests
{
  public class DeleteExpenseRequest : IRequest
  {
    public string Id { get; set; }
  }
}