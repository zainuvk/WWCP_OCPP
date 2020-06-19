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
using System.Xml.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod;
using org.GraphDefined.Vanaheimr.Hermod.DNS;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;
using org.GraphDefined.Vanaheimr.Hermod.SOAP;

using org.GraphDefined.WWCP.OCPPv1_6.CS;

#endregion

namespace org.GraphDefined.WWCP.OCPPv1_6.CP
{

    /// <summary>
    /// The charge point HTTP/SOAP/XML server.
    /// </summary>
    public class ChargePointSOAPServer : ASOAPServer
    {

        #region Data

        /// <summary>
        /// The default HTTP/SOAP/XML server name.
        /// </summary>
        public new const           String           DefaultHTTPServerName  = "GraphDefined OCPP " + Version.Number + " HTTP/SOAP/XML Charge Point API";

        /// <summary>
        /// The default HTTP/SOAP/XML server TCP port.
        /// </summary>
        public new static readonly IPPort           DefaultHTTPServerPort  = IPPort.Parse(2010);

        /// <summary>
        /// The default HTTP/SOAP/XML server URI prefix.
        /// </summary>
        public new static readonly HTTPPath         DefaultURLPrefix       = HTTPPath.Parse("/" + Version.Number);

        /// <summary>
        /// The default HTTP/SOAP/XML content type.
        /// </summary>
        public new static readonly HTTPContentType  DefaultContentType     = HTTPContentType.XMLTEXT_UTF8;

        /// <summary>
        /// The default query timeout.
        /// </summary>
        public new static readonly TimeSpan         DefaultRequestTimeout  = TimeSpan.FromMinutes(1);

        #endregion

        #region Events

        #region OnReserveNow

        /// <summary>
        /// An event sent whenever a reserve now SOAP request was received.
        /// </summary>
        public event RequestLogHandler     OnReserveNowSOAPRequest;

        /// <summary>
        /// An event sent whenever a SOAP response to a reserve now request was sent.
        /// </summary>
        public event AccessLogHandler      OnReserveNowSOAPResponse;

        /// <summary>
        /// An event sent whenever a reserve now request was received.
        /// </summary>
        public event OnReserveNowDelegate  OnReserveNowRequest;

        #endregion

        #region OnCancelReservation

        /// <summary>
        /// An event sent whenever a cancel reservation SOAP request was received.
        /// </summary>
        public event RequestLogHandler            OnCancelReservationSOAPRequest;

        /// <summary>
        /// An event sent whenever a SOAP response to a cancel reservation request was sent.
        /// </summary>
        public event AccessLogHandler             OnCancelReservationSOAPResponse;

        /// <summary>
        /// An event sent whenever a cancel reservation request was received.
        /// </summary>
        public event OnCancelReservationDelegate  OnCancelReservationRequest;

        #endregion

        #region OnRemoteStartTransaction

        /// <summary>
        /// An event sent whenever a remote start transaction SOAP request was received.
        /// </summary>
        public event RequestLogHandler                 OnRemoteStartTransactionSOAPRequest;

        /// <summary>
        /// An event sent whenever a SOAP response to a remote start transaction request was sent.
        /// </summary>
        public event AccessLogHandler                  OnRemoteStartTransactionSOAPResponse;

        /// <summary>
        /// An event sent whenever a remote start transaction request was received.
        /// </summary>
        public event OnRemoteStartTransactionDelegate  OnRemoteStartTransactionRequest;

        #endregion

        #region OnRemoteStopTransaction

        /// <summary>
        /// An event sent whenever a remote stop transaction SOAP request was received.
        /// </summary>
        public event RequestLogHandler                OnRemoteStopTransactionSOAPRequest;

        /// <summary>
        /// An event sent whenever a SOAP response to a remote stop transaction request was sent.
        /// </summary>
        public event AccessLogHandler                 OnRemoteStopTransactionSOAPResponse;

