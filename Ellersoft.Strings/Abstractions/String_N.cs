using System;
using System.Collections.Generic;
using System.Text;

namespace Ellersoft.Strings
{
    public abstract class String_N
        : StringRegex
    {
        protected abstract int MaxLength { get; }
        protected override string RegexValidation => $"^.{{0,{MaxLength}}}$";
        protected override string ErrorRequirement => $"be no more than {MaxLength} characters";
        protected override bool AllowNull => true;

        protected String_N() { }
        public String_N(string str) : base(str) { }
    }
}
