using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace XM.Shared.Core.Json
{
    [JsonConverter(typeof(JobLevelMapJsonConverter))]
    public class JobLevelMap : Dictionary<int, int> { }
    [JsonConverter(typeof(JobXPMapJsonConverter))]
    public class JobXPMap : Dictionary<int, int> { }

    public sealed class JobLevelMapJsonConverter : JsonConverter<JobLevelMap>
    {
        public override JobLevelMap Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject for JobLevelMap");
            var map = new JobLevelMap();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject) break;
                if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();
                var key = reader.GetString() ?? string.Empty;
                reader.Read();
                var value = reader.GetInt32();
                var code = KeyNameRegistry.ToCode("JobType", key) ?? (int.TryParse(key, out var k) ? k : 0);
                map[code] = value;
            }
            return map;
        }

        public override void Write(Utf8JsonWriter writer, JobLevelMap value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var (code, level) in value)
            {
                var name = KeyNameRegistry.ToName("JobType", code);
                writer.WriteNumber(name, level);
            }
            writer.WriteEndObject();
        }
    }

    public sealed class JobXPMapJsonConverter : JsonConverter<JobXPMap>
    {
        public override JobXPMap Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected StartObject for JobXPMap");
            var map = new JobXPMap();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject) break;
                if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();
                var key = reader.GetString() ?? string.Empty;
                reader.Read();
                var value = reader.GetInt32();
                var code = KeyNameRegistry.ToCode("JobType", key) ?? (int.TryParse(key, out var k) ? k : 0);
                map[code] = value;
            }
            return map;
        }

        public override void Write(Utf8JsonWriter writer, JobXPMap value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var (code, xp) in value)
            {
                var name = KeyNameRegistry.ToName("JobType", code);
                writer.WriteNumber(name, xp);
            }
            writer.WriteEndObject();
        }
    }
}


