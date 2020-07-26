using System;
using System.Collections.Generic;
using System.Text;

namespace JsonStorageExamples
{
    public class DerivedClass1 : BaseClass
    {
        public string DerivedClass1Field { get; set; }

        public DerivedClass1()
        {
            //Default constructor required for derserialization
        }

        public override string ToString()
        {
            return $"DerivedClass1Field: BaseIntField = {BaseIntField} DerivedClass1Field = {DerivedClass1Field}";
        }
    }
}
