using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Collections.Specialized;
using System.IO;

namespace AzureStorage.Blob
{
    /// <summary>
    /// Manage a Blob storage
    /// </summary>
    public class BlobManager : AzureStorage.Blob.IBlobManager
    {
        #region Variables
        /// =======================================================================================
        protected CloudStorageAccount Account;
        protected List<string> BlobContainers;
        /// =======================================================================================
        #endregion

        #region Constructors
        /// =======================================================================================
        /// <summary>
        /// Blob manager constructor
        /// </summary>
        /// <param name="BlobStorageKey">Key of the blob storage</param>
        /// <param name="BlobContainers">List of blob containers</param>
        public BlobManager(string BlobStorageKey, List<string> BlobContainers)
        {
            Account = CloudStorageAccount.FromConfigurationSetting(BlobStorageKey);
            this.BlobContainers = BlobContainers;
            Initialize();
        }

        /// <summary>
        /// Initialize the blob client and the blob containers
        /// </summary>
        protected void Initialize()
        {
            CloudBlobClient BlobClient = Account.CreateCloudBlobClient();
            foreach(string ContainerReference in BlobContainers)
            {
                CloudBlobContainer Container = BlobClient.GetContainerReference(ContainerReference);
                Container.CreateIfNotExist();
                Container.SetPermissions(
                        new BlobContainerPermissions 
                            { PublicAccess = BlobContainerPublicAccessType.Blob }
                    );
            }
        }
        /// =======================================================================================
        #endregion

        #region Private Functions
        /// =======================================================================================
        /// <summary>
        /// Return a Blob container
        /// </summary>
        /// <param name="ContainerName">Name of the blob container</param>
        /// <returns>The Blob Container</returns>
        protected CloudBlobContainer GetContainer(string ContainerName)
        {
            if(BlobContainers.Contains(ContainerName))
            {
                return Account.CreateCloudBlobClient().GetContainerReference(ContainerName);
            }
            return null;
        }
        /// =======================================================================================
        #endregion

        #region CRUD
        /// =======================================================================================
        /// <summary>
        /// Create a blob object on the first container
        /// </summary>
        /// <param name="BlobName">Name of the blob</param>
        /// <param name="ContentType">Type of the blob</param>
        /// <param name="Metadata">Metadata of the blob</param>
        /// <param name="Data">Data Stream of the blob</param>
        public void Create(string BlobName, string ContentType, NameValueCollection Metadata, Stream Data)
        {
            Create(BlobContainers.First(), BlobName, ContentType, Metadata, Data);
        }

        /// <summary>
        /// Create a blob object on a spacified container
        /// </summary>
        /// <param name="ContainerName">Name of the Container</param>
        /// <param name="BlobName">Name of the blob</param>
        /// <param name="ContentType">Type of the blob</param>
        /// <param name="Metadata">Metadata of the blob</param>
        /// <param name="Data">Data Stream of the blob</param>
        public void Create(string ContainerName, string BlobName, string ContentType, NameValueCollection Metadata, Stream Data)
        {
            CloudBlobContainer BlobContainer = GetContainer(ContainerName);
            if(BlobContainers == null) return;

            CloudBlob Blob = BlobContainer.GetBlobReference(BlobName);
            Blob.Properties.ContentType = ContentType;
            Blob.Metadata.Add(Metadata);
            Blob.UploadFromStream(Data);
        }

        /// <summary>
        /// Read a blob on the first container
        /// </summary>
        /// <param name="BlobName">Name of the blob</param>
        /// <returns>The blob object</returns>
        public CloudBlob Read(string BlobName)
        {
            return Read(BlobContainers.First(), BlobName);
        }

        /// <summary>
        /// Read a blob on a specified container
        /// </summary>
        /// <param name="ContainerName">Name of the container</param>
        /// <param name="BlobName">Name of the blob</param>
        /// <returns>The blob object</returns>
        public CloudBlob Read(string ContainerName, string BlobName)
        {
            CloudBlobContainer BlobContainer = GetContainer(ContainerName);
            if(BlobContainers == null) return null;
            return BlobContainer.GetBlobReference(BlobName);
        }

        /// <summary>
        /// Read all blob on the first container
        /// </summary>
        /// <returns>List of blob object</returns>
        public List<CloudBlob> ReadAll()
        {
            return ReadAll(BlobContainers.First());
        }

        /// <summary>
        /// Read all blob on a specified container
        /// </summary>
        /// <param name="ContainerName">Name of the container</param>
        /// <returns>List of blob object</returns>
        public List<CloudBlob> ReadAll(string ContainerName)
        {
            CloudBlobContainer BlobContainer = GetContainer(ContainerName);
            if(BlobContainers == null) return null;

            IEnumerable<IListBlobItem> Blobs = BlobContainer.ListBlobs(new BlobRequestOptions()
            {
                UseFlatBlobListing = true,
                BlobListingDetails = BlobListingDetails.All
            });

            List<CloudBlob> CloudBlobs = new List<CloudBlob>();
            foreach(IListBlobItem Item in Blobs)
            {
                CloudBlob Blob = Item as CloudBlob;
                CloudBlobs.Add(Blob);
            }
            return CloudBlobs;
        }

        /// <summary>
        /// Delete a blob on the first container
        /// </summary>
        /// <param name="BlobName">Name of the blob</param>
        public void Delete(string BlobName)
        {
            Delete(BlobContainers.First(), BlobName);
        }

        /// <summary>
        /// Delete a blob on a speficied container
        /// </summary>
        /// <param name="ContainerName">Name of the container</param>
        /// <param name="BlobName">Name of the blob</param>
        public void Delete(string ContainerName, string BlobName)
        {
            CloudBlobContainer BlobContainer = GetContainer(ContainerName);
            if(BlobContainers == null) return;
            BlobContainer.GetBlobReference(BlobName).DeleteIfExists();
        }
        /// =======================================================================================
        #endregion
    }
}