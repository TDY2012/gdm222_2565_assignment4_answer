using System;

class Program
{
    static void Main(string[] args)
    {
        int nodeCount = int.Parse(Console.ReadLine());

        Graph g = new Graph();

        for(int i=0; i<nodeCount; i++)
        {
            g.AddNode(0);
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

        g.ColorGraph();
        
        LinkedList<int> nodeList = g.GetAllNode();

        int max = nodeList.Get(0);
        int value;
        for(int i=1; i<nodeList.GetLength(); i++)
        {
            value = nodeList.Get(i);
            if(value > max)
            {
                max = value;
            }
        }

        Console.WriteLine(max + 1);
    }
}