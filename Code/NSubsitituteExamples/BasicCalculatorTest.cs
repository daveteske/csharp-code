using System;
using Xunit;
using NSubstitute;
using ExampleForTesting;

namespace NSubsitituteExamples
{
    public class BasicCalculatorTest
    {
        [Fact]
        public void CanCreateASubForCalcAdd()
        {
            // arr
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);
            //act
            var res = calculator.Add(1, 2);

            //assert
            Assert.Equal(3, res);
            Assert.NotEqual(5, res);

            //Assert.Equal(4, calculator.Add(2, 2)); // Be carefull as with any mock it only knows what you tell it above.
        }

        [Fact]
        public void CanVerifyASubIsCalled()
        {
            var calc = Substitute.For<ICalculator>();

            calc.Add(3, 4).Returns(7);

            var res = calc.Add(3, 4);

            calc.Received().Add(3, 4);
            calc.DidNotReceive().Add(5, 7);
        }

        [Fact]
        public void CanCheckPropertiesTwoWays()
        {
            var calc = Substitute.For<ICalculator>();

            // using the method syntax
            calc.Mode.Returns("HEX");
            Assert.Equal("HEX", calc.Mode);

            // just assigning
            calc.Mode = "DEC";
            Assert.Equal("DEC", calc.Mode);
        }

        [Fact]
        public void CanSupportArgumentMatchingForReturns()
        {
            var calc = Substitute.For<ICalculator>();
            calc.Add(1, 2).Returns(3);

            calc.Add(1, 2);

            calc.Received().Add(1, Arg.Any<int>());
        }

        [Fact]
        public void CanSupportArgumentMatchingForSetup()
        {
            var calc = Substitute.For<ICalculator>();
            calc
                .Add(Arg.Any<int>(), Arg.Any<int>())
                .Returns(p => (int)p[0] + (int)p[1]);
            var x = 5;
            var y = 4;

            var res = calc.Add(x, y);

            calc.Received().Add(x, y);
            Assert.Equal(9, res);
        }

        [Fact]
        public void ReturnsMethodCanTakeSequenceOfValuesForAPrpoperty()
        {
            var calc = Substitute.For<ICalculator>();
            calc
                .Mode
                .Returns("HEX", "DEC", "RAD");

            Assert.Equal("HEX", calc.Mode);
            //Assert.Equal("RAD", calc.Mode);  // <-- this fails because it's out of sequence
            Assert.Equal("DEC", calc.Mode);
            Assert.Equal("RAD", calc.Mode);  // <-- its correct here
        }

        [Fact]
        public void CanHandleEventsAsWell()
        {
            var calc = Substitute.For<ICalculator>();
            bool eventWasRaised = false;
            calc.PowerUp += (sender, args) => eventWasRaised = true;

            calc.PowerUp += Raise.Event();
            Assert.True(eventWasRaised);


        }

        [Fact]
        public void CanDoNamedParameters()
        {
            var calc = Substitute.For<ICalculator>();
            calc
                .Add(y: Arg.Any<int>(), x: 10)
                .Returns(30);

            var ret = calc.Add(10, 3);

            calc.Received().Add(x: 10, y: 3);


        }
    }
}
