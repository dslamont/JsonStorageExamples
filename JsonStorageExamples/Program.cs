using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            //Create a collection to contain the sample objects
            List<BaseClass> collection = new List<BaseClass>();

            //Create a DerivedClass1 object and add to the collection
            DerivedClass1 derived1 = new DerivedClass1();
            derived1.BaseIntField = 1;
            derived1.DerivedClass1Field = "Derived Class 1";
            collection.Add(derived1);

            //Create a DerivedClass1 object and add to the collection
            DerivedClass2 derived2 = new DerivedClass2();
            derived2.BaseIntField = 2;
            derived2.DerivedClass2Field = "Derived Class 2";
            collection.Add(derived2);

            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.WriteIndented = true;
            serializerOptions.Converters.Add(new DerivedClassConverter());

            //Save the list to diesk
            string fileName = "DerivedClassCollection.json";
            using (FileStream fs = File.Create(fileName))
            {
                await JsonSerializer.SerializeAsync(fs, collection, serializerOptions);
            }

            //Read the List to read in the saved Json
            using (FileStream fs = File.OpenRead(fileName))
            {
                List<BaseClass> recoveredCollection = await JsonSerializer.DeserializeAsync<List<BaseClass>>(fs, serializerOptions);

                foreach (BaseClass item in recoveredCollection)
                {
                    Console.WriteLine(item);
                }
            }
            //Output
            //DerivedClass1Field: BaseIntField = 1 DerivedClass1Field = Derived Class 1
            //DerivedClass2Field: BaseIntField = 2 DerivedClass2Field = Derived Class 2
        }
    }
}
