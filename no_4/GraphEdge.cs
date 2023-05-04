class GraphEdge<T>
{
    private int dstIndex;

    public GraphEdge(int dstIndex)
    {
        this.dstIndex = dstIndex;
    }

    public int GetDstIndex()
    {
        return this.dstIndex;
    }

    public void SetDstIndex(int index)
    {
        this.dstIndex = index;
    }

    public override string ToString()
    {
        return string.Format("GraphEdge( --> {0} )", this.GetDstIndex());
    }
}