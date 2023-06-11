# Table of Contents

* [General info](#general-info)
* [Documentation](#documentation)
* [Functionality](#functionality)
* [Technologies](#technologies)
* [Setup](#setup)

# General-info

## Goal:
_You need to develop a fully functional 2D game. It could be Pac-Man, Sudoku, Tetris, etc. Additionally, you need to prepare project documentation (minimum 5 pages of A4) containing a UML diagram and a detailed description of classes along with their methods._

## My solution
The development of the classic game "Snake" using the C# programming language and the OpenGL library for graphics rendering. The provided code represents the implementation of the "Snake" game with support for multiplayer mode. Below is the documentation for each class and method, explaining their purpose and functionality.

# Documentation

### Class [`Program`](https://github.com/Bronid/SnakeGameOpenGL/blob/master/Program.cs)

This class contains the entry point of the program and methods to start the game and display the menu.

#### Method `StartGame()`
Starts the game by prompting the user to enter the width and height of the game map, the frame delay, and the multiplayer mode option. It creates an instance of `DisplayManager` and starts the game loop.

#### Method `Main()`
The main method of the program. Displays a welcome message and offers the user options to start the game or exit. It handles user input in a loop and performs the corresponding action.

### Class [`DisplayManager : GameWindow`](https://github.com/Bronid/SnakeGameOpenGL/blob/master/Display/DisplayManager.cs)

The `DisplayManager` class is responsible for managing the display and user input for a snake game. It inherits from the `GameWindow` class.

#### Constructors

- `DisplayManager(int _width, int _height, string _title, int _globalwidth, int _globalheight, int _delay, bool _multiplayerMode)`

  Creates a new instance of the `DisplayManager` class with the specified parameters.

  - `_width`: The width of the game window.
  - `_height`: The height of the game window.
  - `_title`: The title of the game window.
  - `_globalwidth`: The global width of the game field.
  - `_globalheight`: The global height of the game field.
  - `_delay`: The delay (in milliseconds) between each frame update.
  - `_multiplayerMode`: A boolean value indicating whether the game is in multiplayer mode.

#### Properties

- `static int GLOBAL_WIDTH`
  The global width of the game field.

- `static int GLOBAL_HEIGHT`
  The global height of the game field.

- `static int DELAY`
  The delay (in milliseconds) between each frame update.

- `static bool PAUSE`
  A boolean value indicating whether the game is paused.

- `static bool MULTIPLAYER_MODE`
  A boolean value indicating whether the game is in multiplayer mode.

- `static Field Field`
  The game field.

- `static SnakePlayer Snake`
  The main snake player.

- `static SnakePlayer Snake2`
  The second snake player (only available in multiplayer mode).

#### Methods

- `protected void Restart()`
  Restarts the game by creating a new game field and snake players. Resets the stopwatch.

- `protected override void OnLoad(EventArgs e)`
  Called when the game window is loaded. Sets the clear color and starts the stopwatch.

- `protected override void OnRenderFrame(FrameEventArgs e)`
  Called when a frame is rendered. Updates the game logic, draws the game field, and swaps the display buffers. If the game ends, displays the scores and restarts the game.

- `protected override void OnKeyDown(KeyboardKeyEventArgs e)`
  Called when a key is pressed. Handles the user input to control the snake players' movement and

 pause the game.

### Structure [`Block`](https://github.com/Bronid/SnakeGameOpenGL/blob/master/Snake/Block.cs)

The `Block` struct represents a single block in the game field of the Multiplayer Snake game. It defines the type of the block and, optionally, a custom color for rendering.

#### Properties

- `type`: Gets the type of the block, which is an instance of the `EnumBlockType` enumeration.
- `CustomColor`: Gets the custom color of the block represented by an array of three float values (red, green, blue) in the range [0, 1]. If no custom color is specified, it is set to `null`.

#### Constructors

- `Block(EnumBlockType _type, float[] customColor = null)`
  Initializes a new instance of the `Block` struct with the specified block type and custom color (optional).

#### Methods

- `ToString()`: Overrides the `ToString()` method and returns a string representation of the block based on its type.

- `DrawBlock(int x, int y)`: Draws the block at the specified coordinates (x, y) using OpenGL. The rendering color is determined based on the block's type or the custom color if specified.

### Enumeration [`EnumBlockType`](https://github.com/Bronid/SnakeGameOpenGL/blob/master/Snake/EnumBlockType.cs)

The `EnumBlockType` enumeration defines the possible types of blocks on the game field, such as:
- Border
- Food
- Snake body
- Snake head
- Empty space

### Enumeration [`EnumDirection`](https://github.com/Bronid/SnakeGameOpenGL/blob/master/Snake/EnumDirection.cs)

The `EnumDirection` enumeration defines the possible movement directions for the snake, namely:
- Up
- Down
- Right
- Left

### Class [`Field`](https://github.com/Bronid/SnakeGameOpenGL/blob/master/Snake/Field.cs)

The `Field` class represents the game field in the Multiplayer Snake game. It manages the blocks within the field and provides methods for drawing and manipulating the field. The entire field is stored in a matrix of `Block` structures.

#### Properties

- `containFood`: Indicates whether the field currently contains food (a block of type `EnumBlockType.Food`). It is initially set to `false`.

#### Constructors

- `Field(int fieldWidth, int fieldHeight)`
  Initializes a new instance of the `Field` class with the specified width and height.

#### Methods

- `DrawBlocks()`
  Draws all the blocks in the field using OpenGL. It iterates over the matrix of blocks, calls the `DrawBlock()` method of each block, and prints the string representation of each block in the console.

- `DrawFieldGrid()`
  Draws the grid lines of the field using OpenGL. It creates a grid pattern by drawing lines between each cell in the field.

- `FillMap()`
  Fills the matrix of blocks with initial values. It sets all the inner cells to `EnumBlockType.Space` and assigns `EnumBlockType.Border` to the boundary cells.

- `setToField(Block _block, int _x, int _y)`
  Sets the specified block at the given coordinates (_x, _y) in the field matrix.

- `getBlockByCoords(int _x, int _y) : Block`
  Retrieves the block at the specified coordinates (_x, _y) from the field matrix.

- `generateFood()`
  Generates a new food block (`EnumBlockType.Food`) in a random empty space within the field. It first identifies all the free space coordinates, randomly selects one of them, and assigns a new food block to that location. The `containFood` property is updated accordingly.

### Class [`SnakePlayer`](https://github.com/Bronid/SnakeGameOpenGL/blob/master/Snake/SnakePlayer.cs)

The `SnakePlayer` class represents a player-controlled snake in the Multiplayer Snake game. It manages the state and behavior of the snake, including its movement, growth, collision detection, and scoring.

#### Properties

- `Alive`: Indicates whether the snake is alive. It is initially set to `true`.
- `HeadCoords`: An array of integers representing the coordinates (x, y) of the snake's head.
- `TailCoords`: A queue of arrays of integers representing the coordinates (x, y) of the segments of the snake's tail.
- `CurrentDirection`: An enumeration value of type `EnumDirection` representing the current direction of the snake's movement.
- `DirectionChanged`: Indicates whether the direction of the snake has been changed since the last movement. It is initially set to `false`.
- `Score`: An integer representing the score of the player controlling the snake.
- `TailColor`: An array of floats representing the RGB color values for the snake's tail.
- `HeadColor`: An array of floats representing the RGB color values for the snake's head.

#### Constructors

- `SnakePlayer(int _initX, int _initY, int _initBodyLength, Field _field, float[] tailColor, float[] headColor)`

  Initializes a new instance of the `SnakePlayer` class with the specified initial coordinates, body length, field, tail color, and head color. It sets up the initial state of the snake by placing the head and tail blocks on the field and initializing the relevant properties.

#### Methods

- `Wipe()`

  Clears the state of the snake by setting the `HeadCoords` and `TailCoords` properties to `null`.

- `Move(Field _field)`

  Moves the snake by updating its head and tail positions on the field. It enqueues the current head coordinates to the `TailCoords` queue and updates the `HeadCoords` based on the current direction. The method also checks for collisions with the field boundaries or the snake's own tail. If a collision is detected, the snake is marked as not alive (`Alive = false`) and its state is wiped. If the snake eats food (a block of type `EnumBlockType.Food`), the score is increased, and the `containFood` flag of the field is set to `false`.

# Functionality

* Customization of the game field width, height, delay, and selection of game mode (1 or 2 players)
* Collecting food to increase the snake's length
* Collision detection with the world boundaries and the snake's tail

### Controls:
1. → ← ↓ ↑ - control the first player
2. W A S D - control the second player
3. SPACE - pause the game

# Technologies
* [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [OpenTK (OpenGL) V3.0 (!)](https://opentk.net/)

# Setup

1. Install this project via git
```
git clone https://github.com/Bronid/SnakeGameOpenGL.git
```
2. Install the OpenTK library via NuGet (version 3.0)
3. Run the [Program.cs](https://github.com/Bronid/SnakeGameOpenGL/blob/master/Program.cs) file using your IDE
4. Done =)
