using System;
using System.Collections.Generic;
using System.Text;

namespace Evbpc.Strings
{
    public class StringHex
        : RegexString
    {
        protected override string RegexValidation => "^[0-9a-fA-F]*$";
        protected override string ErrorRequirement => "be a hexadecimal number";
        protected override bool AllowNull => true;

        protected StringHex() { }
        public StringHex(string str) : base(str) { }

        public static explicit operator StringHex(string str) => new StringHex(str);
    }
}
