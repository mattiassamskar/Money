using System.Collections.Generic;

namespace Money.Models
{
  public class Statement
  {
    public ICollection<string> Lines { get; set; }
  }
}