﻿using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SlackNet
{
    class EnumNameConverter : JsonConverter
    {
        private readonly NamingStrategy _namingStrategy;
        public EnumNameConverter(NamingStrategy namingStrategy) => _namingStrategy = namingStrategy;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var enumValue = (Enum)value;
                var enumText = enumValue.ToString("G");
                if (char.IsNumber(enumText[0]) || enumText[0] == '-')
                    throw new JsonSerializationException(string.Format("Integer value {0} is not allowed.", CultureInfo.InvariantCulture, enumText));

                writer.WriteValue(_namingStrategy.GetPropertyName(enumText, false));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return IsNullable(objectType)
                    ? throw new JsonSerializationException(string.Format("Cannot convert null value to {0}.", CultureInfo.InvariantCulture, objectType))
                    : (object)null;

            try
            {
                if (reader.TokenType == JsonToken.String)
                    return ParseEnumName(UnderlyingType(objectType), reader.Value.ToString());
                if (reader.TokenType == JsonToken.Integer)
                    throw new JsonSerializationException(string.Format("Integer value {0} is not allowed.", CultureInfo.InvariantCulture, reader.Value));
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException(string.Format("Error converting value {0} to type '{1}'.", CultureInfo.InvariantCulture, reader.Value, objectType), ex);
            }

            throw new JsonSerializationException(string.Format("Unexpected token {0} when parsing enum.", CultureInfo.InvariantCulture, reader.TokenType));
        }

        private object ParseEnumName(Type type, string name) => 
            Enum.GetValues(type)
                .Cast<Enum>()
                .FirstOrDefault(e => _namingStrategy.GetPropertyName(e.ToString("G"), false) == name);

        public override bool CanConvert(Type objectType) => typeof(Enum).GetTypeInfo().IsAssignableFrom(UnderlyingType(objectType).GetTypeInfo());

        private static bool IsNullable(Type objectType)
        {
            var typeInfo = objectType.GetTypeInfo();
            return typeInfo.IsGenericType
                && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private static Type UnderlyingType(Type objectType) =>
            IsNullable(objectType)
                ? Nullable.GetUnderlyingType(objectType)
                : objectType;
    }
}
