using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace InvoiceParser.Tests
{
  public class Tests
  {
    [Fact]
    public void Should_parse_pdf()
    {
      var filters = new List<List<string>>
      {
        new List<string> {"COOP", "ICA"},
        new List<string> {"NETFLIX"}
      };

      var filePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\pdf.pdf";
      var bytes = File.ReadAllBytes(filePath);

      var text = new PdfParser().GetTextFromPdf(bytes);

      var expenses = Expenses.From(text);
      var total = expenses.Sum();

      filters.ForEach(filter =>
      {
        var sum = expenses.ThatMatches(filter).Sum();
      });
    }
  }
}