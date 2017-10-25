using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace InvoiceParser
{
  public class PdfParser
  {
    public string GetTextFromPdf(byte[] bytes)
    {
      var text = string.Empty;

      using (var pdfReader = new PdfReader(bytes))
      {
        for (var i = 1; i <= pdfReader.NumberOfPages; i++)
        {
          text += PdfTextExtractor.GetTextFromPage(pdfReader, i);
        }
      }
      return text;
    }
  }
}