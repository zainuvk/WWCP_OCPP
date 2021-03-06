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
using System.Xml.Linq;

using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod.JSON;

#endregion

namespace cloud.charging.adapters.OCPPv1_6.CS
{

    /// <summary>
    /// A remote stop transaction request.
    /// </summary>
    public class RemoteStopTransactionRequest : ARequest<RemoteStopTransactionRequest>
    {

        #region Properties

        /// <summary>
        /// The identification of the transaction which the charge
        /// point is requested to stop.
        /// </summary>
        public Transaction_Id  TransactionId    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a remote stop transaction request.
        /// </summary>
        /// <param name="TransactionId">The identification of the transaction which the charge point is requested to stop.</param>
        public RemoteStopTransactionRequest(Transaction_Id TransactionId)
        {

            #region Initial checks

            if (TransactionId.IsNullOrEmpty)
                throw new ArgumentNullException(nameof(TransactionId),  "The given transaction identification must not be null!");

            #endregion

            this.TransactionId = TransactionId;

        }

        #endregion


        #region Documentation

        // <soap:Envelope xmlns:soap = "http://www.w3.org/2003/05/soap-envelope"
        //                xmlns:wsa  = "http://www.w3.org/2005/08/addressing"
        //                xmlns:ns   = "urn://Ocpp/Cp/2015/10/">
        //
        //    <soap:Header>
        //       ...
        //    </soap:Header>
        //
        //    <soap:Body>
        //       <ns:remoteStopTransactionRequest>
        //
        //          <ns:transactionId>?</ns:transactionId>
        //
        //       </ns:remoteStopTransactionRequest>
        //    </soap:Body>
        //
        // </soap:Envelope>

        // {
        //     "$schema": "http://json-schema.org/draft-04/schema#",
        //     "id":      "urn:OCPP:1.6:2019:12:RemoteStopTransactionRequest",
        //     "title":   "RemoteStopTransactionRequest",
        //     "type":    "object",
        //     "properties": {
        //         "transactionId": {
        //             "type": "integer"
        //         }
        //     },
        //     "additionalProperties": false,
        //     "required": [
        //         "transactionId"
        //     ]
        // }

        #endregion

        #region (static) Parse   (RemoteStopTransactionRequestXML,  OnException = null)

        /// <summary>
        /// Parse the given XML representation of a remote stop transaction request.
        /// </summary>
        /// <param name="RemoteStopTransactionRequestXML">The XML to be parsed.</param>
        /// <param name="OnException">An optional delegate called whenever an exception occured.</param>
        public static RemoteStopTransactionRequest Parse(XElement             RemoteStopTransactionRequestXML,
                                                         OnExceptionDelegate  OnException = null)
        {

            if (TryParse(RemoteStopTransactionRequestXML,
                         out RemoteStopTransactionRequest remoteStopTransactionRequest,
                         OnException))
            {
                return remoteStopTransactionRequest;
            }

            return null;

        }

        #endregion

        #region (static) Parse   (RemoteStopTransactionRequestJSON, OnException = null)

        /// <summary>
        /// Parse the given JSON representation of a remote stop transaction request.
        /// </summary>
        /// <param name="RemoteStopTransactionRequestJSON">The JSON to be parsed.</param>
        /// <param name="OnException">An optional delegate called whenever an exception occured.</param>
        public static RemoteStopTransactionRequest Parse(JObject              RemoteStopTransactionRequestJSON,
                                                         OnExceptionDelegate  OnException = null)
        {

            if (TryParse(RemoteStopTransactionRequestJSON,
                         out RemoteStopTransactionRequest remoteStopTransactionRequest,
                         OnException))
            {
                return remoteStopTransactionRequest;
            }

            return null;

        }

        #endregion

        #region (static) Parse   (RemoteStopTransactionRequestText, OnException = null)

