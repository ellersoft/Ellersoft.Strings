using System;
using System.Collections.Generic;
using System.Text;

namespace Ellersoft.Strings
{
    public abstract class StringNN
        : StringRegex
    {
        protected abstract int MinLength { get; }
        protected abstract int MaxLength { get; }
        protected override string RegexValidation => $"^.{{{MinLength},{MaxLength}}}$";
        protected override string ErrorRequirement => $"The value must be between {MinLength} and {MaxLength} characters";
        protected override bool AllowNull => true;

        protected StringNN() { }
        public StringNN(string str) : base(str) { }
    }
}
