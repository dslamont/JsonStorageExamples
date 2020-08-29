namespace JsonStorageExamples
{
    public class DerivedClass2 : IBaseInterface
    {
        public int DerivedClass2IntField { get; set; }
        public int BaseIntField { get; set; }

        public override string ToString()
        {
            return $"DerivedClass2: BaseIntField = {BaseIntField} DerivedClass2IntField = {DerivedClass2IntField}";
        }
    }
}
