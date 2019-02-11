namespace Progcube.Flow.NodeLibrary.Npgsql
{
    /// <summary>
    /// Represents a SQL statement or function (stored procedure) to execute against a PostgreSQL database.
    /// </summary>
    public class NpgsqlCommandNode : INode
    {
        /// <inheritdoc/>
        public string Name => "NpgsqlCommand";

        /// <summary>
        /// The SQL text of the query.
        /// </summary>
        public IInput<string> CmdText;

        /// <summary>
        /// A NpgsqlConnection that represents the connection to a PostgreSQL server.
        /// </summary>
        public IInput<string/*NpgsqlConnection*/> Connection;

        /// <summary>
        /// An instance of NpgsqlCommand.
        /// </summary>
        public IOutput<string/*NpgsqlCommand*/> Out;
    }
}