        /// <summary>
        /// Parse the given text representation of a remote stop transaction request.
        /// </summary>
        /// <param name="RemoteStopTransactionRequestText">The text to be parsed.</param>
        /// <param name="OnException">An optional delegate called whenever an exception occured.</param>
        public static RemoteStopTransactionRequest Parse(String               RemoteStopTransactionRequestText,
                                                         OnExceptionDelegate  OnException = null)
        {

            if (TryParse(RemoteStopTransactionRequestText,
                         out RemoteStopTransactionRequest remoteStopTransactionRequest,
                         OnException))
            {
                return remoteStopTransactionRequest;
            }

            return null;

        }

        #endregion

        #region (static) TryParse(RemoteStopTransactionRequestXML,  out RemoteStopTransactionRequest, OnException = null)

        /// <summary>
        /// Try to parse the given XML representation of a remote stop transaction request.
        /// </summary>
        /// <param name="RemoteStopTransactionRequestXML">The XML to be parsed.</param>
        /// <param name="RemoteStopTransactionRequest">The parsed remote stop transaction request.</param>
        /// <param name="OnException">An optional delegate called whenever an exception occured.</param>
        public static Boolean TryParse(XElement                          RemoteStopTransactionRequestXML,
                                       out RemoteStopTransactionRequest  RemoteStopTransactionRequest,
                                       OnExceptionDelegate               OnException  = null)
        {

            try
            {

                RemoteStopTransactionRequest = new RemoteStopTransactionRequest(

                                                   RemoteStopTransactionRequestXML.MapValueOrFail(OCPPNS.OCPPv1_6_CP + "transactionId",
                                                                                                  Transaction_Id.Parse)

                                               );

                return true;

            }
            catch (Exception e)
            {

                OnException?.Invoke(DateTime.UtcNow, RemoteStopTransactionRequestXML, e);

                RemoteStopTransactionRequest = null;
                return false;

            }

        }

        #endregion

        #region (static) TryParse(RemoteStopTransactionRequestJSON, out RemoteStopTransactionRequest, OnException = null)

        /// <summary>
        /// Try to parse the given JSON representation of a remote stop transaction request.
        /// </summary>
        /// <param name="RemoteStopTransactionRequestJSON">The JSON to be parsed.</param>
        /// <param name="RemoteStopTransactionRequest">The parsed remote stop transaction request.</param>
        /// <param name="OnException">An optional delegate called whenever an exception occured.</param>
        public static Boolean TryParse(JObject                           RemoteStopTransactionRequestJSON,
                                       out RemoteStopTransactionRequest  RemoteStopTransactionRequest,
                                       OnExceptionDelegate               OnException  = null)
        {

            try
            {

                RemoteStopTransactionRequest = null;

                #region TransactionId

                if (!RemoteStopTransactionRequestJSON.ParseMandatory("transactionId",
                                                                     "transaction identification",
                                                                     Transaction_Id.TryParse,
                                                                     out Transaction_Id  TransactionId,
                                                                     out String          ErrorResponse))
                {
                    return false;
                }

                #endregion


                RemoteStopTransactionRequest = new RemoteStopTransactionRequest(TransactionId);

                return true;

            }
            catch (Exception e)
            {

                OnException?.Invoke(DateTime.UtcNow, RemoteStopTransactionRequestJSON, e);

                RemoteStopTransactionRequest = null;
                return false;

            }

        }

        #endregion

        #region (static) TryParse(RemoteStopTransactionRequestText, out RemoteStopTransactionRequest, OnException = null)

        /// <summary>
        /// Try to parse the given text representation of a remote stop transaction request.
        /// </summary>
        /// <param name="RemoteStopTransactionRequestText">The text to be parsed.</param>
        /// <param name="RemoteStopTransactionRequest">The parsed remote stop transaction request.</param>
        /// <param name="OnException">An optional delegate called whenever an exception occured.</param>
        public static Boolean TryParse(String                            RemoteStopTransactionRequestText,
                                       out RemoteStopTransactionRequest  RemoteStopTransactionRequest,
                                       OnExceptionDelegate               OnException  = null)
        {

            try
            {

                RemoteStopTransactionRequestText = RemoteStopTransactionRequestText?.Trim();

                if (RemoteStopTransactionRequestText.IsNotNullOrEmpty())
                {

                    if (RemoteStopTransactionRequestText.StartsWith("{") &&
                        TryParse(JObject.Parse(RemoteStopTransactionRequestText),
                                 out RemoteStopTransactionRequest,
                                 OnException))
                    {
                        return true;
                    }

                    if (TryParse(XDocument.Parse(RemoteStopTransactionRequestText).Root,
                                 out RemoteStopTransactionRequest,
                                 OnException))
                    {
                        return true;
                    }

                }

            }
            catch (Exception e)
            {
                OnException?.Invoke(DateTime.UtcNow, RemoteStopTransactionRequestText, e);
            }

            RemoteStopTransactionRequest = null;
            return false;

        }

