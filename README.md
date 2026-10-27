# Factory Tycoon — Unity Test Assignment

A small factory management game developed in Unity as a test assignment.

The goal is to complete production orders, earn money, and upgrade equipment to improve production efficiency.

## Gameplay

The core gameplay loop consists of:

1. Accepting the current production order.
2. Starting production and waiting for the production timer to finish.
3. Receiving a monetary reward after completing the order.
4. Spending money on equipment upgrades.
5. Progressing through the available orders.

## Features

- **Production system** — timed production with progress tracking.
- **Order system** — multiple orders with configurable quantities, production durations, and rewards.
- **Economy system** — balance management, rewards, and purchase validation.
- **Equipment upgrades** — upgrade levels, costs, and increased production speed.
- **Save system** — local progress persistence using Unity PlayerPrefs.
- **UI** — current order information, balance, production progress, and upgrade controls.
- **Animated factory scene** — conveyor belt and mechanical inserters.

## Technical Implementation

The project is written in C# and uses Unity's component-based architecture.

The main systems are separated into individual MonoBehaviour components:

- `EconomySystem` — manages the player's balance and money transactions.
- `OrderManager` — manages the order sequence and coordinates production and rewards.
- `ProductionMachine` — controls production timing and progress.
- `MachineUpgradeSystem` — manages equipment levels, upgrade costs, and production speed.
- `FactoryUI` — updates the interface and handles player input.
- `SaveSystem` — saves and restores the player's progress using PlayerPrefs.

Orders are configured through ScriptableObject assets, allowing their parameters to be changed in the Unity Inspector without modifying the core gameplay code.

## Save System

The game stores the following data locally:

- Player balance.
- Equipment upgrade level.
- Current order index.

The saved progress is restored when the game starts. The current production timer is not persisted.

## How to Run

1. Clone or download this repository.
2. Open the project in Unity Hub using a compatible Unity version.
3. Open the main game scene.
4. Press **Play** to start the game.

## Controls

- Use the UI buttons to start production and purchase equipment upgrades.

## Future Improvements

- Add visual feedback for completed orders and purchased upgrades.
- Improve production resource movement along the conveyor.
- Add more production machines and order types.
- Expand save functionality to include additional gameplay state.
- Improve UI presentation and add sound effects.

## Technologies

- Unity
- C#
- MonoBehaviour
- ScriptableObject
- Coroutines
- PlayerPrefs

## Project Status

The project implements the core production, economy, order progression, and equipment upgrade gameplay loop.
