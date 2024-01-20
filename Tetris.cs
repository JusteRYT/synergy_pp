using System;
using System.Threading;

class Tetris
{
    static void Main()
    {
        Console.Title = "Tetris";
        Console.WindowHeight = 30;
        Console.WindowWidth = 30;

        Game game = new Game();

        while (!game.GameOver)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                game.ProcessKey(key);
            }

            game.Update();
            game.Render();

            Thread.Sleep(100); // Задержка для управления скоростью игры
        }

        Console.Clear();
        Console.WriteLine("Game Over!");
        Console.ReadLine();
    }
}

class Game
{
    private const int Width = 10;
    private const int Height = 20;

    private int[,] field;
    private int[,] currentPiece;
    private int currentPieceX;
    private int currentPieceY;
    private bool gameOver;

    public bool GameOver => gameOver;

    public Game()
    {
        field = new int[Width, Height];
        currentPiece = GenerateRandomPiece();
        currentPieceX = Width / 2 - currentPiece.GetLength(0) / 2;
        currentPieceY = 0;
        gameOver = false;
    }

    public void ProcessKey(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.LeftArrow:
                MovePiece(-1, 0);
                break;
            case ConsoleKey.RightArrow:
                MovePiece(1, 0);
                break;
            case ConsoleKey.DownArrow:
                MovePiece(0, 1);
                break;
            case ConsoleKey.UpArrow:
                RotatePiece();
                break;
            case ConsoleKey.Escape:
                gameOver = true;
                break;
        }
    }

    public void Update()
    {
        MovePiece(0, 1);

        if (IsCollision())
        {
            PlacePieceOnField();
            CheckGameOver();
            currentPiece = GenerateRandomPiece();
            currentPieceX = Width / 2 - currentPiece.GetLength(0) / 2;
            currentPieceY = 0;
        }
    }

    public void Render()
    {
        Console.Clear();

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (field[x, y] == 1 || (x >= currentPieceX && x < currentPieceX + currentPiece.GetLength(0) &&
                                          y >= currentPieceY && y < currentPieceY + currentPiece.GetLength(1) &&
                                          currentPiece[x - currentPieceX, y - currentPieceY] == 1))
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }

    private void MovePiece(int deltaX, int deltaY)
    {
        currentPieceX += deltaX;
        currentPieceY += deltaY;

        if (IsCollision())
        {
            currentPieceX -= deltaX;
            currentPieceY -= deltaY;
        }
    }

    private void RotatePiece()
    {
        int[,] rotatedPiece = new int[currentPiece.GetLength(1), currentPiece.GetLength(0)];

        for (int x = 0; x < currentPiece.GetLength(0); x++)
        {
            for (int y = 0; y < currentPiece.GetLength(1); y++)
            {
                rotatedPiece[y, currentPiece.GetLength(0) - 1 - x] = currentPiece[x, y];
            }
        }

        if (!IsCollision(rotatedPiece, currentPieceX, currentPieceY))
        {
            currentPiece = rotatedPiece;
        }
    }

    private int[,] GenerateRandomPiece()
    {
        // В данном примере используется фигура I
        return new int[,]
        {
            { 1, 1, 1, 1 }
        };
    }

    private bool IsCollision(int[,] piece, int x, int y)
    {
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                if (piece[i, j] == 1 &&
                    (x + i < 0 || x + i >= Width || y + j >= Height || (y + j >= 0 && field[x + i, y + j] == 1)))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsCollision()
    {
        return IsCollision(currentPiece, currentPieceX, currentPieceY);
    }

    private void PlacePieceOnField()
    {
        for (int i = 0; i < currentPiece.GetLength(0); i++)
        {
            for (int j = 0; j < currentPiece.GetLength(1); j++)
            {
                if (currentPiece[i, j] == 1)
                {
                    field[currentPieceX + i, currentPieceY + j] = 1;
                }
            }
        }
    }

    private void CheckGameOver()
    {
        for (int x = 0; x < Width; x++)
        {
            if (field[x, 0] == 1)
            {
                gameOver = true;
                break;
            }
        }
    }
}
