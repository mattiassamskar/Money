using MediatR;

namespace Money.Core.Requests
{
  public class AddFilterRequest : IRequest
  {
    public string Text { get; set; }
  }
}