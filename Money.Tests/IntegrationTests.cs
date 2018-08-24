using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Refit;
using Xunit;

namespace Money.Tests
{
  public class IntegrationTests
  {
    public interface IUploadApi
    {
      [Multipart]
      [Post("/upload")]
      Task Upload(ByteArrayPart byteArrayPart);
    }

    public interface IExpensesApi
    {
      [Get("/expenses")]
      Task<List<Expense>> GetExpenses();
    }

    [Fact(Skip="Skip this test on build server")]
    public async Task Upload_Always_ParsesFileAndSavesExpensesAsync()
    {
      // Arrange
      var uploadApi = RestService.For<IUploadApi>("http://localhost:5000");
      var expensesApi = RestService.For<IExpensesApi>("http://localhost:5000");
      var bytes = File.ReadAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\pdf.pdf");
      var byteArrayPart = new ByteArrayPart(bytes, "pdf.pdf");

      // Act
      await uploadApi.Upload(byteArrayPart);
      var expenses = await expensesApi.GetExpenses();

      // Assert
      Assert.NotEmpty(expenses);
    }
  }

  public class Expense
  {
    public DateTime Date { get; set; }

    public string Description { get; set; }

    public double Amount { get; set; }
  }
}