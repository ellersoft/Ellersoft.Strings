using System;
using System.Collections.Generic;
using System.Text;

namespace Evbpc.Strings.Abstractions
{
    public class ValidatedStringComparer : IEqualityComparer<ValidatedString>, IComparer<ValidatedString>
    {
        bool IEqualityComparer<ValidatedString>.Equals(ValidatedString x, ValidatedString y) => x.Equals(y);
        int IEqualityComparer<ValidatedString>.GetHashCode(ValidatedString obj) => obj.GetHashCode();
        int IComparer<ValidatedString>.Compare(ValidatedString strA, ValidatedString strB) => ValidatedString.Compare(strA, strB);
    }
}