        /// <summary>
        /// An event sent whenever a remote stop transaction request was received.
        /// </summary>
        public event OnRemoteStopTransactionDelegate  OnRemoteStopTransactionRequest;

        #endregion


        #region OnDataTransfer

        /// <summary>
        /// An event sent whenever a data transfer SOAP request was received.
        /// </summary>
        public event RequestLogHandler       OnDataTransferSOAPRequest;

        /// <summary>
        /// An event sent whenever a SOAP response to a data transfer request was sent.
        /// </summary>
        public event AccessLogHandler        OnDataTransferSOAPResponse;

        /// <summary>
        /// An event sent whenever a data transfer request was received.
        /// </summary>
        public event OnDataTransferDelegate  OnDataTransferRequest;

        #endregion

        #endregion

        #region Constructor(s)

        #region ChargePointSOAPServer(HTTPServerName, TCPPort = default, URLPrefix = default, ContentType = default, DNSClient = null, AutoStart = false)

        /// <summary>
        /// Initialize a new HTTP server for the charge point HTTP/SOAP/XML API.
        /// </summary>
        /// <param name="HTTPServerName">An optional identification string for the HTTP server.</param>
        /// <param name="TCPPort">An optional TCP port for the HTTP server.</param>
        /// <param name="URLPrefix">An optional prefix for the HTTP URLs.</param>
        /// <param name="ContentType">An optional HTTP content type to use.</param>
        /// <param name="RegisterHTTPRootService">Register HTTP root services for sending a notice to clients connecting via HTML or plain text.</param>
        /// <param name="DNSClient">An optional DNS client to use.</param>
        /// <param name="AutoStart">Start the server immediately.</param>
        public ChargePointSOAPServer(String           HTTPServerName           = DefaultHTTPServerName,
                                     IPPort?          TCPPort                  = null,
                                     HTTPPath?        URLPrefix                = null,
                                     HTTPContentType  ContentType              = null,
                                     Boolean          RegisterHTTPRootService  = true,
                                     DNSClient        DNSClient                = null,
                                     Boolean          AutoStart                = false)

            : base(HTTPServerName.IsNotNullOrEmpty()
                       ? HTTPServerName
                       : DefaultHTTPServerName,
                   TCPPort     ?? DefaultHTTPServerPort,
                   URLPrefix   ?? DefaultURLPrefix,
                   ContentType ?? DefaultContentType,
                   RegisterHTTPRootService,
                   DNSClient,
                   AutoStart: false)

        {

            RegisterURLTemplates();

            if (AutoStart)
                Start();

        }

        #endregion

        #region ChargePointSOAPServer(SOAPServer, URLPrefix = DefaultURLPrefix)

        /// <summary>
        /// Use the given HTTP server for the charge point HTTP/SOAP/XML API.
        /// </summary>
        /// <param name="SOAPServer">A SOAP server.</param>
        /// <param name="URLPrefix">An optional prefix for the HTTP URLs.</param>
        public ChargePointSOAPServer(SOAPServer  SOAPServer,
                                     HTTPPath?   URLPrefix   = null)

            : base(SOAPServer,
                   URLPrefix ?? DefaultURLPrefix)

        {

            RegisterURLTemplates();

        }

        #endregion

        #endregion


        #region RegisterURLTemplates()

