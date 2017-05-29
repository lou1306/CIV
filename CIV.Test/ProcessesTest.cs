using System;
using System.Linq;
using CIV.Ccs;
using CIV.Interfaces;
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
            var nil = new NilProcess();
            Assert.Equal(0, nil.Transitions().Count());
        }

        [Fact]
        public void PrefixProcessHasOneTransition()
        {
            var process = new PrefixProcess
            {
                Label = Const.tau,
                Inner = Mock.Of<IProcess>()
            };
            Assert.Equal(1, process.Transitions().Count());
        }

        [Fact]
        public void ParProcessHasParTransitions()
        {
            var transitions = SetupParProcess().Transitions();
            foreach (var t in transitions)
            {
                Assert.Equal(typeof(ParProcess), t.Process.GetType());
            }
        }

        [Theory]
        [InlineData("action")]
        [InlineData(Const.tau)]
        public void ParProcessFollowsSemantics(String action)
        {
            var transitions = SetupParProcess(action).Transitions().ToList();
            Assert.Equal(3, transitions.Count);
            switch (action)
            {
                case Const.tau:
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
            var restrictions = new RestrictionSet { innerAction };
            var process = new RestrictedProcess
            {
                Inner = Common.SetupMockProcess(innerAction),
                Restrictions = restrictions
            };
            var n = innerAction == Const.tau ? 1 : 0;
            Assert.Equal(n, process.Transitions().Count());

            process = new RestrictedProcess
            {
                Inner = SetupParProcess(innerAction),
                Restrictions = restrictions
            };

            n = innerAction == Const.tau ? 3 : 1;
            Assert.Equal(n, process.Transitions().Count());
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
