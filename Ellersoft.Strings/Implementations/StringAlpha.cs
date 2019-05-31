using System;
using System.Collections.Generic;
using System.Text;

namespace Ellersoft.Strings
{
    public class StringAlpha
        : StringRegex
    {
        protected override string RegexValidation => "^[a-zA-Z]*$";
        protected override string ErrorRequirement => "contain only alphabetical (a-z) characters";
        protected override bool AllowNull => true;

        protected StringAlpha() { }
        public StringAlpha(string str) : base(str) { }

        public static explicit operator StringAlpha(string str) => new StringAlpha(str);
    }
}
