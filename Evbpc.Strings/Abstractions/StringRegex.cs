using System;
using System.Text.RegularExpressions;

namespace Evbpc.Strings
{
    public abstract class StringRegex
        : StringValidated
    {
        protected abstract string RegexValidation { get; }
        protected abstract bool AllowNull { get; }
        protected override string ErrorRequirement => $"match the Regular Expression: {RegexValidation}";

        private Regex _regex;

        protected StringRegex() { }
        public StringRegex(string str) : base(str) { }

        protected override bool IsValid(string str)
        {
            if (_regex == null) { _regex = new Regex(RegexValidation); };
            if (str == null) { return AllowNull; }
            return _regex.IsMatch(str);
        }
    }
}
