using System.Collections.Generic;
using MediatR;

namespace InvoiceParser.Requests
{
  public class ParsePdfRequest : IRequest<List<string>>
  {
    public byte[] Bytes { get; set; }
  }
}