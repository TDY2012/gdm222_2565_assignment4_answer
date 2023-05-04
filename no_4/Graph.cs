class Graph
{
    private LinkedList<int> nodeList;
    private LinkedList<LinkedList<GraphEdge<int>>> edgeTable;

    public Graph()
    {
        this.nodeList = new LinkedList<int>();
        this.edgeTable = new LinkedList<LinkedList<GraphEdge<int>>>();
    }

    public void AddNode(int value)
    {
        this.nodeList.Add(value);
        this.edgeTable.Add(new LinkedList<GraphEdge<int>>());
    }

    public void InsertNode(int index, int value)
    {
        this.nodeList.Insert(index, value);
        this.edgeTable.Insert(index, new LinkedList<GraphEdge<int>>());

        LinkedList<GraphEdge<int>> graphEdgeList;
        GraphEdge<int> graphEdge;
        for(int i=0; i<this.edgeTable.GetLength(); i++)
        {
            graphEdgeList = this.edgeTable.Get(i);
            for(int j=0; j<graphEdgeList.GetLength(); j++)
            {
                graphEdge = graphEdgeList.Get(j);
                if(graphEdge.GetDstIndex() >= index)
                {
                    graphEdge.SetDstIndex(graphEdge.GetDstIndex() + 1);
                }
            }
        }
    }

    public void RemoveNode(int index)
    {
        this.nodeList.Remove(index);
        this.edgeTable.Remove(index);

        LinkedList<GraphEdge<int>> graphEdgeList;
        LinkedList<GraphEdge<int>> newGraphEdgeList;
        GraphEdge<int> graphEdge;
        for(int i=0; i<this.edgeTable.GetLength(); i++)
        {
            graphEdgeList = this.edgeTable.Get(i);
            newGraphEdgeList = new LinkedList<GraphEdge<int>>();
            for(int j=0; j<graphEdgeList.GetLength(); j++)
            {
                graphEdge = graphEdgeList.Get(j);
                if(graphEdge.GetDstIndex() < index)
                {
                    newGraphEdgeList.Add(new GraphEdge<int>(graphEdge.GetDstIndex()));
                }
                else if(graphEdge.GetDstIndex() > index)
                {
                    newGraphEdgeList.Add(new GraphEdge<int>(graphEdge.GetDstIndex() - 1));
                }
            }

            this.edgeTable.Remove(i);
            this.edgeTable.Insert(i, newGraphEdgeList);
        }
    }

    public void AddEdge(int srcIndex, int dstIndex)
    {
        GraphEdge<int> edgeTo = new GraphEdge<int>(dstIndex);
        GraphEdge<int> edgeFrom = new GraphEdge<int>(srcIndex);
        this.edgeTable.Get(srcIndex).Add(edgeTo);
        this.edgeTable.Get(dstIndex).Add(edgeFrom);
    }

    private void DepthFirstTraverse(int nodeIndex, ref int iteration, ref LinkedList<int> visitedNodeIndexList)
    {
        visitedNodeIndexList.Add(nodeIndex);
        iteration--;

        if(iteration <= 0)
        {
            return;
        }

        LinkedList<GraphEdge<int>> graphEdgeList = this.edgeTable.Get(nodeIndex);
        for(int i=0; i<graphEdgeList.GetLength(); i++)
        {
            int nextNodeIndex = graphEdgeList.Get(i).GetDstIndex();
            bool isVisited = false;
            for(int j=0; j<visitedNodeIndexList.GetLength(); j++)
            {
                if(nextNodeIndex == visitedNodeIndexList.Get(j))
                {
                    isVisited = true;
                    break;
                }
            }

            if(isVisited)
            {
                continue;
            }
            
            this.DepthFirstTraverse(nextNodeIndex, ref iteration, ref visitedNodeIndexList);
        }
    }

    public LinkedList<int> GetAllNode()
    {
        int nodeIndex = 0;
        int iteration = this.GetNodeCount();
        LinkedList<int> visitedNodeIndexList = new LinkedList<int>();

        while(nodeIndex < this.GetNodeCount() && iteration > 0)
        {
            this.DepthFirstTraverse(nodeIndex, ref iteration, ref visitedNodeIndexList);
            nodeIndex++;
        }
        
        LinkedList<int> nodeValueList = new LinkedList<int>();
        for(int i=0; i<visitedNodeIndexList.GetLength(); i++)
        {
            nodeValueList.Add(this.nodeList.Get(visitedNodeIndexList.Get(i)));
        }

        return nodeValueList;
    }

    public void ColorGraph()
    {
        int currentNodeValue;
        int adjacentNodeIndex;
        int adjacentNodeValue;
        for(int i=0; i<this.nodeList.GetLength(); i++)
        {
            currentNodeValue = this.GetNode(i);
            LinkedList<GraphEdge<int>> graphEdgeList = this.edgeTable.Get(i);
            for(int j=0; j<graphEdgeList.GetLength(); j++)
            {
                adjacentNodeIndex = graphEdgeList.Get(j).GetDstIndex();
                adjacentNodeValue = this.GetNode(adjacentNodeIndex);

                if(currentNodeValue == adjacentNodeValue)
                {
                    this.nodeList.Set(adjacentNodeIndex, adjacentNodeValue + 1);
                }
            }
        }
    }

    public int GetNode(int index)
    {
        return this.nodeList.Get(index);
    }

    public int GetNodeCount()
    {
        return this.nodeList.GetLength();
    }

    public int GetEdgeCount()
    {
        int edgeCount = 0;
        for(int i=0; i<this.edgeTable.GetLength(); i++)
        {
            edgeCount += this.edgeTable.Get(i).GetLength();
        }

        return edgeCount;
    }

    public override string ToString()
    {
        string msg = "";

        LinkedList<GraphEdge<int>> graphEdgeList;
        for(int i=0; i<this.nodeList.GetLength(); i++)
        {
            msg += string.Format(" {0}\n", this.GetNode(i));
            graphEdgeList = this.edgeTable.Get(i);
            for(int j=0; j<graphEdgeList.GetLength(); j++)
            {
                msg += string.Format("  --> {0}\n", this.GetNode(graphEdgeList.Get(j).GetDstIndex()));
            }
        }
        msg = string.Format("Graph(\n{0}\n)", msg);
        return msg;
    }
}