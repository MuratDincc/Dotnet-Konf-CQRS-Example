using System.Text.Json;
using Confluent.Kafka;
using Consumers.Data.Sync.Configuration;
using Consumers.Data.Sync.Enums;
using Consumers.Data.Sync.Extensions;
using Consumers.Data.Sync.Models;
using Data.MongoDB.Business.Abstracts;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = AppBuilder.InitApp();

        var kafkaSettings = serviceProvider.GetService<KafkaSettings>();

        var conf = new ConsumerConfig
        {
            GroupId = kafkaSettings.GroupId,
            BootstrapServers = kafkaSettings.Host,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
        {
            c.Subscribe(kafkaSettings.TopicName);

            CancellationTokenSource cts = new CancellationTokenSource();

            var transactionBusiness = serviceProvider.GetService<ITransactionBusiness>();

            while (true)
            {
                var cr = c.Consume(cts.Token);

                if (string.IsNullOrWhiteSpace(cr.Value))
                {
                    c.Commit(cr);
                    continue;
                }

                var queueData = JsonSerializer.Deserialize<DebeziumPayload<TransactionMessageModel>>(cr.Value);

                if (queueData == null || queueData.Op == DebeziumOperationType.Read)
                {
                    c.Commit(cr);
                    continue;
                }

                if (queueData.Op == DebeziumOperationType.Create && queueData.After != null)
                {
                    try
                    {
                        var result = await transactionBusiness.Create(new ()
                        {
                            TransactionId = queueData.After.Id,
                            FromWalletId = queueData.After.FromWalletId,
                            ToWalletId = queueData.After.ToWalletId,
                            CardNumber = queueData.After.CardNumber,
                            FirstName = queueData.After.FirstName,
                            LastName = queueData.After.LastName,
                            Email = queueData.After.Email,
                            Total = queueData.After.Total,
                            CreatedOnUtc = queueData.After.CreatedOnUtc,
                            UpdatedOnUtc = queueData.After.UpdatedOnUtc
                        });
                
                        if (!result)
                        {
                            Console.WriteLine($"INSERT - FAIL");
                            continue;
                        }
                
                        Console.WriteLine($"INSERT - OK");
                
                        c.Commit(cr);
                    }
                    catch (Exception insertEx)
                    {
                        Console.WriteLine($"INSERT - FAIL (EXCEPTION): {queueData.After.Id} ({insertEx.Message})");
                    }
                }
                
                if (queueData.Op == DebeziumOperationType.Update && queueData.After != null)
                {
                    try
                    {
                        var result = await transactionBusiness.Update( queueData.After.Id,new ()
                        {
                            TransactionId = queueData.After.Id,
                            FromWalletId = queueData.After.FromWalletId,
                            ToWalletId = queueData.After.ToWalletId,
                            CardNumber = queueData.After.CardNumber,
                            FirstName = queueData.After.FirstName,
                            LastName = queueData.After.LastName,
                            Email = queueData.After.Email,
                            Total = queueData.After.Total,
                            UpdatedOnUtc = queueData.After.UpdatedOnUtc
                        });
                
                        if (!result)
                        {
                            Console.WriteLine($"UPDATE - FAIL");
                            continue;
                        }
                
                        Console.WriteLine($"UPDATE - OK");
                
                        c.Commit(cr);
                    }
                    catch (Exception updateEx)
                    {
                        Console.WriteLine($"UPDATE - FAIL (EXCEPTION): {queueData.After.Id} ({updateEx.Message})");
                    }
                }
                
                if (queueData.Op == DebeziumOperationType.Delete)
                {
                    await transactionBusiness.Delete(queueData.After.Id);
                    
                    Console.WriteLine($"DELETE - OK");
                    c.Commit(cr);
                    continue;
                }
            }
        }
    }
}
