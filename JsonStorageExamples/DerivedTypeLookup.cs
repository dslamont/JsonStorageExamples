using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonStorageExamples
{
    public class DerivedTypeLookup
    {
        private static Dictionary<Type, string> _typesLookup;

        //Returns the text label to identify a Type
        public static string GetIdentifierByType(Type type)
        {
            string identifier = String.Empty;

            //Extract the text label from the lookup
            if (TypesLookup.TryGetValue(type, out var typeId))
            {
                identifier = typeId;
            }

            return identifier;
        }

        //Returns the Type specified by a label
        public static Type GetTypeByIdentifier(string typeId)
        {
            Type type = TypesLookup.FirstOrDefault(t => t.Value == typeId).Key;

            return type;
        }

        //Set up the lookup for Types/Labels
        public static Dictionary<Type, string> TypesLookup
        {
            get
            {
                if (_typesLookup == null)
                {
                    _typesLookup = new Dictionary<Type, string>();
                    _typesLookup.Add(typeof(DerivedClass1), "Derived Class 1");
                    _typesLookup.Add(typeof(DerivedClass2), "Derived Class 2");
                }

                return _typesLookup;
            }

        }

    }
}
