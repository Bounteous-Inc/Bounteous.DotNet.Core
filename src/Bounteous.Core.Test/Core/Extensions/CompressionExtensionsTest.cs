using Bounteous.Core.Extensions;
using Bounteous.Core.Validations;
using Xunit;

namespace Bounteous.Core.Test.Core.Extensions
{
    [Collection("base")]
    public class CompressionExtensionsTest
    {
        private const string Original = "Hi there";

        [Fact]
        public void Base64()
            => Validate.Begin()
                .IsEqual(Original.ToBase64().FromBase64(), Original, "original").Check();
        
        [Fact]
        public void GetBytes()
            => Validate.Begin()
                .IsEqual(Original.GetBytes().FromBytes(), Original, "original").Check();
    }
}