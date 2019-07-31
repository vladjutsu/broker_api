using System;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.Azure.Storage; // Namespace for CloudStorageAccount
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Queue; // Namespace for Queue storage types
using Newtonsoft.Json;
using testapp.Models;

namespace testapp.Controllers
{
    public class AzureQueueStorage
    {
        private readonly CloudQueue _queue;

        public AzureQueueStorage()
        {
            const string StorageAccountName = "uvcstorage";
            const string StorageAccountKey = "miZ0H6x8F+2nZ6OEgzDgBt3vn22iB1rrNJUvOjYX1LEHr/XDhZz/BO7Z+huaBnWoTSrHVtvOlwnjQqWoHfR+gw==";

            var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), true);
            var client = storageAccount.CreateCloudQueueClient();

            _queue = client.GetQueueReference("brokerqueue");
            _queue.CreateIfNotExists();
        }

        public void AddMessage(User user)
        {
            string serializedMessage = JsonConvert.SerializeObject(user);
            CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(serializedMessage);
            _queue.AddMessage(cloudQueueMessage);
        }

    }
}
