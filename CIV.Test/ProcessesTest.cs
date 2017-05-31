using System;
using System.Linq;
using CIV.Ccs;
using Moq;
using Xunit;

namespace CIV.Test
{
    public class ProcessesTest
    {

        [Fact]
        public void ConstTauMatchesLexerTau()
        {
            var lexerTau = CcsLexer
                .DefaultVocabulary
                .GetLiteralName(CcsLexer.TAU)
                .Replace("'", "");
            Assert.Equal(Const.tau, lexerTau);
        }

        [Fact]
        public void NilProcessHasNoTransitions()
        {
            var nil = NilProcess.Instance;
            Assert.Equal(0, nil.GetTransitions().Count());
        }

        [Fact]
        public void PrefixProcessHasOneTransition()
        {
            var process = new PrefixProcess
            {
                Label = Const.tau,
                Inner = Mock.Of<CcsProcess>()
            };
            Assert.Equal(1, process.GetTransitions().Count());
        }


        [Fact]
        public void ChoiceProcessEqualityIsSymmetric()
        {
            var p1 = new PrefixProcess { Label = "a", Inner = NilProcess.Instance };
            var p2 = new PrefixProcess { Label = "b", Inner = NilProcess.Instance };

			var choice1 = new ChoiceProcess { Inner1 = p1, Inner2 = p2 };
			var choice2 = new ChoiceProcess { Inner1 = p2, Inner2 = p1 };
			Assert.True(choice1.Equals(choice2));
            Assert.Equal(choice1.GetHashCode(), choice2.GetHashCode());
        }

        [Fact]
        public void PrefixProcessEquality()
        {
			var p1 = new PrefixProcess { Label = "a", Inner = NilProcess.Instance };
			var p2 = new PrefixProcess { Label = "b", Inner = NilProcess.Instance };
			var p3 = new PrefixProcess { Label = "a", Inner = NilProcess.Instance };

			Assert.True(p1.Equals(p3));
			Assert.False(p1.Equals(p2));
		}


        [Fact]
        public void ParProcessEqualityIsSymmetric()
        {
			var p1 = new PrefixProcess { Label = "a", Inner = NilProcess.Instance };
			var p2 = new PrefixProcess { Label = "b", Inner = NilProcess.Instance };

            var choice1 = new ParProcess { Inner1 = p1, Inner2 = p2 };
            var choice2 = new ParProcess { Inner1 = p2, Inner2 = p1 };
			Assert.True(choice1.Equals(choice2));
			Assert.Equal(choice1.GetHashCode(), choice2.GetHashCode());
        }


        [Fact]
        public void ParProcessHasParTransitions()
        {
            var transitions = SetupParProcess().GetTransitions();
            foreach (var t in transitions)
            {
                Assert.Equal(typeof(ParProcess), t.Process.GetType());
            }
        }

        [Theory]
        [InlineData("action")]
        [InlineData("tau")]
        public void ParProcessFollowsSemantics(String action)
        {
            // TODO: split into two facts.
            var transitions = SetupParProcess(action).GetTransitions().ToList();
            Assert.Equal(3, transitions.Count);
            switch (action)
            {
                case "tau":
                    Assert.Equal(3, transitions.Count(t => t.Label == action));
                    break;
                default:
                    Assert.Equal(1, transitions.Count(t => t.Label == action));
                    Assert.Equal(1, transitions.Count(t => t.Label == action.Coaction()));
                    Assert.Equal(1, transitions.Count(t => t.Label == Const.tau));
                    break;
            }
        }

        [Theory]
        [InlineData("action")]
        [InlineData("'action")]
        public void RestrictedProcessFollowsSemantics(String innerAction)
        {
            // TODO: split into facts, add tau case
            var restrictions = new RestrictionSet { innerAction };
            var process = new RestrictedProcess
            {
                Inner = Common.SetupMockProcess(innerAction),
                Restrictions = restrictions
            };
            var n = innerAction == Const.tau ? 1 : 0;
            Assert.Equal(n, process.GetTransitions().Count());

            process = new RestrictedProcess
            {
                Inner = SetupParProcess(innerAction),
                Restrictions = restrictions
            };

            n = innerAction == Const.tau ? 3 : 1;
            Assert.Equal(n, process.GetTransitions().Count());
        }

        [Fact]
        public void RestrictionSetRefusesTau()
        {
            Assert.Throws<ArgumentException>(
                () => new RestrictionSet { Const.tau }
            );
        }


        /// <summary>
        /// Setup a ParProcess where inner processes can evolve together.
        /// </summary>
        /// <returns>The par process.</returns>
        ParProcess SetupParProcess(String action = "action")
        {
            return new ParProcess
            {
                Inner1 = Common.SetupMockProcess(action),
                Inner2 = Common.SetupMockProcess(action.Coaction())
            };
        }

    }
}
