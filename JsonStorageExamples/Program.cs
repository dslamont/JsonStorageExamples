using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JsonStorageExamples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Create a sample object
            StorageExampleClass exampleObj = new StorageExampleClass();
            exampleObj.TextField = "Sample Text";
            exampleObj.IntegerField = 23;
            exampleObj.EnumField = TestEnum.Two;

            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.WriteIndented = true;

            //Save the object to disk
            string fileName = "SerializedObject.json";
            using (FileStream fs = File.Create(fileName))
            {
                await JsonSerializer.SerializeAsync(fs, exampleObj, serializerOptions);
            }

            //Create a new object from the saved Json
            using (FileStream fs = File.OpenRead(fileName))
            {
                StorageExampleClass readinObj = await JsonSerializer.DeserializeAsync<StorageExampleClass>(fs);
                Console.WriteLine($"Text Field = {readinObj.TextField}");
                Console.WriteLine($"Integer Field = {readinObj.IntegerField}");
                Console.WriteLine($"Enum Field = {readinObj.EnumField}");

                //Output
                //Text Field = Sample Text
                //Integer Field = 23
                //Enum Field = Two
            }
        }
    }
}