        /// <summary>
        /// Register all URL templates for this SOAP API.
        /// </summary>
        protected void RegisterURLTemplates()
        {

            #region / - ReserveNow

            SOAPServer.RegisterSOAPDelegate(HTTPHostname.Any,
                                            URLPrefix,
                                            "ReserveNow",
                                            XML => XML.Descendants(OCPPNS.OCPPv1_6_CS + "reserveNowRequest").FirstOrDefault(),
                                            async (Request, HeaderXML, ReserveNowXML) => {

                #region Send OnReserveNowSOAPRequest event

                try
                {

                    OnReserveNowSOAPRequest?.Invoke(DateTime.UtcNow,
                                                    this.SOAPServer.HTTPServer,
                                                    Request);

                }
                catch (Exception e)
                {
                    e.Log(nameof(ChargePointSOAPServer) + "." + nameof(OnReserveNowSOAPRequest));
                }

                #endregion


                var _OCPPHeader         = SOAPHeader.Parse(HeaderXML);
                var _ReserveNowRequest  = ReserveNowRequest.Parse(ReserveNowXML);

                ReserveNowResponse response            = null;



                #region Call async subscribers

                if (response == null)
                {

                    var results = OnReserveNowRequest?.
                                      GetInvocationList()?.
                                      SafeSelect(subscriber => (subscriber as OnReserveNowDelegate)
                                          (DateTime.UtcNow,
                                           this,
                                           Request.CancellationToken,
                                           Request.EventTrackingId,
                                           _OCPPHeader.ChargeBoxIdentity,
                                           _ReserveNowRequest.ConnectorId,
                                           _ReserveNowRequest.ReservationId,
                                           _ReserveNowRequest.ExpiryDate,
                                           _ReserveNowRequest.IdTag,
                                           _ReserveNowRequest.ParentIdTag,
                                           DefaultRequestTimeout)).
                                      ToArray();

                    if (results.Length > 0)
                    {

                        await Task.WhenAll(results);

                        response = results.FirstOrDefault()?.Result;

                    }

                    if (results.Length == 0 || response == null)
                        response = ReserveNowResponse.Failed;

                }

                #endregion



                #region Create SOAPResponse

                var HTTPResponse = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode  = HTTPStatusCode.OK,
                    Server          = SOAPServer.HTTPServer.DefaultServerName,
                    Date            = DateTime.UtcNow,
                    ContentType     = HTTPContentType.XMLTEXT_UTF8,
                    Content         = SOAP.Encapsulation(_OCPPHeader.ChargeBoxIdentity,
                                                         "/ReserveNowResponse",
                                                         null,
                                                         _OCPPHeader.MessageId,  // MessageId from the request as RelatesTo Id
                                                         _OCPPHeader.To,         // Fake it!
                                                         _OCPPHeader.From,       // Fake it!
                                                         response.ToXML()).ToUTF8Bytes()
                };

                #endregion


                #region Send OnReserveNowSOAPResponse event

                try
                {

                    OnReserveNowSOAPResponse?.Invoke(HTTPResponse.Timestamp,
                                                     this.SOAPServer.HTTPServer,
                                                     Request,
                                                     HTTPResponse);

                }
                catch (Exception e)
                {
                    e.Log(nameof(ChargePointSOAPServer) + "." + nameof(OnReserveNowSOAPResponse));
                }

                #endregion

                return HTTPResponse;

            });

            #endregion

            #region / - CancelReservation

