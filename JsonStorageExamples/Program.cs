using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonStorageExamples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Create a collection to contain the sample objects
            List<IBaseInterface> collection = new List<IBaseInterface>();

            //Create a DerivedClass1 object and add to the collection
            DerivedClass1 derived1 = new DerivedClass1();
            derived1.BaseIntField = 1;
            derived1.DerivedClass1StringField = "Derived Class 1 Field Text";
            collection.Add(derived1);

            //Create a DerivedClass1 object and add to the collection
            DerivedClass2 derived2 = new DerivedClass2();
            derived2.BaseIntField = 2;
            derived2.DerivedClass2IntField = 44;
            collection.Add(derived2);

            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.WriteIndented = true;
            serializerOptions.Converters.Add(new DerivedClassConverter<IBaseInterface, List<IBaseInterface>>());

            //Save the list to disk
            string fileName = "DerivedClassCollection.json";
            using (FileStream fs = File.Create(fileName))
            {
                await JsonSerializer.SerializeAsync(fs, collection, serializerOptions);
            }

            //Read the List to read in the saved Json
            using (FileStream fs = File.OpenRead(fileName))
            {
                List<IBaseInterface> recoveredCollection = await JsonSerializer.DeserializeAsync<List<IBaseInterface>>(fs, serializerOptions);

                foreach (IBaseInterface item in recoveredCollection)
                {
                    Console.WriteLine(item);
                }
                //Output
                //DerivedClass1Field: BaseIntField = 1 DerivedClass1StringField = Derived Class 1 Field Text
                //DerivedClass2Field: BaseIntField = 2 DerivedClass2IntField = 44

            }

            Console.WriteLine("Press any key to exit...");
            Console.In.ReadLine();
        }
    }
}
