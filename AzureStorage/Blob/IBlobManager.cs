using System;
namespace AzureStorage.Blob
{
    public interface IBlobManager
    {
        void Create(string BlobName, string ContentType, System.Collections.Specialized.NameValueCollection Metadata, System.IO.Stream Data);
        void Create(string ContainerName, string BlobName, string ContentType, System.Collections.Specialized.NameValueCollection Metadata, System.IO.Stream Data);
        void Delete(string BlobName);
        void Delete(string ContainerName, string BlobName);
        Microsoft.WindowsAzure.StorageClient.CloudBlob Read(string BlobName);
        Microsoft.WindowsAzure.StorageClient.CloudBlob Read(string ContainerName, string BlobName);
        System.Collections.Generic.List<Microsoft.WindowsAzure.StorageClient.CloudBlob> ReadAll();
        System.Collections.Generic.List<Microsoft.WindowsAzure.StorageClient.CloudBlob> ReadAll(string ContainerName);
    }
}
