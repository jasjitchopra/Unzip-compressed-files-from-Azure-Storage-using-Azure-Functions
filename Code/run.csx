#r "Microsoft.WindowsAzure.Storage"

using System;
using System.Configuration;
using System.IO.Compression;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

public static void Run(Stream myBlob, string name, TraceWriter log)
{
    log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

    myBlob.Position = 0;
    string date = DateTime.Now.ToString("yyyyMMdd");
    string StorageConnectionString = ConfigurationManager.ConnectionStrings["storage_con"].ConnectionString;
    string targetShareReference = "tempdaily";
    
    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);

    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

    CloudBlobContainer container = blobClient.GetContainerReference(targetShareReference);
    
    var zip = new ZipArchive(myBlob);
    
    foreach (var file in zip.Entries)
       {
            string foldName = file.Name;
            string newFileName = foldName.Substring(0,foldName.LastIndexOf('.')).Replace(" ","").Replace("_","").Replace(".","");
            log.Info($"Processing file: {file.Name} with new name as: {newFileName}");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("unzip/" + date + "/" + newFileName);
            blockBlob.Properties.ContentType = "text/csv";
            blockBlob.UploadFromStream(file.Open());
       }
    
    log.Info("Done !");
}
