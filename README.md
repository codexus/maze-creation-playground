# Maze generation playground

Sample project to show different methods for maze generation.
At this point there are two algorithms implemented:

1. Recursive Backtracking
2. Hunt-and-Kill algorithm

The goal is to show the code structure and architecture therefore the visual part looks kind of dull.

The grid data is stored as bit flags and uses good'ol biwise flag checking to avoid boxing.
Reason why the bit flags where chosen is because they are lightweight and can be easly tranfered to backend.

App uses state machine architecture to separate the logic from the presentation side.
Views are driven by the view manager that could handle animations easily. 
The Cells are spawned using pool.

All the code was written by myself and the only 3rd party lib was TextMeshPro.
