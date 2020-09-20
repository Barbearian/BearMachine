"# BearMachine"
Bear Machine is a database.
It stores infomation in graph form.

When receive a graph input, it will map input graph with graphes in the memory pool.
Then with graphs in memory, it will complete the current graph.

Main challenges:
0, representing different type of data with graphs. 
    currently nodes are representiong features (color) and 
    edges are representing spatial (radius and distance) or temporal (distance) relationhip between nodes.
1, mapping between current graph and graphes in memory pool.
2, storing graphes in memory pool so that it makes mapping easier.
    attempting implementing hierarchicalization.
3, generating graph patterns given a set of graphes with similarity.
4, complete a graph with pattern
    fill colors on nodes,
    create new nodes and edges.
