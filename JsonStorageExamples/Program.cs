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
            //Create a DerivedClass1 object and serialize to disk
            DerivedClass1 derived1 = new DerivedClass1();
            derived1.BaseIntField = 1;
            derived1.DerivedClass1Field = "Derived Class 1";
            await SaveObject<DerivedClass1>("DerivedClass1.json", derived1);

            //Create a DerivedClass1 object and serialize to disk
            DerivedClass2 derived2 = new DerivedClass2();
            derived2.BaseIntField = 2;
            derived2.DerivedClass2Field = "Derived Class 2";
            await SaveObject<DerivedClass2>("DerivedClass2.json", derived2);
        }

        protected static async Task SaveObject<T>(string fileName, T objToSave)
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            serializerOptions.WriteIndented = true;

            using (FileStream fs = File.Create(fileName))
            {
                await JsonSerializer.SerializeAsync(fs, objToSave, serializerOptions);
            }

        }
    }
}
