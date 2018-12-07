using System;
using System.Collections.Generic;
using System.Text;

namespace Evbpc.Strings
{
    public class StringHexPrefix
        : StringRegex
    {
        protected override string RegexValidation => "^(0x|&H)?[0-9a-fA-F]*$";
        protected override string ErrorRequirement => "be a hexadecimal number (optional 0x or &H prefix)";
        protected override bool AllowNull => true;

        protected StringHexPrefix() { }
        public StringHexPrefix(string str) : base(str) { }

        public static explicit operator StringHexPrefix(string str) => new StringHexPrefix(str);
    }
}
