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
        
        public string String { get; private set; }
        public int Length => String.Length;
        public char this[int index] => String[index];

        protected ValidatedString() { }
        public ValidatedString(string str)
        {
            String = Validate(str);
        }
        
        private string Validate(string str) => IsValid(str) ? str : throw Exception;

        protected abstract bool IsValid(string str);

        public static implicit operator string(ValidatedString str) => str?.String;
        public override bool Equals(object obj) => (String == null && obj == null) || (String?.Equals(obj) ?? false);
        public override int GetHashCode() => String?.GetHashCode() ?? 0;
        public override string ToString() => String?.ToString();

        int IComparable.CompareTo(object obj) => (String == null && obj == null) ? 0 : ((IComparable)String)?.CompareTo(obj) ?? 0;
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)String)?.GetEnumerator();
        public IEnumerator<char> GetEnumerator() => ((IEnumerable<char>)String?.ToCharArray()).GetEnumerator();
        public int CompareTo(string other) => (String == null && other == null) ? 0 : String?.CompareTo(other) ?? other.CompareTo(String);
        public int CompareTo(ValidatedString other) => (String == null && other.String == null) ? 0 : String?.CompareTo(other.String) ?? other.String.CompareTo(String);
        public bool Equals(string other) => (String == null && other == null) || (String?.Equals(other) ?? false);
        public bool Equals(ValidatedString other) => (String == null && other.String == null) || (String?.Equals(other.String) ?? false);
        
        public static bool operator ==(ValidatedString a, ValidatedString b) => a.String == b.String;
        public static bool operator !=(ValidatedString a, ValidatedString b) => a.String != b.String;

        public static int Compare(ValidatedString strA, ValidatedString strB) => string.Compare(strA.String, strB.String);
        [SecuritySafeCritical]
        public static int Compare(ValidatedString strA, ValidatedString strB, StringComparison comparisonType) => string.Compare(strA.String, strB.String, comparisonType);
        public static int Compare(ValidatedString strA, int indexA, ValidatedString strB, int indexB, int length) => string.Compare(strA.String, indexA, strB.String, indexB, length);
        [SecuritySafeCritical]
        public static int Compare(ValidatedString strA, int indexA, ValidatedString strB, int indexB, int length, StringComparison comparisonType) => string.Compare(strA.String, indexA, strB.String, indexB, length, comparisonType);

        public static int CompareOrdinal(ValidatedString strA, ValidatedString strB) => string.CompareOrdinal(strA.String, strB.String);
        [SecuritySafeCritical]
        public static int CompareOrdinal(ValidatedString strA, int indexA, ValidatedString strB, int indexB, int length) => string.CompareOrdinal(strA.String, indexA, strB.String, indexB, length);

        public static bool Equals(ValidatedString a, ValidatedString b) => string.Equals(a.String, b.String);
        [SecuritySafeCritical]
        public static bool Equals(ValidatedString a, ValidatedString b, StringComparison comparisonType) => string.Equals(a.String, b.String, comparisonType);

        XmlSchema IXmlSerializable.GetSchema() => null;
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            var isEmpty = reader.IsEmptyElement;
            reader.Read();
            if (isEmpty) return;
            String = Validate(reader.Value);
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteValue(String);
        }
    }
}
