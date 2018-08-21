using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Money.Core.Models;

namespace Money.Core.Services
{
  public class StatementService : IStatementService
  {
    private readonly List<IStatementParser> _statementParsers;

    public StatementService(List<IStatementParser> statementParsers)
    {
      _statementParsers = statementParsers;
    }

    public IEnumerable<Expense> Parse(byte[] bytes)
    {
      var statement = new Statement
      {
        Lines = GetLinesFromPdf(bytes)
      };

      var parser = _statementParsers.FirstOrDefault(sp => sp.CanParse(statement));
      if (parser == null) throw new ArgumentException("Could not find suitable parser");

      return parser.Parse(statement);
    }

    private List<string> GetLinesFromPdf(byte[] bytes)
    {
      var text = string.Empty;

      using (var pdfReader = new PdfReader(new MemoryStream(bytes)))
      {
        var pdfDocument = new PdfDocument(pdfReader);

        for (var i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
        {
          text += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i));
          text += Environment.NewLine;
        }
      }
      return text.Split('\n').ToList();
    }
  }
}