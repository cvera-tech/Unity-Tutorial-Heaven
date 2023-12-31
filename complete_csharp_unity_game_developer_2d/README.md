# Complete C# Unity Developer 2D

This folder contains project files for the *Complete C# Unity Developer 2D* course by GameDev.tv.

https://www.udemy.com/course/unitycourse/

## Notes

As opposed to the [Game Kit Intro course](../unity_3d_video_game_kit_introduction), this one has some actual C# programming. As of the time of this writing, I have been writing code for over a decade, and I first cut my teeth on C# back in 2019 (four years ago 😲), so I can't really comment on how effective the material is at teaching complete beginners to programming. I will say, though, that the course is highly engaging. When I first started out, all I had was a copy of *HTML, XHTML, and CSS For Dummies*, the family laptop that we got from Circuit City, and vague web developer aspirations. Sure, I learned a lot, and my Neopets page looked spiffy, but it wasn't as fun as making a little pixel snowboarder faceplant after breaking the sound barrier doing backflips.

The course is split into multiple sections, each with a different project to complete. The projects all build upon knowledge and skills learned from previous projects.

Also, this course was updated some time ago to use Unity 2021.1. I used the latest LTS release available to me (2022.3.7f1), and I haven't noticed any significant changes, unlike the Game Kit Intro course.

## Projects

### 1. [Section 2: Delivery Driver](Section_2)

This is a top-down driving game where the player controls a car that delivers packages to customers. It's about as simple as games go, but the course managed to cover a lot of ground, including variables and variable types, methods, boolean logic, and if statements. As for the Unity-specific topics, the course covered Transforms, Colliders/Triggers, RigidBodies, and basic asset management.

Overall, I think this was a nice first project. It covers a lot of ground, and the resulting game is fun and has many opportunites for future improvements.

### 2. [Section 3: Snow Boarder](Section_3)

This is a side-scrolling action game where the player controls a snow boarder moving down a sloped terrain. At first, I thought that this would be a tricky project; I had some experience making a 2D platformer in Unity before where I remember running into some trouble with how the physics engine handled slopes. It turns out that Unity's surface effectors provide an easy (if unrealistic) way to have things slide around.

### 3. [Section 4: Quiz Master](Section_4)

This is a quiz game, plain and simple; however, creating it was anything but, mostly because my own prior experience in software development got the better of me.

This project introduces user interface development in Unity. We get to play with TextMeshPro, canvases, buttons, and images. All of that is great! UI is a critical part of games. The part of the project that I struggled with was the game architecture.

The instructor (Gary Pettie) wrote the game in a way where each game system was tightly coupled with several other systems. I tried to overlook this since this is a small game meant primarily to teach game programming basics, but at some point the little brain itch became too bothersome to ignore. I rewrote significant parts of the project to introduce a finite state machine.

I'm not entirely happy with the solution I came up with, but after spending several hours trying and failing to rearchitect the game to use a ScriptableObject-based game event system, I settled for what I had. On the plus side, I learned much about how to work with Unity's script lifecycle and about some neat features in C# that were added in the time since I last used it (like the `new()` instantiation shorthand, and `switch` expressions).

### 4. [Section 5: TileVania](Section_5)

Despite the title, this game is less a Metdroidvania and more a basic platformer. It is the largest project of the course in terms of lecture time and covers a wide range of topics including tilemaps, sprite animation, Unity's new input system, physics materials, and object persistence through scene loading.

Similar to the previous section, I modified the architecture of the game significantly compared to what was presented in the lectures. I discovered the [Unity Open Project](https://github.com/UnityTechnologies/open-project-1), and finally made sense of how to create and use a ScriptableObject-based event system. Scene loading and player score and health management were written to be event-based instead of relying on hard references between components. I also added a Scriptable Object to hold session data, which I was quite pleased with since it removed the need to have a singleton GameObject to keep track of data between scene loads.

Overall, I found this section to be both incredibly challenging and incredibly rewarding. Had I followed what Rick Davidson instructed, I might have saved myself a lot of trouble and finished much earlier. Slowing down and thinking carefully about the game's architecture was a better decision, I think, since I learned a lot about how to design components that work together without being tightly coupled. Besides, I think developing proper habits as early as possible is better than having to unlearn some bad habits borne of doing things the quick and dirty way.

### 5. [Section 6: Laser Defender](Section_6)

This section took the longest to complete not because of any appreciable increase in difficulty in the project, but moreso that I was becoming a bit bored of the material. The game is a top-down space-themed shooter. Among the concepts and features covered in the project are:

* Number clamping
* 2D path creation
* Game object instantiation at runtime with loops
* DontDestroyOnLoad and singletons
* Texture scrolling
* Background music

Much of these were already covered in the previous section, which made this section less engaging for me. Once again, I veered away from the lectures by using Scriptable Objects for an event system and data persistence instead of singleton game objects.

## Final Thoughts

This course was dense and overall quite fun to go through. None of the programming concepts presented were new to me, but I learned much about working with the Unity editor. I also branched off and learned how to make use of Scriptable Objects to reduce component coupling and to persist data between scenes.