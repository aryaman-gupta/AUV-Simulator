# AUV-Simulator

![Screenshot](https://github.com/aryaman-gupta/AUV-Simulator/blob/master/Screenshots/Screenshot1.png)

A simulator used to test control algorithms written for an Autonomous Underwater Vehicle

This simulator was developed as a part of my work in Team Tiburon, the autonomous underwater vehicle team of NIT Rourkela. It is developed using Unity3D and written in C#.

The simulator works by communicating with a control algorithm through TCP sockets. The control algorithm may run on a seperate machine.

The simulator receives from the control algorithm, the values of the forces to be applied to the individual thrusters. It send simulated sensor data to the control algorithm as feedback. Amongst the sensors used are cameras. Camera images are sent to the control algorithm every frame. To ensure optimal communication frequency, edge detection is performed on the images before sending.

The code for simulating underwater forces like planing force, slamming force etc., i.e. the contents of ./Underwater Forces, is experimental and not currently in use. This code was taken from the tutorial posted by Erik Nordeus, 'Make a realistic boat in Unity with C#', and all rights for that part of the code belong to him.

http://www.habrador.com/tutorials/unity-boat-tutorial/

To use:
- Once the IP addresses and Port numbers are set correctly, run the simulator. Then, run the control algorithm, set up a client connection to the simulator, and run its server. Finally, press the space bar on the simulator to set up its client connection with the control algorithm.
- The control algorithm must send to the simulator forces to be applied at each thruster, as float values. The number, position, and orientation of thrusters on the vehicle can easily be changed.
- The simulator provides simulated sensor values as feedback. A total of 8 floating point values are sent, with two digit decimal places, three digits before the decimal, and the sign. This is mentioned for easy decoding of the feedback. The eight values are: the orientation of the vehicle in the x, y, and z axes respectively, the acceleration of the vehicle in the x, y, and z axes respectively, the depth of the vehicle under water, and the forward velocity of the vehicle in its local frame.
- To use camera images as feedback, uncomment the like '#usePics' in ControlThrusters.cs. You must decode the image from the values of the edges.
