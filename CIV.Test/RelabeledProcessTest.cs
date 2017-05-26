using System;
using System.Linq;
using CIV.Helpers;
using CIV.Processes;
using Moq;
using Xunit;

namespace CIV.Test
{
    public class RelabeledProcessTest
    {
        [Theory]
        [InlineData("action", "renamed")]
        public void RenamedProcessHasRenamedTransitions(String action, String renamed)
        {
            var process = new RenamedProcess
            {
                Inner = Mock.Of<IProcess>(),
                Renamings = new RelabelingFunction
                {
                    { "action", "relabeled" }
                }
            };
            var transitions = process.Transitions();

            foreach (var t in transitions)
            {
                Assert.Equal(t.Process.GetType(), typeof(RenamedProcess));
            }
        }

        [Theory]
		[InlineData("action", "relabel", "relabel")]
		[InlineData("action", "'relabel", "'relabel")]
		[InlineData("'action", "relabel", "'relabel")]
		[InlineData("'action", "'relabel", "relabel")]
        public void RelabeledProcessRelabelsInputAsInput(
            string action,
            string relabel,
            string expectedRelabel
        )
        {
            var innerAction = action.IsOutput() ? action.Coaction() : action;

            var process = new RenamedProcess
            {
                Inner = Common.SetupMockProcess(innerAction),
                Renamings = new RelabelingFunction
                {
                    { action, relabel }
                }
            };

            var transitions = process.Transitions();
            Assert.Equal(0, transitions.Where(t => t.Label == action).Count());
            Assert.Equal(1, transitions.Where(t => t.Label == expectedRelabel).Count());
        }

    }
}
