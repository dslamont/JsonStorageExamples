using System;
using System.Collections.Generic;
using System.Text;

namespace JsonStorageExamples
{
    public class DerivedClass1 : IBaseInterface    {

        public string DerivedClass1StringField { get; set; }
        public int BaseIntField { get; set; }

        public override string ToString()
        {
            return $"DerivedClass1: BaseIntField = {BaseIntField} DerivedClass1StringField = {DerivedClass1StringField}";
        }
    }
}
