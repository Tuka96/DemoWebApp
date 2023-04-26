using WebApp1.Data.ViewModel;

namespace WebApp1.Data
{
    public interface IBatchService
    {
        string AddBatch(BatchVM batch);
        BatchVM GetBatchByGuid(string guid);
        void AddFileBatch(string path, string _fileName, string _batchId);
    }
}