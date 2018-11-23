using System;
using System.Collections.Generic;
using System.Text;

namespace Evbpc.Strings
{
    public class NonEmptyString
        : ValidatedString
    {
        protected override string ErrorRequirement => "not be null, empty, or whitespace";

        protected NonEmptyString() { }
        public NonEmptyString(string str) : base(str) { }

        protected override bool IsValid(string str) => !string.IsNullOrWhiteSpace(str);
        public static explicit operator NonEmptyString(string str) => new NonEmptyString(str);
    }
}
