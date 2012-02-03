using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureStorage.Queue
{
    public class QueueManager : AzureStorage.Queue.IQueueManager
    {
        #region Variables
        /// =======================================================================================
        protected CloudStorageAccount Account;
        protected List<string> QueuesName;
        /// =======================================================================================
        #endregion

        #region Constructors
        /// =======================================================================================
        /// <summary>
        /// Queue manager constructor
        /// </summary>
        /// <param name="QueueManagerKey">Key of the queue storage</param>
        /// <param name="QueuesName">List of queues name</param>
        public QueueManager(string QueueManagerKey, List<string> QueuesName)
        {
            Account = CloudStorageAccount.FromConfigurationSetting(QueueManagerKey);
            this.QueuesName = QueuesName;
            Initialize();
        }
        
        /// <summary>
        /// Initialize the queue client and the queues
        /// </summary>
        protected void Initialize()
        {
            CloudQueueClient QueueClient = Account.CreateCloudQueueClient();
            foreach(string QueueName in QueuesName)
            {
                CloudQueue Queue = QueueClient.GetQueueReference(QueueName);
                Queue.CreateIfNotExist();
            }
        }
        /// =======================================================================================
        #endregion
        
        #region Private Functions
        /// =======================================================================================
        /// <summary>
        /// Return a Queue
        /// </summary>
        /// <param name="QueueName">Name of the Queue</param>
        /// <returns>The Queue</returns>
        protected CloudQueue GetQueue(string QueueName)
        {
            if(QueuesName.Contains(QueueName))
            {
                return Account.CreateCloudQueueClient().GetQueueReference(QueueName);
            }
            return null;
        }
        /// =======================================================================================
        #endregion
        
        #region CRUD
        /// =======================================================================================
        /// <summary>
        /// Create a queue message on the first queue
        /// </summary>
        /// <param name="Message">Message</param>
        public void Create(string Message)
        {
            Create(QueuesName.First(), Message);
        }

        /// <summary>
        /// Create a queue message on a speficied queue
        /// </summary>
        /// <param name="QueueName">Name of the queue</param>
        /// <param name="Message">Message</param>
        public void Create(string QueueName, string Message)
        {
            CloudQueue Queue = GetQueue(QueueName);
            if(Queue == null) return;
            Queue.AddMessage(new CloudQueueMessage(Message));
        }

        /// <summary>
        /// Read the last queue message on the first queue
        /// </summary>
        /// <returns>The queue message</returns>
        public CloudQueueMessage Read()
        {
            return Read(QueuesName.First());
        }

        /// <summary>
        /// Read the last queue message on a specified queue
        /// </summary>
        /// <param name="QueueName">Name of the queue</param>
        /// <returns>The queue message</returns>
        public CloudQueueMessage Read(string QueueName)
        {
            CloudQueue Queue = GetQueue(QueueName);
            if(Queue == null) return null;
            return Queue.GetMessage();
        }

        /// <summary>
        /// Read all queue message on the first message
        /// </summary>
        /// <returns>List of queue messages</returns>
        public List<CloudQueueMessage> ReadAll()
        {
            return ReadAll(QueuesName.First());
        }

        /// <summary>
        /// Read all queue message on a specified queue
        /// </summary>
        /// <param name="QueueName">Name of the queue</param>
        /// <returns>List of queue messages</returns>
        public List<CloudQueueMessage> ReadAll(string QueueName)
        {
            CloudQueue Queue = GetQueue(QueueName);
            if(Queue == null) return null;

            int? QueueCount = Queue.ApproximateMessageCount;
            if(QueueCount == null)
                QueueCount = 0;
            return GetQueue(QueueName).GetMessages((int)QueueCount).ToList();
        }
        
        /// <summary>
        /// Delete a queue message on the first queue
        /// </summary>
        /// <param name="Message">Message to delete</param>
        public void Delete(CloudQueueMessage Message)
        {
            Delete(QueuesName.First(), Message);
        }

        /// <summary>
        /// Delete a queue message on a specified queue
        /// </summary>
        /// <param name="QueueName">Name of the queue</param>
        /// <param name="Message">Message to delete</param>
        public void Delete(string QueueName, CloudQueueMessage Message)
        {
            CloudQueue Queue = GetQueue(QueueName);
            if(Queue == null) return;
            Queue.DeleteMessage(Message);
        }
        /// =======================================================================================
        #endregion
    }
}