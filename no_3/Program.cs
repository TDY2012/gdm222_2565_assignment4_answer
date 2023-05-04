using System;

class Program
{
    static void Main(string[] args)
    {
        int nodeCount = int.Parse(Console.ReadLine());

        Graph<int> g = new Graph<int>();

        for(int i=0; i<nodeCount; i++)
        {
            g.AddNode(i);
        }

        int srcIndex, dstIndex;
        while(true)
        {
            srcIndex = int.Parse(Console.ReadLine());
            if(srcIndex < 0 || srcIndex >= nodeCount)
            {
                break;
            }

            dstIndex = int.Parse(Console.ReadLine());
            if(dstIndex < 0 || dstIndex >= nodeCount)
            {
                break;
            }

            g.AddEdge(srcIndex, dstIndex);
        }

        int srcIndexCheck = int.Parse(Console.ReadLine());
        int dstIndexCheck = int.Parse(Console.ReadLine());

        if(g.IsReachable(srcIndexCheck, dstIndexCheck))
        {
            Console.WriteLine("Reachable");
        }
        else
        {
            Console.WriteLine("Unreachable");
        }
    }
}