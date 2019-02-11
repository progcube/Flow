using Progcube.Flow.NodeLibrary;
using System.Collections.Generic;
using System.Linq;

namespace Progcube.Flow.NodeLibraryImpl
{
    /// <summary>
    /// An implementation of a descendent of an INode.
    /// </summary>
    /// <typeparam name="T">The INode type that is implemented.</typeparam>
    public interface INodeImplementation<out T> where T : INode
    {
        /// <summary>
        /// Whether the node has been evaluated.
        /// </summary>
        bool IsOutputAvailable { get; }

        /// <summary>
        /// Return the value of an output of this node, evaluating it if needed.
        /// </summary>
        /// <param name="outputIndex">The node's output index desired.</param>
        object GetValue(int outputIndex = 0);
    }

    /// <summary>
    /// Represents an implementation of a specific INode type.
    /// </summary>
    public abstract class NodeImplementation<T> : INodeImplementation<T> where T : INode
    {

        public InputTerminal[] Inputs { get; private set; }
        public object[] Outputs { get; private set; }

        /// <summary>
        /// The list of validation errors for this node.
        /// </summary>
        List<string> Errors { get; }

        public NodeImplementation()
        {
            // Use reflexion to initialize all inputs and outputs to the correct type
            var props = typeof(T).GetProperties(); // get name
            var fields = typeof(T).GetFields();

            var inputFields = fields.Where(f => f.FieldType.GetGenericTypeDefinition() == typeof(IInput<>)).ToList();
            var outputFields = fields.Where(f => f.FieldType.GetGenericTypeDefinition() == typeof(IOutput<>)).ToList();
            
            Inputs = inputFields.Select(f => new InputTerminal { ValueType = f.FieldType.GetGenericArguments()[0] }).ToArray();
            Outputs = new object[outputFields.Count];
        }

        // TODO
        public void Emit() { }

        /// <summary>
        /// Run the node and fill its output(s) with the computed result.
        /// </summary>
        public abstract void Evaluate();

        /// <summary>
        /// Return the value of an output of this node, evaluating it if needed.
        /// </summary>
        /// <param name="outputIndex">The node's output index desired.</param>
        public object GetValue(int outputIndex = 0)
        {
            if (!IsOutputAvailable)
            {
                Evaluate();
            }

            return Outputs[outputIndex];
        }

        /// <summary>
        /// Whether the node has been evaluated.
        /// </summary>
        public bool IsOutputAvailable
        {
            get
            {
                return Outputs?.All(o => o != null) ?? false;
            }
        }
    }
}
