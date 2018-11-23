using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Evbpc.Strings
{
    [JsonConverter(typeof(ValidatedStringJsonNetConverter))]
    public abstract class ValidatedString
        : IComparable, IEnumerable, IEnumerable<char>, IComparable<string>, IComparable<ValidatedString>, IEquatable<string>, IEquatable<ValidatedString>, IXmlSerializable
    {
        protected abstract string ErrorRequirement { get; }
        protected Exception Exception => new ArgumentException($"The value must {ErrorRequirement}");
        
        public string Value { get; private set; }
        public int Length => Value.Length;
        public char this[int index] => Value[index];

        protected ValidatedString() { }
        public ValidatedString(string str)
        {
            Value = Validate(str);
        }
        
        private string Validate(string str) => IsValid(str) ? str : throw Exception;

        protected abstract bool IsValid(string str);

        public static implicit operator string(ValidatedString str) => str?.Value;
        public override bool Equals(object obj) => (Value == null && obj == null) || (Value?.Equals(obj) ?? false);
        public override int GetHashCode() => Value?.GetHashCode() ?? 0;
        public override string ToString() => Value?.ToString();

        int IComparable.CompareTo(object obj) => (Value == null && obj == null) ? 0 : ((IComparable)Value)?.CompareTo(obj) ?? 0;
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Value)?.GetEnumerator();
        public IEnumerator<char> GetEnumerator() => ((IEnumerable<char>)Value?.ToCharArray()).GetEnumerator();
        public int CompareTo(string other) => (Value == null && other == null) ? 0 : Value?.CompareTo(other) ?? other.CompareTo(Value);
        public int CompareTo(ValidatedString other) => (Value == null && other.Value == null) ? 0 : Value?.CompareTo(other.Value) ?? other.Value.CompareTo(Value);
        public bool Equals(string other) => (Value == null && other == null) || (Value?.Equals(other) ?? false);
        public bool Equals(ValidatedString other) => (Value == null && other.Value == null) || (Value?.Equals(other.Value) ?? false);
        
        public static bool operator ==(ValidatedString a, ValidatedString b) => a.Value == b.Value;
        public static bool operator !=(ValidatedString a, ValidatedString b) => a.Value != b.Value;

        public static int Compare(ValidatedString strA, ValidatedString strB) => string.Compare(strA.Value, strB.Value);
        [SecuritySafeCritical]
        public static int Compare(ValidatedString strA, ValidatedString strB, StringComparison comparisonType) => string.Compare(strA.Value, strB.Value, comparisonType);
        public static int Compare(ValidatedString strA, int indexA, ValidatedString strB, int indexB, int length) => string.Compare(strA.Value, indexA, strB.Value, indexB, length);
        [SecuritySafeCritical]
        public static int Compare(ValidatedString strA, int indexA, ValidatedString strB, int indexB, int length, StringComparison comparisonType) => string.Compare(strA.Value, indexA, strB.Value, indexB, length, comparisonType);

        public static int CompareOrdinal(ValidatedString strA, ValidatedString strB) => string.CompareOrdinal(strA.Value, strB.Value);
        [SecuritySafeCritical]
        public static int CompareOrdinal(ValidatedString strA, int indexA, ValidatedString strB, int indexB, int length) => string.CompareOrdinal(strA.Value, indexA, strB.Value, indexB, length);

        public static bool Equals(ValidatedString a, ValidatedString b) => string.Equals(a.Value, b.Value);
        [SecuritySafeCritical]
        public static bool Equals(ValidatedString a, ValidatedString b, StringComparison comparisonType) => string.Equals(a.Value, b.Value, comparisonType);

        XmlSchema IXmlSerializable.GetSchema() => null;
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            var isEmpty = reader.IsEmptyElement;
            reader.Read();
            string value = null;
            if (!isEmpty)
            {
                value = reader.Value;
            }
            Value = Validate(value);
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteValue(Value);
        }
    }
}
