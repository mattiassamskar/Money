using System;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Money.Models;
using Money.StatementParsers;

namespace Money
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

      using (var pdfReader = new PdfReader(bytes))
      {
        for (var i = 1; i <= pdfReader.NumberOfPages; i++)
        {
          text += PdfTextExtractor.GetTextFromPage(pdfReader, i);
          text += Environment.NewLine;
        }
      }
      return text.Split('\n').ToList();
    }
  }
}