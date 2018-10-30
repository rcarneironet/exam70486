using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Threading;

namespace AzureQueueStorage
{
    class Program
    {
        private const string StorageAccountName = "rcarneiroazurestorage";
        private const string StorageAccountKey = "rwLKv18/7lG7lSkjhmEnujJUtX9omtkKo3ZscUlYNYuY7KoPUW6KYSqj9N2rax7YciO7U6ZLruWXxzuh4cNyvg==";

        static void Main(string[] args)
        {
          
            //enviando
            var storageAccount = new CloudStorageAccount(
                new StorageCredentials(StorageAccountName, StorageAccountKey),
                true);

            var client = storageAccount.CreateCloudQueueClient();

            var queue = client.GetQueueReference("minha-fila");

            queue.CreateIfNotExistsAsync();

            for (int i = 0; i < 5; i++)
            {
                queue.AddMessageAsync(new CloudQueueMessage($"Mensagem numero: {i}"));
            }

            Console.WriteLine("Completed!");
            Console.ReadKey();

            //consumidor

            while (true)
            {
                var message = queue.GetMessageAsync();

                if (message != null)
                {                                      
                    Console.WriteLine("Retrieved message");
                }
                Thread.Sleep(1000);
            }
        }
    }
}
