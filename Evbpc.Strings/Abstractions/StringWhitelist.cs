using System;
using System.Collections.Generic;
using System.Text;

namespace Evbpc.Strings
{
    public abstract class StringWhitelist
        : RegexString
    {
        private const string _special = @"[\^$.|?*+()";

        protected abstract char[] Whitelist { get; }
        protected override string RegexValidation => $"^[{CreateWhitelist(Whitelist)}]*$";
        protected override string ErrorRequirement => $"contain only the whitelisted characters: {CreateWhitelist(Whitelist)}";
        protected override bool AllowNull => true;

        protected StringWhitelist() { }
        public StringWhitelist(string str) : base(str) { }

        public static string CreateWhitelist(char[] whitelist)
        {
            var result = new StringBuilder(whitelist.Length);

            foreach (var c in whitelist)
            {
                if (_special.IndexOf(c) >= 0)
                {
                    result.Append($@"\{c}");
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}
