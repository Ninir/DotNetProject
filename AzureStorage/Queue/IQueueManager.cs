using System;
namespace AzureStorage.Queue
{
    public interface IQueueManager
    {
        void Create(string Message);
        void Create(string QueueName, string Message);
        void Delete(Microsoft.WindowsAzure.StorageClient.CloudQueueMessage Message);
        void Delete(string QueueName, Microsoft.WindowsAzure.StorageClient.CloudQueueMessage Message);
        Microsoft.WindowsAzure.StorageClient.CloudQueueMessage Read();
        Microsoft.WindowsAzure.StorageClient.CloudQueueMessage Read(string QueueName);
        System.Collections.Generic.List<Microsoft.WindowsAzure.StorageClient.CloudQueueMessage> ReadAll();
        System.Collections.Generic.List<Microsoft.WindowsAzure.StorageClient.CloudQueueMessage> ReadAll(string QueueName);
    }
}
