using System;
using System.Collections.Generic;
using System.Text;

namespace Evbpc.Strings
{
    public abstract class StringWhitelistN_
        : StringWhitelist
    {
        protected abstract int MinLength { get; }
        protected override string RegexValidation => $"^[{CreateWhitelist(Whitelist)}]{{{MinLength},}}$";
        protected override string ErrorRequirement => $"be no less than {MinLength} characters and {base.ErrorRequirement}";

        protected StringWhitelistN_() { }
        public StringWhitelistN_(string str) : base(str) { }
    }
}
