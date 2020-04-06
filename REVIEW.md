# Project Review

## Applicant name
Tomasz Banasiak
---
The endless faller project taught me quite a few things about Unity.

My biggest mention would be the Unity testing system with which I have never worked before and had to learn it from the beginning.
It wasn't so bad because I already had knowledge about Unit tests from different languages. (Java to be precise)
Still, I was only able to do some basic tests.

Apart from them also a new thing for me was the Animations. Specifically the fade-out/in effect.
Fortunately, they were easy to implement after fiddling with the animator tool for some time.

As for the programming patterns I used the singleton pattern for the SceneManager. I know that it's not the most liked programming pattern but in a project as small as this one the ease of connecting variables (like highscore) when changing the scenes it offers, justifies its use in my opinion.

The main logic of the game is held inside the levelManager class. It has a lot of methods that control the state of the game.
Like GameOver, Pause, Play, Reset. All things that can happen in a single level. It holds the highscore value and passes it to the gameManager if the player decides to return to the main menu.

The highscore is being loaded from file every time the user visits the main menu. It's also saved to file when leaving the level.

The blue platform indicates a platform that will make the player beat his highscore upon touching.

If I had to make this game more interesting I would create some kind of way for the player to accelerate himself while falling down. That would enable players to survive longer and it would engage them more. The simplest way to do that is to add speed when pressing the down arrow. But a different solution like mashing some buttons would also work.

All in all the project was very informative. The only thing I didn't do (apart from properly writing the tests) was the "Hey You just got a highscore" text on touching the blue platform. I hope to make more projects like this in the future.

<!-- Your review goes here -->
<!-- Explain why you did the things that way or any snippet that is word mentioning -->
<!-- If you had any issue and how you resolved them -->
