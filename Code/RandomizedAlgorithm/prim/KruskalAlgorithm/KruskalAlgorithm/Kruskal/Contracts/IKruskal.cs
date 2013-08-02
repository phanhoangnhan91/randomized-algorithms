namespace Kruskal
{
    using System.Collections.Generic;

    public interface IKruskal
    {
        IList<Edge> Solve(IList<Edge> graph, out int totalCost);

        //IList<Edge> RandMST(IList<Edge> graph, IList<Vertex> vertices, out int totalCost);

        IList<Edge> Prim(IList<Edge> _graph, out int totalCost);
    }
}
