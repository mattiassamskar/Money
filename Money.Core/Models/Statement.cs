using System.Collections.Generic;

namespace Money.Core.Models
{
  public class Statement
  {
    public ICollection<string> Lines { get; set; }
  }
}