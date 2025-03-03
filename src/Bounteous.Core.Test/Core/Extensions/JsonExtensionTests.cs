using Bounteous.Core.Extensions;
using Bounteous.Core.Validations;
using Xunit;

namespace Bounteous.Core.Test.Core.Extensions
{
    public class JsonExtensionTests
    {
        [Fact]
        public void Serialize()
        {
            var foo = new Foo { FirstName = "Al", LastName = "Bundy" };
            var toJson = foo.ToJson();
            var from = toJson.FromJson<Foo>();

            Validate.Begin()
                .IsNotNull(from, "from")
                .IsEqual(from.FirstName, foo.FirstName, "first")
                .IsEqual(from.LastName, foo.LastName, "las")
                .Check();
        }
    }

    public class Foo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}