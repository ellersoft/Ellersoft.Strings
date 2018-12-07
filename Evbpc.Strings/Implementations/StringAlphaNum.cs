using System;
using System.Collections.Generic;
using System.Text;

namespace Evbpc.Strings
{
    public class StringAlphaNum
        : StringRegex
    {
        protected override string RegexValidation => "^[a-zA-Z0-9]*$";
        protected override string ErrorRequirement => "contain only alphabetical (a-z) or numeric (0-9) characters";
        protected override bool AllowNull => true;

        protected StringAlphaNum() { }
        public StringAlphaNum(string str) : base(str) { }

        public static explicit operator StringAlphaNum(string str) => new StringAlphaNum(str);
    }
}
