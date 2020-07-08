using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.Json.Serialization;

namespace JsonStorageExamples
{
    class StorageExampleClass
    {
        public string TextField { get; set; }
        public int IntegerField { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DateField { get; set; }
    }
}
