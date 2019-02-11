using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progcube.Flow.NodeLibrary;
using Progcube.Flow.NodeLibraryImpl;
using System;

namespace Progcube.Flow.Tests
{
    [TestClass]
    public class NodeImplementationTests
    {
        [TestMethod]
        public void OneNode_NoInput_ExceptionThrown()
        {
            var node = new AdditionNodeImpl();

            Assert.ThrowsException<ArgumentNullException>(() => node.GetValue());
        }

        [TestMethod]
        public void OneNode_StaticValues()
        {
            var node = new AdditionNodeImpl();

            node.Inputs[0].SetValue(2);
            node.Inputs[1].SetValue(3);

            Assert.IsTrue(node.Inputs[0].IsStaticValue);
            Assert.IsNull(node.Inputs[0].AttachedNode);
            Assert.IsTrue(node.Inputs[1].IsStaticValue);
            Assert.IsNull(node.Inputs[1].AttachedNode);

            Assert.IsFalse(node.IsOutputAvailable);
            Assert.AreEqual(5, node.GetValue());
            Assert.IsTrue(node.IsOutputAvailable);
        }

        [TestMethod]
        public void TwoNodes_Node2InputLinkedToNode1Output()
        {
            var node1 = new AdditionNodeImpl();
            var node2 = new NopNodeImpl();

            node1.Inputs[0].SetValue(2);
            node1.Inputs[1].SetValue(3);

            Assert.IsNull(node2.Inputs[0].AttachedNode);
            node2.Inputs[0].LinkTo(node1);
            Assert.IsNotNull(node2.Inputs[0].AttachedNode);

            Assert.AreEqual(5, node2.GetValue());
        }
    }

    #region Dummy classes
    public class AdditionNode : INode
    {
        public string Name => "Addition";

        public IInput<int> Int1;
        public IInput<int> Int2;
        public IOutput<int> Result;
    }

    public class NopNode : INode
    {
        public string Name => "Nop";

        public IInput<object> In;
        public IOutput<object> Out;
    }

    // AdditionNode
    // Takes two numbers and adds them together.
    // 
    //       +----------+
    //       | Addition |
    //       +----------+
   	//       | In   Out |
    //   2 ->|0        0|-> 5
    //   3 ->|1         |
    //       +----------+
    //
    public class AdditionNodeImpl : NodeImplementation<AdditionNode>
    {
        public override void Evaluate()
        {
            if (!IsOutputAvailable)
            {
                Outputs[0] = (int)Inputs[0].Value + (int)Inputs[1].Value;
            }
        }
    }

    // NopNode
    // Does nothing (the output is identical to the input).
    // 
    //       +---------+
    //       |   Nop   |
    //       +---------+
    //       | In  Out |
    //   5 ->|0       0|-> 5
    //       +---------+
    //
    public class NopNodeImpl : NodeImplementation<NopNode>
    {
        public override void Evaluate()
        {
            if (!IsOutputAvailable)
            {
                Outputs[0] = Inputs[0].Value;
            }
        }
    }
    #endregion
}
