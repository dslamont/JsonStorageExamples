using System;
using System.Collections.Generic;
using System.Text;

namespace JsonStorageExamples
{
    public class DerivedClass2 :BaseClass
    {
        public string DerivedClass2Field { get; set; }
        public DerivedClass2()
        {
            //Default constructor required for derserialization
        }
        public override string ToString()
        {
            return $"DerivedClass2Field: BaseIntField = {BaseIntField} DerivedClass2Field = {DerivedClass2Field}";
        }
    }
}
