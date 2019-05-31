Ellersoft.Strings
===

A type-safe string validation framework.

---

Purpose
===

One of the biggest .NET shortcomings, in my opinion, is there's no way to guarantee the type-safety of strings to certain contracts. That is: how can we guarantee a string is an `Email`, for example?

This project seeks to provide a base for a solution to that problem. By utilizing this structure, it is trivial to create type-safe (runtime-verified, for now) string references. This can be used to guarantee that a string meets a certain contract.

Examples
===

One can implement the `ValidatedString` abstract class to create new validations, for example, the aforementioned `Email` string validation can be created with the following class which will inherit from `RegexString`:

    public class StringEmail : RegexString
    {
        protected override string ErrorRequirement => "be a valid email of the format <example>@<example>.<com>";
        protected override string RegexValidation => @"^.+@.+\..+$";
        protected override bool AllowNull => false;

        protected StringEmail() { }
        public StringEmail(string str) : base(str) { }
    }

This allows one to create a contract for said string, such that they may use `StringEmail` as the type for an object, such as the following:

    public class Test
    {
        public StringEmail Email { get; set; }
    }

Now, it requires one to always ensure that the string is safe.

Conversion from `string`
---

Additionally, if you don't want to have to manually call the `String<X>(string)` constructor, you can specify an `implicit` or `explicit` conversion operator on the `String<X>` type:

    public static explicit operator StringEmail(string str) => new StringEmail(str);

A basic template for it is:

    public static explicit operator String<...>(string str) => new String<...>(str);

Additional Notes
===

The following built-in string components are defined (also with the `explicit` operator):

- `NonEmptyString`: verifies that a string does not pass `System.String.IsNullOrWhitespace`;
- `StringAlpha`: verifies that a string is alpha-only (a-z in either case);
- `StringAlphaNum`: verifies that a string is alpha-numeric-only (0-9, a-z in either case);
- `StringHex`: verifies that a string is hexadecimal-only (0-9, a-f in either case);
- `StringHexPrefix`: verifies that a string is hexadecimal-only (0-9, a-f in either case) with an optional prefix (`0x` or `&H`);
- `StringNum`: verifies that a string is numeric-only (0-9);
