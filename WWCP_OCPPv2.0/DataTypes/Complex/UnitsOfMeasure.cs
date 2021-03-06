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

using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod.JSON;

#endregion

namespace cloud.charging.adapters.OCPPv2_0
{

    /// <summary>
    /// A unit of measure and a multiplier.
    /// </summary>
    public class UnitsOfMeasure
    {

        #region Properties

        /// <summary>
        /// The unit of the measured value. 20
        /// </summary>
        public String      Unit          { get; }

        /// <summary>
        /// Multiplier, this value represents the exponent to base 10. I.e. multiplier 3 means 10 raised to the 3rd power.
        /// </summary>
        public Int32       Multiplier    { get; }

        /// <summary>
        /// An optional custom data object to allow to store any kind of customer specific data.
        /// </summary>
        public CustomData  CustomData    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new unit of measure and a multiplier.
        /// </summary>
        /// <param name="Unit">The unit of the measured value.</param>
        /// <param name="Multiplier">Multiplier, this value represents the exponent to base 10. I.e. multiplier 3 means 10 raised to the 3rd power.</param>
        /// <param name="CustomData">An optional custom data object to allow to store any kind of customer specific data.</param>
        public UnitsOfMeasure(String      Unit,
                              Int32       Multiplier,
                              CustomData  CustomData  = null)
        {

            this.Unit        = Unit?.Trim() ?? throw new ArgumentNullException(nameof(Unit), "The given unit of measure must not be null or empty!");
            this.Multiplier  = Multiplier;
            this.CustomData  = CustomData;

        }

        #endregion


        #region Documentation

        // {
        //   "$schema": "http://json-schema.org/draft-06/schema#",
        //   "$id": "urn:OCPP:Cp:2:2020:3:UnitsOfMeasureType",
        //   "comment": "OCPP 2.0.1 FINAL",
        //   "description": "Represents a UnitOfMeasure with a multiplier\r\n",
        //   "javaType": "UnitsOfMeasure",
        //   "type": "object",
        //   "additionalProperties": false,
        //   "properties": {
        //     "customData": {
        //       "$ref": "#/definitions/CustomDataType"
        //     },
        //     "unit": {
        //       "description": "Unit of the value. Default = \"Wh\" if the (default) measurand is an \"Energy\" type.\r\nThis field SHALL use a value from the list Standardized Units of Measurements in Part 2 Appendices. \r\nIf an applicable unit is available in that list, otherwise a \"custom\" unit might be used.\r\n",
        //       "type": "string",
        //       "default": "Wh",
        //       "maxLength": 20
        //     },
        //     "multiplier": {
        //       "description": "Multiplier, this value represents the exponent to base 10. I.e. multiplier 3 means 10 raised to the 3rd power. Default is 0.\r\n",
        //       "type": "integer",
        //       "default": 0
        //     }
        //   }
        // }

        #endregion

        #region (static) Parse   (UnitsOfMeasureJSON, OnException = null)

        /// <summary>
        /// Parse the given JSON representation of a units of measure.
        /// </summary>
        /// <param name="UnitsOfMeasureJSON">The JSON to be parsed.</param>
        /// <param name="OnException">An optional delegate called whenever an exception occured.</param>
        public static UnitsOfMeasure Parse(JObject              UnitsOfMeasureJSON,
                                           OnExceptionDelegate  OnException   = null)
        {

            if (TryParse(UnitsOfMeasureJSON,
                         out UnitsOfMeasure modem,
                         OnException))
            {
                return modem;
            }

            return default;

        }

        #endregion

        #region (static) Parse   (UnitsOfMeasureText, OnException = null)

        /// <summary>
        /// Parse the given text representation of a units of measure.
        /// </summary>
        /// <param name="UnitsOfMeasureText">The text to be parsed.</param>
        /// <param name="OnException">An optional delegate called whenever an exception occured.</param>
        public static UnitsOfMeasure Parse(String               UnitsOfMeasureText,
                                           OnExceptionDelegate  OnException   = null)
        {


            if (TryParse(UnitsOfMeasureText,
                         out UnitsOfMeasure modem,
                         OnException))
            {
                return modem;
            }

            return default;

        }

        #endregion

        #region (static) TryParse(UnitsOfMeasureJSON, out UnitsOfMeasure, OnException = null)

