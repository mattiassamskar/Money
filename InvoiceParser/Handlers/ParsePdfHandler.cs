using System;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using InvoiceParser.Requests;
using MediatR;

namespace InvoiceParser.Handlers
{
  public class ParsePdfHandler : IRequestHandler<ParsePdfRequest, List<string>>
  {
    public List<string> Handle(ParsePdfRequest message)
    {
      var text = string.Empty;

      using (var pdfReader = new PdfReader(message.Bytes))
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