            SOAPServer.RegisterSOAPDelegate(HTTPHostname.Any,
                                            URLPrefix,
                                            "CancelReservation",
                                            XML => XML.Descendants(OCPPNS.OCPPv1_6_CS + "dataTransferRequest").FirstOrDefault(),
                                            async (Request, HeaderXML, CancelReservationXML) => {

                #region Send OnCancelReservationSOAPRequest event

                try
                {

                    OnCancelReservationSOAPRequest?.Invoke(DateTime.UtcNow,
                                                           this.SOAPServer.HTTPServer,
                                                           Request);

                }
                catch (Exception e)
                {
                    e.Log(nameof(ChargePointSOAPServer) + "." + nameof(OnCancelReservationSOAPRequest));
                }

                #endregion


                var _OCPPHeader                = SOAPHeader.Parse(HeaderXML);
                var _CancelReservationRequest  = CancelReservationRequest.Parse(CancelReservationXML);

                CancelReservationResponse response            = null;



                #region Call async subscribers

                if (response == null)
                {

                    var results = OnCancelReservationRequest?.
                                      GetInvocationList()?.
                                      SafeSelect(subscriber => (subscriber as OnCancelReservationDelegate)
                                          (DateTime.UtcNow,
                                           this,
                                           Request.CancellationToken,
                                           Request.EventTrackingId,
                                           _OCPPHeader.ChargeBoxIdentity,
                                           _CancelReservationRequest.ReservationId,
                                           DefaultRequestTimeout)).
                                      ToArray();

                    if (results.Length > 0)
                    {

                        await Task.WhenAll(results);

                        response = results.FirstOrDefault()?.Result;

                    }

                    if (results.Length == 0 || response == null)
                        response = CancelReservationResponse.Failed;

                }

                #endregion



                #region Create SOAPResponse

                var HTTPResponse = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode  = HTTPStatusCode.OK,
                    Server          = SOAPServer.HTTPServer.DefaultServerName,
                    Date            = DateTime.UtcNow,
                    ContentType     = HTTPContentType.XMLTEXT_UTF8,
                    Content         = SOAP.Encapsulation(_OCPPHeader.ChargeBoxIdentity,
                                                         "/CancelReservationResponse",
                                                         null,
                                                         _OCPPHeader.MessageId,  // MessageId from the request as RelatesTo Id
                                                         _OCPPHeader.To,         // Fake it!
                                                         _OCPPHeader.From,       // Fake it!
                                                         response.ToXML()).ToUTF8Bytes()
                };

                #endregion


                #region Send OnCancelReservationSOAPResponse event

                try
                {

                    OnCancelReservationSOAPResponse?.Invoke(HTTPResponse.Timestamp,
                                                            this.SOAPServer.HTTPServer,
                                                            Request,
                                                            HTTPResponse);

                }
                catch (Exception e)
                {
                    e.Log(nameof(ChargePointSOAPServer) + "." + nameof(OnCancelReservationSOAPResponse));
                }

                #endregion

                return HTTPResponse;

            });

            #endregion

            #region / - RemoteStartTransaction

