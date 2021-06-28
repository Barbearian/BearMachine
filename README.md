# BearMachine
BearMachine is an graph based AI framework running on Unity. 
It explores the possibility of real time interactive learning via constructing state machine like graph structure by parsing sequential signals.

## Components:

### Grids: A 2D infinite Tape

a collection of nodes used to represent a cartisian space. 
node in grids could have values assigned to them such Integers, strings etc.


### Tracker: The pointer

a class used to track current state.
Instead of a point think it more like a mouse navigating in a maze.
It has a head direction, which means it could face to different directions.

### Graph: Instructions

a Graph used to tell tracker how to react to a state.
It not hard to see BearMachine is turling complete. 
You could read or write on grids, track could move around and change values on Components, and a directed graph will tell the tracker how to move around.

## Summary
Maybe people will wonder why am I reinventing the wheel.
However, it is not a reinvention but discovery on the associations between grids cells in ethortinal cortex and turling machine.
What if we were turling complete? 

