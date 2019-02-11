using System;
using System.Collections.Generic;
using System.Linq;

namespace Progcube.Flow.NodeLibraryImpl.Npgsql
{
    public class NpgsqlCommandNodeImpl : NodeImplementation<NodeLibrary.Npgsql.NpgsqlCommandNode>
    {
        public List<string> Errors => new List<string>();

        public NpgsqlCommandNodeImpl()
        {
            //Out = new ComputedValue<string>();
        }

        public override void Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
