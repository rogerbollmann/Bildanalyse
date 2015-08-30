using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TransferService
{
    //Define the ServiceContract
    [ServiceContract]
    public interface IService1
    {
        //Define the Operation Contract
        [OperationContract]

        //Define the public methode for the service
        void UploadImage(string fileName, string fileInfo, byte[] data);

    }


}
