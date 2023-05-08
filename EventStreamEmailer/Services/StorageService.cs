using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;

namespace EventStreamEmailer.Services
{
    public class StorageService
    {
        private const string _firestorageProjectId = "dhl-warn-traffic-service";
        private const string _mapsImagesFolder = "maps-images";
        private const string _bucketName = "dhl-warn-traffic-service.appspot.com";
        private FirestoreDb _firestoreDb;
        private StorageClient _storageClient;

        public StorageService()
        {
            _firestoreDb = FirestoreDb.Create(_firestorageProjectId);
            _storageClient = StorageClient.Create();
        }

        public async Task<string> StoreMapImageAsync(Stream imageStream, string fileName)
        {
            string objectName = _mapsImagesFolder + "/" + fileName + ".png";

            UploadObjectOptions options = new()
            {
                PredefinedAcl = PredefinedObjectAcl.PublicRead,
            };
            var storageObject = await _storageClient.UploadObjectAsync(_bucketName, objectName, "image/png", imageStream, options); 
            

            string url = $"https://storage.googleapis.com/{_bucketName}/{objectName}";
            return url;
            // return storageObject.MediaLink.ToString();
        }
    }
}