/**********************************************************************
// @@@ START COPYRIGHT @@@
//
// (C) Copyright 2010-2015 Hewlett-Packard Development Company, L.P.
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

namespace Trafodion.Data.ETL
{
    using System;

    internal class Header: INetworkReply
    {
        public int afRetCode;
        public int svcRetCode;

        public void ReadFromDataStream(DataStream ds, TrafodionDBEncoder enc)
        {
            this.afRetCode = ds.ReadInt32();
            if (afRetCode != 0)
            {
                throw new Exception(string.Format("NeoGetInfo returned error code: {0}", afRetCode));
            }

            this.svcRetCode = ds.ReadInt32();
            if (svcRetCode != 0)
            {
                // TODO: read 3 ints + 1 string
                throw new Exception("DBTCLI_ERR_SVC_ERR");
            }
        }
    }
}
