# Main Objective
Create a Tower Defence prototype, in continuous space, which enemies don't have a predefined path to player's base and the player can place towers, 
or obstacles, as much as he like, as long as it doesn't completely blocks all enemies paths to player's base.

![](https://github.com/PuceVinicius/TowerDefence/blob/main/Gifs/pathfinding.gif)

### Pathfinding
Since my goal was to use continuous space, and not discrete, the complexity was greatly increased. I tried several things and ways to look to
the problem, but I will focus on the best solution that I found and implemented, which is really close to a "perfect solution". As always,
I tried to solve the problem making the solution as frictionless as possible to the player, that said performance "spikes" or major weird 
gimmicks that could bother a lot the player were out of the table.

For the solution, I used mainly the Unity's NavMesh and its [Advanced Components](https://github.com/Unity-Technologies/NavMeshComponents),
that for some reason is kinda hidden from the users. To create the tower, first I check if the creation position is valid, using some trivial 
verification and the SamplePosition as the final one, in order to not place towers above the others or things like that. But for the second 
and most important part, I store the current NavMeshData, position a NavMeshVolumeModifier where I want to create a tower with same tower size 
(it modifies the NavMesh of its volume to a new Area type, which I setup to be an empty one), requests an async update on the NavMeshSurface to 
the NavMeshVolumeModifier be processed (it takes ~3 frames to finish on my environment), use the CalculatePath for all living enemies to check 
if this new possible tower would block someone's path, if its valid then position the tower where the player requested and, if not, use the
stored NavMeshData to rollback the NavMesh to the previous state. The idea to polish this even more was to use smoke VFX and carpentry SFX
while waiting for the async update, simulating the tower "building" and explaining the small delay to appear on the battlefield, but for time
reasons this could not me finished.

This solution required a vast amount of prototyping time, both thinking of ways to solve the problem and implementing/testing then. For example,
I had to use the NavMeshVolumeModifier instead of the regular NavMeshObstacle with carving option before de CalculatePath, because the
NavMeshObstacle naturally comes with a collider for NavMeshAgents and, even during this testing and preview time, the agents were colliding
with it.

### Base
Cloned my own UnityBoilerplate, [available here](https://github.com/PuceVinicius/UnityBoilerplate), and downgraded it to 2020.3, fixing all
missing elements.

### Architecture 
I used ECS for most part, this way it is less complex to handle a great amount of enemies and optimize it. On some occasions that I was low on 
time and had to focus on speed, I may have not used ECS. With more time, all those parts could be easily refactored.

For the scripts, I separated then in their own assemblies, creating a "Commons" for Scriptable Object, Enums, Datas and Consts of that assembly.
In addition, for the towers and the enemies, I created a base named "Unit", that is inherited by them and implemented as they wish. Not only,
"Tower" and "Enemy" were created to be as used as base for all different implementations, like "TowerFast" and "EnemyTough".

About the prefabs, it followed the same logic used on scripts, "Unit" as their primary base and prefab variants whenever was needed to implement
something new or differ from base. The first level was created using this organization too, specifying what would be generic for every level or
individual.

## How To Play
1. Clone the repository or download de .zip;
2. Open the Bootstrap.unity Scene and press play;
3. Left click the giant "Play" button on the Placeholder Main Menu;
4. Left click on the HUD buttons on the left to enable Tower placement preview;
5. Hold left click (just a little) on the battlefield to position your tower. If the tower appears after a couple of frames, it wasn't blocking 
the path of any enemies and is valid! Otherwise, you were trying to block someone;
6. And enjoy! Its not much, but you can definitely see the main objective being achieved and test it!;
