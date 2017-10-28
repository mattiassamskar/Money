using MediatR;

namespace InvoiceParser.Requests
{
  public class ParsePdfRequest : IRequest<string>
  {
    public byte[] Bytes { get; set; }
  }
}