            SOAPServer.RegisterSOAPDelegate(HTTPHostname.Any,
                                            URLPrefix,
                                            "RemoteStartTransaction",
                                            XML => XML.Descendants(OCPPNS.OCPPv1_6_CS + "dataTransferRequest").FirstOrDefault(),
                                            async (Request, HeaderXML, RemoteStartTransactionXML) => {

                #region Send OnRemoteStartTransactionSOAPRequest event

                try
                {

                    OnRemoteStartTransactionSOAPRequest?.Invoke(DateTime.UtcNow,
                                                                this.SOAPServer.HTTPServer,
                                                                Request);

                }
                catch (Exception e)
                {
                    e.Log(nameof(ChargePointSOAPServer) + "." + nameof(OnRemoteStartTransactionSOAPRequest));
                }

                #endregion


                var _OCPPHeader                     = SOAPHeader.Parse(HeaderXML);
                var _RemoteStartTransactionRequest  = RemoteStartTransactionRequest.Parse(RemoteStartTransactionXML);

                RemoteStartTransactionResponse response            = null;



                #region Call async subscribers

                if (response == null)
                {

                    var results = OnRemoteStartTransactionRequest?.
                                      GetInvocationList()?.
                                      SafeSelect(subscriber => (subscriber as OnRemoteStartTransactionDelegate)
                                          (DateTime.UtcNow,
                                           this,
                                           Request.CancellationToken,
                                           Request.EventTrackingId,
                                           _OCPPHeader.ChargeBoxIdentity,
                                           _RemoteStartTransactionRequest.IdTag,
                                           _RemoteStartTransactionRequest.ConnectorId,
                                           _RemoteStartTransactionRequest.ChargingProfile,
                                           DefaultRequestTimeout)).
                                      ToArray();

                    if (results.Length > 0)
                    {

                        await Task.WhenAll(results);

                        response = results.FirstOrDefault()?.Result;

                    }

                    if (results.Length == 0 || response == null)
                        response = RemoteStartTransactionResponse.Failed;

                }

                #endregion



                #region Create SOAPResponse

                var HTTPResponse = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode  = HTTPStatusCode.OK,
                    Server          = SOAPServer.HTTPServer.DefaultServerName,
                    Date            = DateTime.UtcNow,
                    ContentType     = HTTPContentType.XMLTEXT_UTF8,
                    Content         = SOAP.Encapsulation(_OCPPHeader.ChargeBoxIdentity,
                                                         "/RemoteStartTransactionResponse",
                                                         null,
                                                         _OCPPHeader.MessageId,  // MessageId from the request as RelatesTo Id
                                                         _OCPPHeader.To,         // Fake it!
                                                         _OCPPHeader.From,       // Fake it!
                                                         response.ToXML()).ToUTF8Bytes()
                };

                #endregion


                #region Send OnRemoteStartTransactionSOAPResponse event

                try
                {

                    OnRemoteStartTransactionSOAPResponse?.Invoke(HTTPResponse.Timestamp,
                                                                 this.SOAPServer.HTTPServer,
                                                                 Request,
                                                                 HTTPResponse);

                }
                catch (Exception e)
                {
                    e.Log(nameof(ChargePointSOAPServer) + "." + nameof(OnRemoteStartTransactionSOAPResponse));
                }

                #endregion

                return HTTPResponse;

            });

            #endregion

            #region / - RemoteStopTransaction

            SOAPServer.RegisterSOAPDelegate(HTTPHostname.Any,
                                            URLPrefix,
                                            "RemoteStopTransaction",
                                            XML => XML.Descendants(OCPPNS.OCPPv1_6_CS + "dataTransferRequest").FirstOrDefault(),
                                            async (Request, HeaderXML, RemoteStopTransactionXML) => {

                #region Send OnRemoteStopTransactionSOAPRequest event

                try
                {

                    OnRemoteStopTransactionSOAPRequest?.Invoke(DateTime.UtcNow,
                                                               this.SOAPServer.HTTPServer,
                                                               Request);

                }
                catch (Exception e)
                {
                    e.Log(nameof(ChargePointSOAPServer) + "." + nameof(OnRemoteStopTransactionSOAPRequest));
                }

                #endregion


                var _OCPPHeader                    = SOAPHeader.Parse(HeaderXML);
                var _RemoteStopTransactionRequest  = RemoteStopTransactionRequest.Parse(RemoteStopTransactionXML);

                RemoteStopTransactionResponse response            = null;



                #region Call async subscribers

                if (response == null)
                {

                    var results = OnRemoteStopTransactionRequest?.
                                      GetInvocationList()?.
                                      SafeSelect(subscriber => (subscriber as OnRemoteStopTransactionDelegate)
                                          (DateTime.UtcNow,
                                           this,
                                           Request.CancellationToken,
                                           Request.EventTrackingId,
                                           _OCPPHeader.ChargeBoxIdentity,
                                           _RemoteStopTransactionRequest.TransactionId,
                                           DefaultRequestTimeout)).
                                      ToArray();

                    if (results.Length > 0)
                    {

                        await Task.WhenAll(results);

                        response = results.FirstOrDefault()?.Result;

                    }

                    if (results.Length == 0 || response == null)
                        response = RemoteStopTransactionResponse.Failed;

                }

                #endregion



                #region Create SOAPResponse

                var HTTPResponse = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode  = HTTPStatusCode.OK,
                    Server          = SOAPServer.HTTPServer.DefaultServerName,
                    Date            = DateTime.UtcNow,
                    ContentType     = HTTPContentType.XMLTEXT_UTF8,
                    Content         = SOAP.Encapsulation(_OCPPHeader.ChargeBoxIdentity,
                                                         "/RemoteStopTransactionResponse",
                                                         null,
                                                         _OCPPHeader.MessageId,  // MessageId from the request as RelatesTo Id
                                                         _OCPPHeader.To,         // Fake it!
                                                         _OCPPHeader.From,       // Fake it!
                                                         response.ToXML()).ToUTF8Bytes()
                };

                #endregion


                #region Send OnRemoteStopTransactionSOAPResponse event

                try
                {

                    OnRemoteStopTransactionSOAPResponse?.Invoke(HTTPResponse.Timestamp,
                                                                this.SOAPServer.HTTPServer,
                                                                Request,
                                                                HTTPResponse);

                }
                catch (Exception e)
                {
                    e.Log(nameof(ChargePointSOAPServer) + "." + nameof(OnRemoteStopTransactionSOAPResponse));
                }

                #endregion

                return HTTPResponse;

            });

            #endregion


            #region / - DataTransfer

            SOAPServer.RegisterSOAPDelegate(HTTPHostname.Any,
                                            URLPrefix,
                                            "DataTransfer",
                                            XML => XML.Descendants(OCPPNS.OCPPv1_6_CS + "dataTransferRequest").FirstOrDefault(),
                                            async (Request, HeaderXML, DataTransferXML) => {

                #region Send OnDataTransferSOAPRequest event

                try
                {

                    OnDataTransferSOAPRequest?.Invoke(DateTime.UtcNow,
                                                      this.SOAPServer.HTTPServer,
                                                      Request);

                }
                catch (Exception e)
                {
                    e.Log(nameof(ChargePointSOAPServer) + "." + nameof(OnDataTransferSOAPRequest));
                }

                #endregion


                var _OCPPHeader           = SOAPHeader.Parse(HeaderXML);
                var _DataTransferRequest  = DataTransferRequest.Parse(DataTransferXML);

                DataTransferResponse response            = null;



                #region Call async subscribers

                if (response == null)
                {

                    var results = OnDataTransferRequest?.
                                      GetInvocationList()?.
                                      SafeSelect(subscriber => (subscriber as OnDataTransferDelegate)
                                          (DateTime.UtcNow,
                                           this,
                                           Request.CancellationToken,
                                           Request.EventTrackingId,
                                           _OCPPHeader.ChargeBoxIdentity,
                                           _DataTransferRequest.VendorId,
                                           _DataTransferRequest.MessageId,
                                           _DataTransferRequest.Data,
                                           DefaultRequestTimeout)).
                                      ToArray();

                    if (results.Length > 0)
                    {

                        await Task.WhenAll(results);

                        response = results.FirstOrDefault()?.Result;

                    }

                    if (results.Length == 0 || response == null)
                        response = DataTransferResponse.Failed;

                }

                #endregion



                #region Create SOAPResponse

                var HTTPResponse = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode  = HTTPStatusCode.OK,
                    Server          = SOAPServer.HTTPServer.DefaultServerName,
                    Date            = DateTime.UtcNow,
                    ContentType     = HTTPContentType.XMLTEXT_UTF8,
                    Content         = SOAP.Encapsulation(_OCPPHeader.ChargeBoxIdentity,
                                                         "/DataTransferResponse",
                                                         null,
                                                         _OCPPHeader.MessageId,  // MessageId from the request as RelatesTo Id
                                                         _OCPPHeader.To,         // Fake it!
                                                         _OCPPHeader.From,       // Fake it!
                                                         response.ToXML()).ToUTF8Bytes()
                };

                #endregion


                #region Send OnDataTransferSOAPResponse event

                try
                {

                    OnDataTransferSOAPResponse?.Invoke(HTTPResponse.Timestamp,
                                                       this.SOAPServer.HTTPServer,
                                                       Request,
                                                       HTTPResponse);

                }
                catch (Exception e)
                {
                    e.Log(nameof(ChargePointSOAPServer) + "." + nameof(OnDataTransferSOAPResponse));
                }

                #endregion

                return HTTPResponse;

            });

            #endregion

        }

        #endregion


    }

}