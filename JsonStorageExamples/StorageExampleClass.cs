using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JsonStorageExamples
{
    class StorageExampleClass
    {
        public string TextField { get; set; }
        public int IntegerField { get; set; }        
        public TestEnum EnumField { get; set; }
    }
}