        #endregion

        #region ToXML()

        /// <summary>
        /// Return a XML representation of this object.
        /// </summary>
        public XElement ToXML()

            => new XElement(OCPPNS.OCPPv1_6_CP + "remoteStopTransactionRequest",

                   new XElement(OCPPNS.OCPPv1_6_CP + "transactionId",  TransactionId.ToString())

               );

        #endregion

        #region ToJSON(CustomRemoteStopTransactionRequestSerializer = null)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomRemoteStopTransactionRequestSerializer">A delegate to serialize custom remote stop transaction requests.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<RemoteStopTransactionRequest> CustomRemoteStopTransactionRequestSerializer  = null)
        {

            var JSON = JSONObject.Create(
                           new JProperty("transactionId",  TransactionId.ToString())
                       );

            return CustomRemoteStopTransactionRequestSerializer != null
                       ? CustomRemoteStopTransactionRequestSerializer(this, JSON)
                       : JSON;

        }

        #endregion


        #region Operator overloading

        #region Operator == (RemoteStopTransactionRequest1, RemoteStopTransactionRequest2)

        /// <summary>
        /// Compares two remote stop transaction requests for equality.
        /// </summary>
        /// <param name="RemoteStopTransactionRequest1">A remote stop transaction request.</param>
        /// <param name="RemoteStopTransactionRequest2">Another remote stop transaction request.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (RemoteStopTransactionRequest RemoteStopTransactionRequest1, RemoteStopTransactionRequest RemoteStopTransactionRequest2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(RemoteStopTransactionRequest1, RemoteStopTransactionRequest2))
                return true;

            // If one is null, but not both, return false.
            if ((RemoteStopTransactionRequest1 is null) || (RemoteStopTransactionRequest2 is null))
                return false;

            return RemoteStopTransactionRequest1.Equals(RemoteStopTransactionRequest2);

        }

        #endregion

        #region Operator != (RemoteStopTransactionRequest1, RemoteStopTransactionRequest2)

        /// <summary>
        /// Compares two remote stop transaction requests for inequality.
        /// </summary>
        /// <param name="RemoteStopTransactionRequest1">A remote stop transaction request.</param>
        /// <param name="RemoteStopTransactionRequest2">Another remote stop transaction request.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (RemoteStopTransactionRequest RemoteStopTransactionRequest1, RemoteStopTransactionRequest RemoteStopTransactionRequest2)

            => !(RemoteStopTransactionRequest1 == RemoteStopTransactionRequest2);

        #endregion

        #endregion

        #region IEquatable<RemoteStopTransactionRequest> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object is null)
                return false;

            if (!(Object is RemoteStopTransactionRequest RemoteStopTransactionRequest))
                return false;

            return Equals(RemoteStopTransactionRequest);

        }

        #endregion

        #region Equals(RemoteStopTransactionRequest)

        /// <summary>
        /// Compares two remote stop transaction requests for equality.
        /// </summary>
        /// <param name="RemoteStopTransactionRequest">A remote stop transaction request to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public override Boolean Equals(RemoteStopTransactionRequest RemoteStopTransactionRequest)
        {

            if (RemoteStopTransactionRequest is null)
                return false;

            return TransactionId.Equals(RemoteStopTransactionRequest.TransactionId);

        }

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()

            => TransactionId.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => TransactionId.ToString();

        #endregion

    }

}
