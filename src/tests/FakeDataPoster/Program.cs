using Bogus;
using Bogus.Extensions.Extras;
using FakeDataPoster.Dtos;
using FakeDataPoster.Extensions;
using FakeDataPoster.Services;
using FakeDataPoster.Services.Models;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = AppBuilder.InitApp();
var transactionService = serviceProvider.GetService<ITransactionService>();

var transactionFaker = new Faker<TransactionDto>("tr")
    .RuleFor(u => u.FromWalletId, f => f.Random.Long(min: 10000000))
    .RuleFor(u => u.ToWalletId, f => f.Random.Long(min: 10000000))
    .RuleFor(u => u.CardNumber, f => f.Finance.CreditCardNumberObfuscated())
    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
    .RuleFor(u => u.LastName, f => f.Name.LastName())
    .RuleFor(u => u.Email, f => f.Internet.Email())
    .RuleFor(u => u.Total, f => f.Finance.Amount(18, 4));

var transactionList = transactionFaker.Generate(1000);

foreach (var transaction in transactionList)
{
    var result = await transactionService.Create(new CreateTransactionRequest
    {
        FromWalletId = transaction.FromWalletId,
        ToWalletId = transaction.ToWalletId,
        CardNumber = transaction.CardNumber,
        FirstName = transaction.FirstName,
        LastName = transaction.LastName,
        Email = transaction.Email,
        Total = transaction.Total
    });
    
    Console.WriteLine("--------------------");
    Console.WriteLine($"TransactionId: {result}");
    Console.WriteLine("--------------------");
    Console.WriteLine($"FromWalletId: {transaction.FromWalletId}");
    Console.WriteLine($"ToWalletId: {transaction.ToWalletId}");
    Console.WriteLine($"CardNumber: {transaction.CardNumber}");
    Console.WriteLine($"FirstName: {transaction.FirstName}");
    Console.WriteLine($"LastName: {transaction.LastName}");
    Console.WriteLine($"Email: {transaction.Email}");
    Console.WriteLine($"Total: {transaction.Total}");
    Console.WriteLine("--------------------");
}

Console.ReadLine();