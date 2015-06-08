/**********************************************************************
// @@@ START COPYRIGHT @@@
//
// (C) Copyright 2009-2015 Hewlett-Packard Development Company, L.P.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//
// @@@ END COPYRIGHT @@@
********************************************************************/

using System;

namespace Trafodion.Data
{
    internal enum CloseError : int
    {
        Success = 0,
        ParamError = 1,
        InvalidConnection = 2,
        SqlError = 3,
        TransactionError = 4
    }

    internal class CloseReply: INetworkReply
    {
        public CloseError error;
        public int errorDetail;
        public ErrorDesc[] errorDesc;
        public string errorText;

        public void ReadFromDataStream(DataStream ds, TrafodionDBEncoder enc)
        {
            error = (CloseError)ds.ReadInt32();
            errorDetail = ds.ReadInt32();

            switch (error)
            {
                case CloseError.Success:
                case CloseError.InvalidConnection:
                    break;
                case CloseError.ParamError:
                    errorText = enc.GetString(ds.ReadString(), enc.Transport);
                    break;
                case CloseError.SqlError:
                    errorDesc = ErrorDesc.ReadListFromDataStream(ds, enc);
                    break;
            }
        }
    }
}
