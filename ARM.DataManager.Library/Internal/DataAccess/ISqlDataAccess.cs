using System.Collections.Generic;

namespace ARM.DataManager.Library
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        void Dispose();
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        void RollbackTransaction();
        List<T> SaveAndLoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        List<T> SaveAndLoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        void SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
        void SaveDataInTransaction<T>(string storedProcedure, T parameters);
        void StartTransaction(string connectionStringName);
    }
}