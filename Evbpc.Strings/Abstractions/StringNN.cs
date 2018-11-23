using System;
using System.Collections.Generic;
using System.Text;

namespace Evbpc.Strings
{
    public abstract class StringNN
        : RegexString
    {
        protected abstract int MinLength { get; }
        protected abstract int MaxLength { get; }
        protected override string RegexValidation => $"^.{{{MinLength},{MaxLength}}}$";
        protected override string ErrorRequirement => $"be between {MinLength} and {MaxLength} characters";
        protected override bool AllowNull => true;

        protected StringNN() { }
        public StringNN(string str) : base(str) { }
    }
}
