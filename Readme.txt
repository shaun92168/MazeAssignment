===========================================================================
===========================================================================

New release for assignment 3

Title : Maze Game

Release date : December 4, 2020

Author(s) : Jaedin Dhatt, Shaun Tseng, Sam Hadavi

Engine : Unity

Mode : Single player

Description : First person 3D maze game. Explore the labyrinth to find the
exit while avoiding an elusive enemy.

Controls :

W - Move forward

S - Move backward

A - Move Left

D - Move right

Mouse - Look around

Righ click - Fire a ball

Space - Toggle wall collision 

C - Change between day and night

F - Turn fog on/off

T - Turn flashlight on/off

M - Turn Background music on/off

Esc - Open a menu to save game

New Features:
1. Add the option for the user to specify “day” (bright ambient) or “night” (low ambient lighting) conditions using a vertex and fragment shader. The user should be able to toggle this with a key on the keyboard
2. Add the option to turn on a fog effect using a vertex and fragment shader. The user should be able to toggle this with a key on the keyboard
3. Add vertex and pixel shading to mimic a flashlight effect, using vertex and fragment shaders. The user should have a way of turning the flashlight on and off.
4. Add the ability to throw a ball at any direction, and make a sound effect if it hits a wall, the floor or the enemy object. If it hits a wall or the floor, it should bounce. It should only be “alive” for a set amount of time. If it hits the “enemy” object, it should update a score and the ball should disappear immediately. The enemy will die and disappear after being hit by the ball 3 times. After 5 seconds, the enemy will respawn somewhere random in the maze
5. Play a sound effect when the enemy dies.
6. Play a sound effect the enemy respawns somewhere in the maze.
7. If the enemy touches the player, the player dies and the game restarts
8. Change one of the walls to be a door that the user walks through to play a mini-game. The mini-game should be the Pong game from assignment 1. This should be a different scene, and swap back to the maze scene once the game is over. If the user enters the door again, the Pong mini-game should restart.
9. The user should be able to save the state of the game, including at least the location of the player and the enemy object and the score from hitting the enemy with a ball.
10. Play a sound effect whenever the user moves. The sound effect should mimic “walking” or footsteps.
11. Play a sound effect whenever the user collides with a wall.
12. Allow the user to start/stop some background music with the press of a mapped button
13. Now include a second piece of music in your game. One song should be playing during night mode and the other during day mode.
14. If the fog is turned on, change the volume of the music to half the volume it would be otherwise.
15. Modulate the music’s volume as the enemy object moves closer (louder music) or farther away.

===========================================================================
===========================================================================
