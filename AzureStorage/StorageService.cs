using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using AzureStorage.Blob;
using AzureStorage.Queue;

namespace AzureStorage
{
    public class StorageService
    {
        #region Variables
        /// =======================================================================================
        private string BlobStorageKey;
        //private string TableStorageKey;
        private string QueueStorageKey;

        public IBlobManager BlobStorage;
        public IQueueManager QueueStorage;
        /// =======================================================================================
        #endregion

        #region Constructors
        /// =======================================================================================
        /// <summary>
        /// Empty constructor for Storage services.
        /// </summary>
        public StorageService()
        {
        }

        /// <summary>
        /// Storage services Constructor with include of Initialization
        /// </summary>
        /// <param name="BlobStorage">Initialize the blob storage</param>
        /// <param name="QueueStorage">Initialize the queue storage</param>
        /// <param name="CommonStorageKey">Common storage key</param>
        public StorageService(bool BlobStorage, bool QueueStorage, string CommonStorageKey, List<string> BlobContainers, List<string> QueuesName)
        {
            BlobStorageKey = CommonStorageKey;
            QueueStorageKey = CommonStorageKey;

            InitializeStorages(BlobStorage, QueueStorage, 
                BlobContainers, QueuesName);
        }
        /// =======================================================================================
        #endregion

        #region Functions
        /// =======================================================================================
        public void InitializeStorages(bool BlobStorage, bool QueueStorage,
            List<string> BlobContainers, List<string> QueuesName)
        {
            if(BlobStorage)
                this.BlobStorage = new BlobManager(BlobStorageKey, BlobContainers);
            if (QueueStorage)
                this.QueueStorage = new QueueManager(QueueStorageKey, QueuesName);
        }

        public void InitializeConfigurationSetter()
        {
            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                configSetter(RoleEnvironment.GetConfigurationSettingValue(configName));
            });
        }
        /// =======================================================================================
        #endregion
    }
}