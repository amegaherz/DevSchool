using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SautinSoft;

namespace ServerWTP
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        WTPResponse ConvertWithMM(byte[] binary);

        [OperationContract]
        WTPResponse ConvertWithWord(byte[] binary);
    }
    
    [DataContract]
    public class WTPResponse
    {
        [DataMember]
        public string header;

        [DataMember]
        public bool success;

        [DataMember]
        public byte[] binary;
        
        public WTPResponse(byte[] bin)
        {
            header = "A-OK";
            success = true;
            binary = bin;
        }

        public WTPResponse(string message)
        {
            header = message;
            success = false;
        }
    }

}
