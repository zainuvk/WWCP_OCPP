﻿/*
 * Copyright (c) 2014-2020 GraphDefined GmbH
 * This file is part of WWCP OCPP <https://github.com/OpenChargingCloud/WWCP_OCPP>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Linq;
using System.Threading.Tasks;

using cloud.charging.adapters.OCPPv1_6.CP;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod;
using org.GraphDefined.Vanaheimr.Hermod.DNS;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;
using org.GraphDefined.Vanaheimr.Hermod.SOAP;
using org.GraphDefined.Vanaheimr.Hermod.WebSocket;
using Newtonsoft.Json.Linq;
using System.Threading;

#endregion

namespace cloud.charging.adapters.OCPPv1_6.CS
{

    //public class WSRequest : WSMessage
    //{

    //    public     String        Action         { get; }

    //    public WSRequest(String   RequestId,
    //                     String   Action,
    //                     JObject  Data,
    //                     String   ErrorMessage = null)

    //        : base((Byte) MessageTypes.CALL,
    //               RequestId,
    //               Data,
    //               ErrorMessage)

    //    {

    //        this.Action        = Action;

    //    }

    //}

    //public class WSResponse : WSMessage
    //{

    //    public WSResponse(String   RequestId,
    //                      JObject  Data,
    //                      String   ErrorMessage = null)

    //        : base((Byte) MessageTypes.CALLRESULT,
    //               RequestId,
    //               Data,
    //               ErrorMessage)

    //    {

    //    }

    //}

}
