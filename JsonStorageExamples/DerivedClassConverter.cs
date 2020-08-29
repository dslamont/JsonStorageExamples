using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonStorageExamples
{
    public class DerivedClassConverter<TItem, TList> : JsonConverter<TList> where TItem : notnull where TList : IList<TItem>, new()
    {
        public override bool CanConvert(Type typeToConvert)
        {
            //Ensure the current Type is of interest
            bool canConvert = typeof(TList).IsAssignableFrom(typeToConvert);
      
            return canConvert;
        }


        public override TList Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType != JsonTokenType.StartArray)
            {
                //Not at the start so bomb out
                throw new JsonException();
            }

            //Create the list to be returned
            var results = new TList();

            //Read in the first element
            reader.Read(); 

            while (reader.TokenType == JsonTokenType.StartObject)
            { 
                //Read in the next element (should be the property name)
                reader.Read(); 

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    //Not a property name so bomb out
                    throw new JsonException();
                }

                //Read in the type identifier
                var typeKey = reader.GetString();
                
                //Advance to the next element which should be the start of the object
                reader.Read(); 

                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    //Not the start of the object so bomb out
                    throw new JsonException();
                }

                //Determine which type if Item we are going to read in
                Type concreteItemType = DerivedTypeLookup.GetTypeByIdentifier(typeKey);
                if (concreteItemType != null)
                {
                    //Deserialize the item letting System.Text.Json decide how to acheive this
                    var item = (TItem)JsonSerializer.Deserialize(ref reader, concreteItemType, options);
                    
                    //Add the retrieved item to the list
                    results.Add(item);
                }
                else
                {
                    //Unknown Item so bomb out
                    throw new JsonException();
                }

                //Advance to the end of the wrapper section
                reader.Read(); 
                reader.Read(); 
            }

            if (reader.TokenType != JsonTokenType.EndArray)
            {
                //No End Array so bomb out
                throw new JsonException();
            }

            //Return the retrieved list
            return results;
        }

        public override void Write(Utf8JsonWriter writer, TList items, JsonSerializerOptions options)
        {
            //Stat the Json array representing the List
            writer.WriteStartArray();

            //Loop through all the supplied items in the List
            foreach (var item in items)
            {
                //Start the item representation
                writer.WriteStartObject();

                //Determine the Type of the item 
                var itemType = item.GetType();

                //Determine the identifier we will use to identify the item
                string typeKey = DerivedTypeLookup.GetIdentifierByType(itemType);
                if(!String.IsNullOrEmpty(typeKey))
                {
                    //Write the item type identifier
                    writer.WritePropertyName(typeKey);

                    //Output the item letting System.Text.Json decide how to acheive this
                    JsonSerializer.Serialize(writer, item, itemType, options);
                }
                else
                {
                    //No indetifier found so bomb out
                    throw new JsonException();
                }

                //Close the item's representation
                writer.WriteEndObject();
            }

            //Close the List representation
            writer.WriteEndArray();
        }

    }
}
