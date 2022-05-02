using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ConnpassAutomator.Domain.Model.Profile
{
    public class EventTitle
    {
        public string Value { get; init; }

        public EventTitle IncrimentVolNo()
        {
            var result = Regex.Replace(Value,
                @"-?\d+(?!.*\d)",
                match => Increment(match.Value));
            return new(result);
        }

        private string Increment(string text)
            => Format(Parse(text) + 1);

        private string Format(BigInteger value)
            => value.ToString("D");

        private BigInteger Parse(string text)
            => BigInteger.Parse(text, NumberStyles.AllowLeadingSign);

        public EventTitle(string value)
            => Value = value;
    }
}
