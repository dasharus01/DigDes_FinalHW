using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace finalHW_service
{
    [ServiceContract]
    public interface ICounterService
    {

        [OperationContract]
        Dictionary<string, int> wordCountFun(string[] data);
       
    }
}
