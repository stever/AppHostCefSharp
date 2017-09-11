using System.ServiceModel;

namespace ExcelDnaExample.Services
{
    [ServiceContract]
    public interface IAddinServiceHost
    {
        [OperationContract]
        string GetSheetNames();

        [OperationContract]
        void SelectSheet(string name);
    }
}
