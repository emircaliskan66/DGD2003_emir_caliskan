# DGD2003_emir_caliskan

GAME DESIGN DOCUMENT: Find in the Kültür
Version: 1.0

Genre: First-Person Scavenger Hunt / Time-Trial

Platform: PC (Unity 6.3.2f1)

Theme: Speed, Focus, and Campus Life

1. EXECUTIVE SUMMARY
Find in the Kültür is a high-speed scavenger hunt game set within the halls of Istanbul Kültür University. The player is tasked with finding a specific list of misplaced items scattered across a campus floor before the timer hits zero. The game emphasizes spatial awareness, quick reflexes, and the ability to stay calm under the pressure of a ticking clock.

2. CORE GAMEPLAY MECHANICS
2.1. Movement & Controls
First-Person Navigation: Standard WASD controls for fluid movement through corridors and classrooms.

Sprint Mechanic: Players can hold ‘Shift’ to sprint, consuming a stamina bar that regenerates over time.

Interaction (E): Used to pick up items, open doors, and search through drawers or lockers.

2.2. Scavenger System
Item List: A dynamic HUD element displays the items the player needs to find (e.g., icons or silhouettes).

Randomized Spawning: Items are assigned to different "spawn points" each run, ensuring that players cannot simply memorize the locations and must actively search.

Visual Feedback: Items will have a subtle "glow" or outline when the player is looking directly at them from a close distance.

2.3. Time Management
The Countdown: The game starts with a fixed time limit.

Time Bonuses: Finding "Rare Items" or specific "Golden Objects" adds extra seconds to the clock.

Success Condition: All items on the list must be collected and the player must reach the "Exit Gate" before time expires.

3. OBJECTIVES & PROGRESSION
3.1. Stage 1: The Essentials
The player starts in a familiar section of the university (e.g., the main corridor). The goal is to collect four essential student items:

Student ID Card: Often found on desks or near the registrar's office.

Library Book: Hidden among bookshelves or on study tables.

USB Drive: A small, hard-to-spot object located near computer labs.

Takeaway Coffee: Usually left behind in seating areas or near lecture hall entrances.

4. WORLD DESIGN
Environment: A detailed 3D recreation of an Istanbul Kültür University floor, featuring realistic classrooms, labs, and student lounges.

Interactivity: High level of environmental interaction—players can open cabinets, move chairs, and check behind doors to find hidden items.

Atmosphere: A bright, academic setting that becomes increasingly stressful as the screen edges turn red when time is running out.

5. TECHNICAL SPECIFICATIONS
Render Pipeline: Universal Render Pipeline (URP) for optimized performance on PC.

Logic System: A C#-based inventory and timer manager that tracks item collection and game-over states.

Physics: Simple collision detection to prevent clipping through walls while maintaining high-speed movement.

6. ART & AUDIO
VFX: Particle effects (sparks or subtle light) when an item is collected. A pulsing post-processing effect for low-time warnings.

Audio: * Background Music: An ambient track that increases in tempo as the timer decreases.

Sound Effects: Satisfying "click" or "chime" sounds for item pickups and a loud "buzzer" for game over.