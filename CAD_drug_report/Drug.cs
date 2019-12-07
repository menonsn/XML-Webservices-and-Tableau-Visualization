//C# code for Drug Data
namespace QuickTypeDrug
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Drug
    {
        [JsonProperty("address_x")]
        public string AddressX { get; set; }

        [JsonProperty("agency")]
        public Agency Agency { get; set; }

        [JsonProperty("arrival_time_primary_unit", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ArrivalTimePrimaryUnit { get; set; }

        [JsonProperty("beat")]
        public string Beat { get; set; }

        [JsonProperty("closed_time_incident", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ClosedTimeIncident { get; set; }

        [JsonProperty("create_time_incident")]
        public DateTimeOffset CreateTimeIncident { get; set; }

        [JsonProperty("cross_street_1", NullValueHandling = NullValueHandling.Ignore)]
        public string CrossStreet1 { get; set; }

        [JsonProperty("dispatch_time_primary_unit", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DispatchTimePrimaryUnit { get; set; }

        [JsonProperty("disposition_text", NullValueHandling = NullValueHandling.Ignore)]
        public string DispositionText { get; set; }

        [JsonProperty("event_number")]
        public string EventNumber { get; set; }

        [JsonProperty("incident_type_id")]
        public string IncidentTypeId { get; set; }

        [JsonProperty("phone_pickup_time")]
        public DateTimeOffset PhonePickupTime { get; set; }

        [JsonProperty("sna_neighborhood")]
        public string SnaNeighborhood { get; set; }

        [JsonProperty("community_council_neighborhood")]
        public string CommunityCouncilNeighborhood { get; set; }

        [JsonProperty("cfd_incident_type", NullValueHandling = NullValueHandling.Ignore)]
        public string CfdIncidentType { get; set; }

        [JsonProperty("cfd_incident_type_group", NullValueHandling = NullValueHandling.Ignore)]
        public string CfdIncidentTypeGroup { get; set; }

        [JsonProperty("latitude_x")]
        public string LatitudeX { get; set; }

        [JsonProperty("longitude_x")]
        public string LongitudeX { get; set; }

        [JsonProperty("district", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? District { get; set; }

        [JsonProperty("incident_type_desc", NullValueHandling = NullValueHandling.Ignore)]
        public string IncidentTypeDesc { get; set; }

        [JsonProperty("priority", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Priority { get; set; }

        [JsonProperty("priority_color", NullValueHandling = NullValueHandling.Ignore)]
        public string PriorityColor { get; set; }

        [JsonProperty("cpd_neighborhood", NullValueHandling = NullValueHandling.Ignore)]
        public string CpdNeighborhood { get; set; }
    }

    public enum Agency { Cfd, Cpd };

    public partial class Drug
    {
        public static List<Drug> FromJson(string json) => JsonConvert.DeserializeObject<List<Drug>>(json, QuickTypeDrug.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Drug> self) => JsonConvert.SerializeObject(self, QuickTypeDrug.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                AgencyConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class AgencyConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Agency) || t == typeof(Agency?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CFD":
                    return Agency.Cfd;
                case "CPD":
                    return Agency.Cpd;
            }
            throw new Exception("Cannot unmarshal type Agency");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Agency)untypedValue;
            switch (value)
            {
                case Agency.Cfd:
                    serializer.Serialize(writer, "CFD");
                    return;
                case Agency.Cpd:
                    serializer.Serialize(writer, "CPD");
                    return;
            }
            throw new Exception("Cannot marshal type Agency");
        }

        public static readonly AgencyConverter Singleton = new AgencyConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
