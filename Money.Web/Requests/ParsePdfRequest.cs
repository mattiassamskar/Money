﻿using System.Collections.Generic;
using MediatR;
using Money.Models;

namespace Money.Web.Requests
{
  public class ParsePdfRequest : IRequest<ICollection<Expense>>
  {
    public byte[] Bytes { get; set; }
  }
}