        /// <summary>
        /// Try to parse the given JSON representation of a units of measure.
        /// </summary>
        /// <param name="UnitsOfMeasureJSON">The JSON to be parsed.</param>
        /// <param name="UnitsOfMeasure">The parsed units of measure.</param>
        /// <param name="OnException">An optional delegate called whenever an exception occured.</param>
        public static Boolean TryParse(JObject              UnitsOfMeasureJSON,
                                       out UnitsOfMeasure   UnitsOfMeasure,
                                       OnExceptionDelegate  OnException  = null)
        {

            try
            {

                UnitsOfMeasure = default;

                #region Unit

                if (!UnitsOfMeasureJSON.ParseMandatoryText("unit",
                                                           "unit measure",
                                                           out String  Unit,
                                                           out String  ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Multiplier

                if (!UnitsOfMeasureJSON.ParseMandatory("multiplier",
                                                       "multiplier",
                                                       out Int32  Multiplier,
                                                       out        ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region CustomData

                if (UnitsOfMeasureJSON.ParseOptionalJSON("customData",
                                                         "custom data",
                                                         OCPPv2_0.CustomData.TryParse,
                                                         out CustomData  CustomData,
                                                         out             ErrorResponse,
                                                         OnException))
                {

                    if (ErrorResponse != null)
                        return false;

                }

                #endregion


                UnitsOfMeasure = new UnitsOfMeasure(Unit.Trim(),
                                                    Multiplier,
                                                    CustomData);

                return true;

            }
            catch (Exception e)
            {

                OnException?.Invoke(DateTime.UtcNow, UnitsOfMeasureJSON, e);

                UnitsOfMeasure = default;
                return false;

            }

        }

        #endregion

        #region (static) TryParse(UnitsOfMeasureText, out UnitsOfMeasure, OnException = null)

        /// <summary>
        /// Try to parse the given text representation of a units of measure.
        /// </summary>
        /// <param name="UnitsOfMeasureText">The text to be parsed.</param>
        /// <param name="UnitsOfMeasure">The parsed units of measure.</param>
        /// <param name="OnException">An optional delegate called whenever an exception occured.</param>
        public static Boolean TryParse(String               UnitsOfMeasureText,
                                       out UnitsOfMeasure   UnitsOfMeasure,
                                       OnExceptionDelegate  OnException  = null)
        {

            try
            {

                UnitsOfMeasureText = UnitsOfMeasureText?.Trim();

                if (UnitsOfMeasureText.IsNotNullOrEmpty() &&
                    TryParse(JObject.Parse(UnitsOfMeasureText),
                             out UnitsOfMeasure,
                             OnException))
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                OnException?.Invoke(DateTime.UtcNow, UnitsOfMeasureText, e);
            }

            UnitsOfMeasure = default;
            return false;

        }

        #endregion

        #region ToJSON(CustomUnitsOfMeasureResponseSerializer = null, CustomCustomDataResponseSerializer = null)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomUnitsOfMeasureResponseSerializer">A delegate to serialize custom UnitsOfMeasure objects.</param>
        /// <param name="CustomCustomDataResponseSerializer">A delegate to serialize CustomData objects.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<UnitsOfMeasure>  CustomUnitsOfMeasureResponseSerializer   = null,
                              CustomJObjectSerializerDelegate<CustomData>      CustomCustomDataResponseSerializer       = null)
        {

            var JSON = JSONObject.Create(

                           new JProperty("unit",               Unit),

                           Multiplier != 0
                               ? new JProperty("multiplier",   Multiplier)
                               : null,

                           CustomData != null
                               ? new JProperty("customData",   CustomData.ToJSON(CustomCustomDataResponseSerializer))
                               : null

                       );

            return CustomUnitsOfMeasureResponseSerializer != null
                       ? CustomUnitsOfMeasureResponseSerializer(this, JSON)
                       : JSON;

        }

        #endregion


        #region Operator overloading

        #region Operator == (UnitsOfMeasure1, UnitsOfMeasure2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="UnitsOfMeasure1">An id tag info.</param>
        /// <param name="UnitsOfMeasure2">Another id tag info.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (UnitsOfMeasure UnitsOfMeasure1, UnitsOfMeasure UnitsOfMeasure2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(UnitsOfMeasure1, UnitsOfMeasure2))
                return true;

            // If one is null, but not both, return false.
            if (UnitsOfMeasure1 is null || UnitsOfMeasure2 is null)
                return false;

            if (UnitsOfMeasure1 is null)
                throw new ArgumentNullException(nameof(UnitsOfMeasure1),  "The given id tag info must not be null!");

            return UnitsOfMeasure1.Equals(UnitsOfMeasure2);

        }

        #endregion

        #region Operator != (UnitsOfMeasure1, UnitsOfMeasure2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="UnitsOfMeasure1">An id tag info.</param>
        /// <param name="UnitsOfMeasure2">Another id tag info.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (UnitsOfMeasure UnitsOfMeasure1, UnitsOfMeasure UnitsOfMeasure2)
            => !(UnitsOfMeasure1 == UnitsOfMeasure2);

        #endregion

        #endregion

        #region IEquatable<UnitsOfMeasure> Members

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

            if (!(Object is UnitsOfMeasure UnitsOfMeasure))
                return false;

            return Equals(UnitsOfMeasure);

        }

        #endregion

        #region Equals(UnitsOfMeasure)

        /// <summary>
        /// Compares two id tag infos for equality.
        /// </summary>
        /// <param name="UnitsOfMeasure">An id tag info to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(UnitsOfMeasure UnitsOfMeasure)
        {

            if (UnitsOfMeasure is null)
                return false;

            return String.Equals(Unit, UnitsOfMeasure.Unit, StringComparison.OrdinalIgnoreCase) &&
                   Multiplier.Equals(UnitsOfMeasure.Multiplier)                                 &&

                   ((CustomData == null && UnitsOfMeasure.CustomData == null) ||
                    (CustomData != null && UnitsOfMeasure.CustomData != null && CustomData.Equals(UnitsOfMeasure.CustomData)));

        }

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {

                return Unit.      GetHashCode() * 5 ^
                       Multiplier.GetHashCode() * 3 ^

                       (CustomData != null
                            ? CustomData.GetHashCode()
                            : 0);

            }
        }

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => String.Concat(Unit, " ( *10^", Multiplier, " )");

        #endregion

    }

}
