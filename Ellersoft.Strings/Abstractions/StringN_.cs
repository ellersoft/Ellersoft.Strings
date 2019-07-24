using System;
using System.Collections.Generic;
using System.Text;

namespace Ellersoft.Strings
{
    public abstract class StringN_
        : StringRegex
    {
        protected abstract int MinLength { get; }
        protected override string RegexValidation => $"^.{{{MinLength},}}$";
        protected override string ErrorRequirement => $"The value must be no less than {MinLength} characters";
        protected override bool AllowNull => true;

        protected StringN_() { }
        public StringN_(string str) : base(str) { }
    }
}
