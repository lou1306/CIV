﻿using System;
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
            var process = new RelabeledProcess
            {
                Inner = Mock.Of<IProcess>(),
                Relabeling = new RelabelingFunction
                {
                    { "action", "relabeled" }
                }
            };
            var transitions = process.Transitions();

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

            var transitions = process.Transitions();
            Assert.Equal(0, transitions.Where(t => t.Label == action).Count());
            Assert.Equal(1, transitions.Where(t => t.Label == expectedRelabel).Count());
        }

    }
}
