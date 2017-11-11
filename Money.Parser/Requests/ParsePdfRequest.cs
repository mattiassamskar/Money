using System.Collections.Generic;
using MediatR;

namespace Money.Requests
{
  public class ParsePdfRequest : IRequest<List<string>>
  {
    public byte[] Bytes { get; set; }
  }
}