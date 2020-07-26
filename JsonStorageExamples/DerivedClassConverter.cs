using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonStorageExamples
{
    public class DerivedClassConverter : JsonConverter<BaseClass>
    {
        enum TypeDiscriminator
        {
            DerivedClass1 = 1,
            DerivedClass2 = 2
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeof(BaseClass).IsAssignableFrom(typeToConvert);

        public override BaseClass Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            if (propertyName != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            BaseClass BaseClass = typeDiscriminator switch
            {
                TypeDiscriminator.DerivedClass1 => new DerivedClass1(),
                TypeDiscriminator.DerivedClass2 => new DerivedClass2(),
                _ => throw new JsonException()
            };

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return BaseClass;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "DerivedClass1Field":
                            string class1Field = reader.GetString();
                            ((DerivedClass1)BaseClass).DerivedClass1Field = class1Field;
                            break;
                        case "DerivedClass2Field":
                            string class2Field = reader.GetString();
                            ((DerivedClass2)BaseClass).DerivedClass2Field = class2Field;
                            break;
                        case "BaseIntField":
                            int intValue = reader.GetInt32();
                            BaseClass.BaseIntField = intValue;
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, BaseClass BaseClass, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (BaseClass is DerivedClass1 DerivedClass1)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.DerivedClass1);
                writer.WriteString("DerivedClass1Field", DerivedClass1.DerivedClass1Field);
            }
            else if (BaseClass is DerivedClass2 DerivedClass2)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.DerivedClass2);
                writer.WriteString("DerivedClass2Field", DerivedClass2.DerivedClass2Field);
            }

            writer.WriteNumber("BaseIntField", BaseClass.BaseIntField);

            writer.WriteEndObject();
        }
    }
}
