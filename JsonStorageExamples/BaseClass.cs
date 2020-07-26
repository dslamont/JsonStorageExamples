using System;
using System.Collections.Generic;
using System.Text;

namespace JsonStorageExamples
{
    public class BaseClass
    {
        public int BaseIntField { get; set; }

        public BaseClass()
        {
            //Default constructor required for derserialization
        }

        public override string ToString()
        {
            return $"BaseClass: BaseIntField = {BaseIntField}";
        }
    }
}
