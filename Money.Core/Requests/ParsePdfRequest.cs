using System.Collections.Generic;
using MediatR;
using Money.Core.Models;

namespace Money.Core.Requests
{
  public class ParsePdfRequest : IRequest<ICollection<Expense>>
  {
    public byte[] Bytes { get; set; }
  }
}
