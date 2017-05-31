using System;
using System.Linq;
using CIV.Ccs;
using Xunit;

namespace CIV.Test
{
    public class RelabeledProcessTest
    {
        [Fact]
        public void RelabeledProcessHasRelabeledTransitions()
        {
            var process = new RelabeledProcess
            {
                Inner = Common.SetupMockProcess(),
                Relabeling = new RelabelingFunction
                {
                    { "action", "relabeled" }
                }
            };
            var transitions = process.GetTransitions();

            foreach (var t in transitions)
            {
                Assert.Equal(t.Process.GetType(), typeof(RelabeledProcess));
            }
        }

        [Theory]
        [InlineData("action", "relabel", "relabel")]
        [InlineData("action", "'relabel", "'relabel")]
        [InlineData("'action", "relabel", "'relabel")]
        [InlineData("'action", "'relabel", "relabel")]
        public void RelabeledProcessFollowsSemantics(
            string action,
            string relabel,
            string expectedRelabel
        )
        {
            var innerAction = action.IsOutput() ? action.Coaction() : action;

            var process = new RelabeledProcess
            {
                Inner = Common.SetupMockProcess(innerAction),
                Relabeling = new RelabelingFunction
                {
                    { action, relabel }
                }
            };

            var transitions = process.GetTransitions();
            Assert.Equal(0, transitions.Count(t => t.Label == action));
            Assert.Equal(1, transitions.Count(t => t.Label == expectedRelabel));
        }

        [Fact]
        public void RelabelingFunctionRefusesTau()
        {
            Assert.Throws<ArgumentException>(
                () => new RelabelingFunction
            {
                { Const.tau, "relabeled"}
            });
        }
    }
}
