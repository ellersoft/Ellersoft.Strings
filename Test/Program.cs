using Evbpc.Strings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Test
{
    public class StringEmail : StringRegex
    {
        protected override string ErrorRequirement => "be a valid email of the format <example>@<example>.<com>";
        protected override string RegexValidation => @"^.+@.+\..+$";
        protected override bool AllowNull => false;
        protected override Exception Exception => new InvalidEmailException();

        protected StringEmail() { }
        public StringEmail(string str) : base(str) { }

        public static explicit operator StringEmail(string str) => new StringEmail(str);

        public class InvalidEmailException : FormatException
        {
            public InvalidEmailException() : base("The email provided was an invalid format.") { }
        }
    }

    public class String10 : StringWhitelist_N
    {
        protected override int MaxLength => 10;
        protected override char[] Whitelist => new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public String10(string str) : base(str)
        {

        }
    }

    public class Test
    {
        public StringEmail Email { get; set; }
    }

    class Program
    {
        static int _counter = 0;
        static void _try(Action func, Action<Exception> failure = null)
        {
            _counter++;
            Console.Write($"Attempt {_counter}: ");

            try
            {
                func();
            }
            catch (Exception e)
            {
                if (failure == null)
                {
                    Console.WriteLine("Try failed: " + e.ToString());
                }
                else
                {
                    failure(e);
                }
            }
        }
        static void _try(string str, Func<StringValidated> initializer) =>
            _try(
                () => { var result = initializer(); Console.WriteLine($"Success: {result}"); },
                (e) => Console.WriteLine($"Failed ({str}): {e.Message}"));

        static void _testString10(string str) => _try(str, () => new String10(str));
        static void _testStringHexPrefix(string str) => _try(str, () => new StringHexPrefix(str));
        static void _testStringAlpha(string str) => _try(str, () => new StringAlpha(str));
        static void _testStringNum(string str) => _try(str, () => new StringNum(str));
        static void _testStringAlphaNum(string str) => _try(str, () => new StringAlphaNum(str));

        static void Main(string[] args)
        {
            _testString10(null);
            _testString10("1234567890");
            _testString10("12345678901");
            _testStringHexPrefix("1234567890");
            _testStringHexPrefix("0x1234567890A");
            _testStringHexPrefix("1234567890G");
            _testStringAlpha("Abcd");
            _testStringAlpha("Abcd123");
            _testStringNum("123");
            _testStringNum("123Abc");
            _testStringAlphaNum("123");
            _testStringAlphaNum("123Abc");
            _testStringAlphaNum("123Abc ");

            var test = new Test() { Email = (StringEmail)"ebrown@example.com" };
            _try(() => Console.WriteLine((test.Email == "ebrown@example.com") + " (true)"));
            _try(() => Console.WriteLine((test.Email == "ebrown2@example.com") + " (false)"));

            var xmlSer = new XmlSerializer(test.GetType());
            byte[] buffer = null;

            _try(() =>
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    xmlSer.Serialize(ms, test);
                    buffer = ms.GetBuffer();
                }
                Console.WriteLine(new UTF8Encoding(false).GetString(buffer));
            });
            _try(() =>
            {
                using (var ms = new System.IO.MemoryStream(buffer))
                {
                    var result = (Test)xmlSer.Deserialize(ms);
                    Console.WriteLine(result.Email);
                }
            });

            string jsonResult = null;

            _try(() =>
            {
                jsonResult = JsonConvert.SerializeObject(test);
                Console.WriteLine(jsonResult);
                Console.WriteLine(JsonConvert.DeserializeObject<Test>(jsonResult).Email);
            });
            _try(() => { Console.WriteLine("ebrown@example."); test = new Test() { Email = (StringEmail)"ebrown@example." }; /* Purposefully throw exception */ });
            _try(() => { jsonResult = JsonConvert.SerializeObject(test).Replace("ebrown@example.com", "ebrown@example."); Console.WriteLine(jsonResult); Console.WriteLine(JsonConvert.DeserializeObject<Test>(jsonResult).Email); /* Purposefully throw exception */ });

            Console.WriteLine("Tests done.");
            Console.ReadLine();
        }
    }
}
