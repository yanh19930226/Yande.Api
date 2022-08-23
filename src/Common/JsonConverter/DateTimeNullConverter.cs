﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.JsonConverter
{
    public class DateTimeNullConverter : JsonConverter<DateTime?>
    {


        private readonly string formatString;
        public DateTimeNullConverter()
        {
            formatString = "yyyy/MM/dd HH:mm:ss";
        }

        public DateTimeNullConverter(string inFormatString)
        {
            formatString = inFormatString;
        }


        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return string.IsNullOrEmpty(reader.GetString()) ? default(DateTime?) : DateTime.Parse(reader.GetString()!);
        }


        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString(formatString));
        }
    }
}
