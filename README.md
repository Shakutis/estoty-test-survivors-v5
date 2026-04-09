- Two New Enemy Types & Difficulty Scaling
The first two tasks took a bit longer than expected since tasks didnt specify how exactly enemy spawning and scaling should work.
For difficulty scaling I initially went with a global battle-wide approach, but after thinking it through I moved the scaling config to EnemyConfig instead.
This way each enemy type scales independently.

- Experience & Leveling System
I kept this close to the existing patterns in the codebase — Experience and ExperienceOnPickUp are similar to Health and HealOnPickUp.
Experience lives on the hero as a component rather than a global service, keeping it self-contained and easy to extend.

- HUD
The existing HUD was polling every frame for all updates. I refactored it to event-driven since health, XP and kill count don't need to update every frame.

- What's still in progress
I didn't get to fully complete the LevelUpWindow and abilities system. I have the OnLevelUp event in place on the Experience component which would trigger the window,
and the foundation for the leveling system is there — but the ability cards, selection logic and the six abilities themselves still need implementing.
The plan was to use scriptable objects with abstract method to implement abilities effects where simple abilities like Health Up or Damage Up add a StatModifier,
while complex ones like Bouncing or Orbiting Projectiles add MonoBehaviour.

- Given more time
I would check assets and scenes for unneeded components, missing sprites, animator settings, replace unity text with Text Mesh Pro and add pooling.

Overall I really enjoyed working within the existing architecture and trying to extend it in a way that felt natural rather than bolted on.
Looking forward to feedback.
