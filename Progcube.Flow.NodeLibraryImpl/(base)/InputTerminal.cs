using Progcube.Flow.NodeLibrary;
using System;

namespace Progcube.Flow.NodeLibraryImpl
{
    /// <summary>
    /// A node input terminal that can contain a value or a reference to another node's output.
    /// </summary>
    public class InputTerminal
    {
        /// <summary>
        /// The type of the input value.
        /// </summary>
        public Type ValueType { get; internal set; }

        private object _value;
        public object Value
        {
            get
            {
                if (IsStaticValue)
                {
                    return _value;
                }

                // Not a static value, therefore we must have an node linked to get a value.
                if (AttachedNode == null)
                {
                    throw new ArgumentNullException("Input must be provided.");
                }

                return AttachedNode.GetValue(AttachedNodeOutputIndex);
            }
        }

        /// <summary>
        /// Whether this input's value is already available or must be computed.
        /// </summary>
        public bool IsStaticValue { get; private set; }

        /// <summary>
        /// The node attached to this input terminal.
        /// </summary>
        /// <remarks>The input will be obtained by evaluating the attached node. Null if IsStaticValue is true.</remarks>
        public INodeImplementation<INode> AttachedNode { get; private set; }

        /// <summary>
        /// When the input terminal is attached to a node that has many outputs, this is the index of the output to use.
        /// </summary>
        public int AttachedNodeOutputIndex { get; private set; }

        /// <summary>
        /// Attaches a node to this input terminal. The value of this input is going to be provided by the linked node's output.
        /// </summary>
        /// <param name="node">The node to link this input to.</param>
        /// <param name="outputIndex">The index of the linked node's output terminal to use.</param>
        public void LinkTo(INodeImplementation<INode> node, int outputIndex = 0)
        {
            AttachedNode = node;
            AttachedNodeOutputIndex = outputIndex;
            IsStaticValue = false;
        }

        public void SetValue(object value)
        {
            // Reset link
            AttachedNode = null;
            AttachedNodeOutputIndex = default(int);

            // Set value
            _value = value;
            IsStaticValue = true;
        }
    }
}
