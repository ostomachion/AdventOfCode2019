using NUnit.Framework;
using AdventOfCode2019;

namespace AdventOfCode2019Tests
{
    public class Day4Tests
    {
        [Test]
        public void IsPossiblePasswordTest()
        {
            Assert.IsTrue(Day4.IsPossiblePassword(111111));
            Assert.IsFalse(Day4.IsPossiblePassword(223450));
            Assert.IsFalse(Day4.IsPossiblePassword(123789));
        }

        [Test]
        public void IsPossiblePasswordStrictTest()
        {
            Assert.IsTrue(Day4.IsPossiblePasswordStrict(112233));
            Assert.IsFalse(Day4.IsPossiblePasswordStrict(123444));
            Assert.IsTrue(Day4.IsPossiblePasswordStrict(111122));
        }
    }
}