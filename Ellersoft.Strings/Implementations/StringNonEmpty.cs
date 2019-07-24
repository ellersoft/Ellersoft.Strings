using System;
using System.Collections.Generic;
using System.Text;

namespace Ellersoft.Strings
{
    public class StringNonEmpty
        : StringValidated
    {
        protected override string ErrorRequirement => "The value must not be null, empty, or whitespace";

        protected StringNonEmpty() { }
        public StringNonEmpty(string str) : base(str) { }

        protected override bool IsValid(string str) => !string.IsNullOrWhiteSpace(str);
        public static explicit operator StringNonEmpty(string str) => new StringNonEmpty(str);
    }
}
