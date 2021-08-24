using FluentAssertions;
using HackGuide.CmaUtil;
using Xunit;

namespace HackGuide.Tests
{
    public class CanGernerateCmaKey
    {
        private CmaKeys cma { get; set; } = new CmaKeys();

        [Theory]
        [InlineData("4c07029d8b12a520")]
        public void GenerateCmaKey(string aid)
        {
            var key = this.cma.GenerateKeyStr(aid);
            key = key.ToLower();

            key.Should().Be("373e02f6fe7f46ecffe65d898d1d86e6a4d144510301b99091f216eb717ec0f0");
        }
    }
}