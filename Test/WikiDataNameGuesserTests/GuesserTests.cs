using WikiDataNameGuesser.Models;
using WikiDataNameGuesser.Guesser;
using Xunit;

namespace WikiDataNameGuesserTests
{
    public class GuesserTests
    {
        IGuesser _guesser = new Guesser();

        [Theory]
        #region cannot guess
        [InlineData(null)]  //no data at all
        [InlineData(null, "b1", "b1", "b1")]   //only surname given
        [InlineData(null, "b2 a2", "a2 b2")]    //label and birth disagree
        #endregion

        #region gets from label
        [InlineData("a3", "a3 b3")]    //picks first
        [InlineData("a4", "b4 a4", null, "b4")] //picks not family name
        [InlineData("a5", "b5 a5 c5", null, "b5")] //picks first not family name
        #endregion
        
        #region gets from birth name
        [InlineData("a6", null, "a6 b6")]    //picks first
        [InlineData("a7", null, "b7 a7",  "b7")] //picks not family name
        [InlineData("a8", null, "b8 a8 c8", "b8")] //picks first not family name
        #endregion

        #region already set
        [InlineData("a9", null, null, null, "a9")]
        [InlineData("a10", "b10 a10", null, null, "a10")]
        [InlineData("a11", null, "b11 a11",  null, "a11")]
        [InlineData("a12", "b12 c12", "b12 q12 c12", "b12", "a12")]
        #endregion
        public void GuessGivenNameTheory(string expectedGivenName, string label = null, string birthName = null, string familyName = null, string givenName = null)
        {
            var h = new Human
            {
                Label = label,
                BirthName = birthName,
                FamilyName = familyName,
                GivenName = givenName
            };

            var result = _guesser.GuessGivenName(h);

            Assert.Equal(expectedGivenName, result);
        }

        [Fact]
        public void GuessGivenName_handlesNull()
        {
            var result = _guesser.GuessGivenName(null);
            Assert.Null(result);
        }

        [Theory]
        #region cannot guess
        [InlineData(null)]  //no data at all
        [InlineData(null, "b1F", "b1F", null, "b1F")]   //only given name present
        [InlineData(null, "b2F a2F", "a2F b2F")]    //label and birth disagree
        #endregion

        #region gets from label
        [InlineData("b3F", "a3F b3F")]    //picks last
        [InlineData("a4F", "b4F a4F", null, null, "b4F")] //picks not given name
        [InlineData("a5F", "b5F a5F c5F", null, null, "c5F")] //picks last not given name
        #endregion
        
        #region gets from birth name
        [InlineData("b6F", null, "a6F b6F")]   //picks last
        [InlineData("a7F", null, "b7F a7F", null, "b7F")] //picks not given name
        [InlineData("a8F", null, "b8F a8F c8F", null, "c8F")] //picks last not given name
        #endregion

        #region already set
        [InlineData("a9F", null, null, "a9F")]
        [InlineData("a10F", "b10F a10F", null, "a10F")]
        [InlineData("a11F", null, "b11F a11F", "a11F")]
        [InlineData("b12F", "b12F c12F", "b12F q12F c12F", "b12F", "a12F")]
        #endregion
        public void GuessFamilyNameTheory(string expectedFamilyName, string label = null, string birthName = null, string familyName = null, string givenName = null)
        {
            var h = new Human
            {
                Label = label,
                BirthName = birthName,
                FamilyName = familyName,
                GivenName = givenName
            };

            var result = _guesser.GuessFamilyName(h);

            Assert.Equal(expectedFamilyName, result);
        }

        [Fact]
        public void GuessFamilyName_handlesNull()
        {
            var result = _guesser.GuessFamilyName(null);
            Assert.Null(result);
        }

        [Theory]
        #region cannot guess
        [InlineData(null, null)]  //no data at all
        [InlineData(null, null, "b2N a2N", "a2N b2N")]    //label and birth disagree
        [InlineData(null, "b1N", "b1N", "b1N", "b1N")]   //only surname given
        [InlineData("b1FN", null, "b1FN", "b1FN", null, "b1FN")]   //only given name present
        #endregion

        #region gets from label
        [InlineData("a3N", "b3N", "a3N b3N")]    //picks first
        [InlineData("a4N", "b4N", "b4N a4N", null, "b4N")] //picks not family name
        [InlineData("a5N", "b5N", "b5N a5N c5N", null, "b5N")] //picks first not family name
        [InlineData("b4FN", "a4FN", "b4FN a4FN", null, null, "b4FN")] //picks not given name
        [InlineData("c5FN", "a5FN", "b5FN a5FN c5FN", null, null, "c5FN")] //picks last not given name
        #endregion

        #region gets from birth name
        [InlineData("a6N", "b6N", null, "a6N b6N")]    //picks first
        [InlineData("a7N", "b7N", null, "b7N a7N", "b7N")] //picks not family name
        [InlineData("a8N", "b8N", null, "b8N a8N c8N", "b8N")] //picks first not family name
        [InlineData("b7FN", "a7FN", null, "b7FN a7FN", null, "b7FN")] //picks not given name
        [InlineData("c8FN", "a8FN", null, "b8FN a8FN c8FN", null, "c8FN")] //picks last not given name
        #endregion

        #region already set
        [InlineData("a9N", null, null, null, null, "a9N")]
        [InlineData(null, "a9FN", null, null, "a9FN")]
        [InlineData("a10N", "b10N", "b10N a10N", null, null, "a10N")]
        [InlineData("a12N", "b12N", "b12N c12N", "b12N q12N c12N", "b12N", "a12N")]
        #endregion
        public void FillNamesTheory(string expectedGivenName, string expectedFamilyName, string label = null, string birthName = null, string familyName = null, string givenName = null)
        {
            var h = new Human
            {
                Label = label,
                BirthName = birthName,
                FamilyName = familyName,
                GivenName = givenName
            };

            var result = _guesser.FillNames(h);

            Assert.Equal(h, result);
            Assert.Equal(expectedGivenName, result.GivenName);
            Assert.Equal(expectedFamilyName, result.FamilyName);
        }

        [Fact]
        public void FillNames_handlesNull()
        {
            var result = _guesser.FillNames(null);
            Assert.Null(result);
        }
    }
}
