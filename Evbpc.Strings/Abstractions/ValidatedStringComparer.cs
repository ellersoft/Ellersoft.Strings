using System;
using System.Collections.Generic;
using System.Text;

namespace Evbpc.Strings.Abstractions
{
    public class ValidatedStringComparer : IEqualityComparer<ValidatedString>
    {
        public bool Equals(ValidatedString x, ValidatedString y) => x.Equals(y);
        public int GetHashCode(ValidatedString obj) => obj.GetHashCode();
    }
}
