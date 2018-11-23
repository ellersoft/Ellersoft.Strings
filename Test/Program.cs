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
    public class StringEmail : RegexString
    {
        protected override string ErrorRequirement => "be a valid email of the format <example>@<example>.<com>";
        protected override string RegexValidation => @"^.+@.+\..+$";
        protected override bool AllowNull => false;

        protected StringEmail() { }
        public StringEmail(string str) : base(str) { }

        public static explicit operator StringEmail(string str) => new StringEmail(str);
    }

    public class String10 : StringWhitelist_N
    {
        protected override int MaxLength => 10;
        protected override char[] Whitelist => new [] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

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
        static void _try(string str, Func<ValidatedString> initializer)
        {
            try
            {
                var result = initializer();
                Console.WriteLine($"Success: {result}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Failed ({str}): {e.Message}");
            }
        }

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
            Console.WriteLine(test.Email == "ebrown@example.com");
            Console.WriteLine(test.Email == "ebrown2@example.com");
            var xmlSer = new XmlSerializer(test.GetType());
            byte[] buffer;
            using (var ms = new System.IO.MemoryStream())
            {
                xmlSer.Serialize(ms, test);
                buffer = ms.GetBuffer();
            }
            Console.WriteLine(new UTF8Encoding(false).GetString(buffer));
            using (var ms = new System.IO.MemoryStream(buffer))
            {
                var result = (Test)xmlSer.Deserialize(ms);
                Console.WriteLine(result.Email);
            }
            var jsonResult = JsonConvert.SerializeObject(test);
            Console.WriteLine(jsonResult);
            Console.WriteLine(JsonConvert.DeserializeObject<Test>(jsonResult).Email);
            
            Console.ReadLine();
        }
    }
}
