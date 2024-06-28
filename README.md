# Cubic-Run

Concept:
Cubic Duo Dash is an exhilarating infinite runner game where players control a unique character composed of two cubes stacked on top of each other. With a space in the middle, players must navigate through a series of obstacles, utilizing the gap between the cubes to avoid collisions and progress as far as possible. When the player crouches, the space between the cubes reduces giving the effect that the player is crouching. When the player jumps, the space between the two cubes increases giving the player a real time effect of jumping. Attached below is the current gameplay recorded in Unity editor. 

Application:
- Implemented touch controls by utilizing Unity's new input system to control the player movement. 
- There are 4 obstacle sections in the scene at the start of the game. As the player advances, new sections are spawned dynamically at the end of the last section. Each obstacle section has various obstacles on it that the player must avoid in order to advance in the game.
- Designed animation to the player cubes when the palyer is in idle state to get the sense that the player is breathing. Shown below is the player controller state chart. The player has three states. Player_idle_animation_clip is played on loop, while the Player_jump_animation and Player_Crouch_animation are played when the player takes jump or crouch actions respectively. They are controlled by triggers(Crouch and Jump) as shown on left side of the screenshot. Once any these two animations are played, the player goes back to idle state.
(https://github.com/siddhurc/Cubic-Run/assets/32571617/1031f27e-8e11-4a79-8f6d-75bcdf7a4604)
- Implemented animation event triggers as shown below to handle single player jump and crouch movements. The event resetActionInProgress() resets the flag of current animation in progress so that the player can make only a single jump or crouch move at a time. 
https://github.com/siddhurc/Cubic-Run/assets/32571617/2449445a-43a6-4423-81db-8d29fdeb2077
- Added volumetric fog to add a bit of visual appealing to the game.
- Implemented a dyunamic aspect ratio manager which manages the canvas resolution based on various screen sizes.

Gameplay Mechanics:
1. Simple Controls: Players can control the character by swiping on the screen. Swiping the screen up makes the character jump and swiping down makes the character crouch down.
2. Dynamic Obstacles: Various obstacles such as poles, barriers, fire hydrant. Players must time their movements carefully to pass through the gaps between the obstacles.
3. Power-Ups: (Currently In-Dev)Collectible power-ups provide temporary benefits such as shields, speed boosts, or temporary invincibility, enhancing the player's ability to overcome obstacles.
4. Increasing Difficulty: As players progress, the speed gradually increases, and the frequency and complexity of obstacles intensify, providing a greater challenge.
5. Multiplayer Mode: (Future implementations)Players can compete against friends or random opponents in real-time multiplayer matches, racing side by side on parallel tracks. The first player to reach a certain distance or survive the longest wins.

Visuals and Audio:
- The game features minimalist 3D graphics with vibrant colors and sleek designs for the cubes and obstacles.
- Dynamic background music intensifies as the gameplay becomes more fast-paced, adding to the sense of urgency and excitement.

Progression and Rewards:
- Players earn points based on the distance traveled and obstacles overcome. Points can be used to unlock new character skins, themes, or power-ups (In-Dev).
- Daily challenges and missions provide additional rewards, encouraging players to return to the game regularly.

Monetization Opportunities:
1. In-App Purchases: Offer in-app purchases for premium character skins, themes, and power-ups, allowing players to customize their experience and progress faster.
2. Ad Revenue: Implement rewarded video ads or interstitial ads that appear between gameplay sessions or as optional boosts, providing players with incentives to watch ads in exchange for in-game rewards.
3. Subscription Model: Introduce a subscription service that grants players access to exclusive content, bonus rewards, and ad-free gameplay on a monthly basis.
4. Seasonal Events: Host limited-time seasonal events with special challenges, themed content, and exclusive rewards, enticing players to spend money to participate and collect unique items.

Cubic Duo Dash offers a fresh and addictive gaming experience, combining simple controls, challenging obstacles, and competitive multiplayer gameplay, all wrapped in a visually stunning package.

Technologies used : Unity3d
Asset used : Starter Pack - Synty POLYGON - Stylized Low Poly 3D Art
https://assetstore.unity.com/packages/essentials/tutorial-projects/starter-pack-synty-polygon-stylized-low-poly-3d-art-156819

Unity editor gameplay:

https://github.com/siddhurc/Cubic-Run/assets/32571617/710180da-05d7-469b-bc27-6f41bb68eb08

