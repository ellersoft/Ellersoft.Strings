﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ellersoft.Strings
{
    public abstract class StringWhitelistN_
        : StringWhitelist
    {
        protected abstract int MinLength { get; }
        protected override string RegexValidation => $"^[{CreateWhitelist(Whitelist)}]{{{MinLength},}}$";
        protected override string ErrorRequirement => $"The value must be no less than {MinLength} characters and {base.ErrorRequirement}";

        protected StringWhitelistN_() { }
        public StringWhitelistN_(string str) : base(str) { }
    }
}
