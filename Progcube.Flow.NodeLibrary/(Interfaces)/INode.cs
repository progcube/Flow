namespace Progcube.Flow.NodeLibrary
{
    /// <summary>
    /// Represents a node definition.
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// The display name of the node.
        /// </summary>
        string Name { get; }
    }
}
