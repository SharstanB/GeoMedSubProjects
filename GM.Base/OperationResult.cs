using System;
using System.Collections.Generic;

namespace GM.Base
{
    public class OperationResult<T> 
    {
        public T Result { get; set; }
        
        public IEnumerable<T> IEnumerableResult { get; set; }

        public OperationResultTypes OperationResultType { get; set; }

        public Exception Exception { get; set; }

        public string OperationResultMessage { get; set; }

        public bool Issuccess => OperationResultType == OperationResultTypes.Success;
    }
}
