using System;
using System.Collections.Generic;
using System.Text;

namespace Evbpc.Strings
{
    public class StringNum
        : RegexString
    {
        protected override string RegexValidation => "^[0-9]*$";
        protected override string ErrorRequirement => "contain only numeric (0-9) characters";
        protected override bool AllowNull => true;

        protected StringNum() { }
        public StringNum(string str) : base(str) { }

        public static explicit operator StringNum(string str) => new StringNum(str);
    }
}
