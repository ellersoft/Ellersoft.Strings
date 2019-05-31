using System;
using System.Collections.Generic;
using System.Text;

namespace Ellersoft.Strings.Abstractions
{
    public class StringValidatedComparer : IEqualityComparer<StringValidated>, IComparer<StringValidated>
    {
        bool IEqualityComparer<StringValidated>.Equals(StringValidated x, StringValidated y) => x.Equals(y);
        int IEqualityComparer<StringValidated>.GetHashCode(StringValidated obj) => obj.GetHashCode();
        int IComparer<StringValidated>.Compare(StringValidated strA, StringValidated strB) => StringValidated.Compare(strA, strB);
    }
}
