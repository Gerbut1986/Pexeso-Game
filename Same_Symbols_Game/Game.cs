namespace Same_Symbols_Game
{
    using System;
    using System.Collections;
    using System.Threading;

    class Game
    {
        int showedCards = 0, turnCounter = 0, size = 4;
        char[,] gameField;                
        Status[,] statusField;             // Field of chars for cards
        char backCard = '#';               // Field for status for every card
        Position turnedCard;               // Saves position of firts turned card
        char frontCard = 'A';              // Back side of the Card

        public Game()
        {
            gameField = new char[size, size];
            statusField = new Status[size, size];
            GiveCards();
        }

        public Game(int fieldSize)
        {
            size = fieldSize;
            gameField = new char[size, size];
            statusField = new Status[size, size];
            GiveCards();
        }

        void GiveCards()
        {
            // List of free positions on screen:
            ArrayList freePositions = new ArrayList();
            // Filling list:
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Position position = new Position(x, y);
                    freePositions.Add(position);
                    statusField[x, y] = Status.Closed;
                }
            }

            // Randomly chosing positions for card pairs and changing the letter behind:
            while (freePositions.Count >= 2)
            {
                Random numGen = new Random(Guid.NewGuid().GetHashCode());
                int number = numGen.Next(freePositions.Count);

                Position firstCard = (Position)freePositions[number];
                freePositions.Remove(firstCard);
                number = numGen.Next(freePositions.Count);
                Position secondCard = (Position)freePositions[number];
                freePositions.Remove(secondCard);

                gameField[firstCard.X, firstCard.Y] = frontCard;
                gameField[secondCard.X, secondCard.Y] = frontCard;
                frontCard++;
            }
        }

        // Draw cards on the screen:
        public void DrawScreen()
        {
            Console.Clear();

            // Turning and dissmising the cards:
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    switch (statusField[x, y])
                    {
                        case Status.Closed:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("\t\t\t" + backCard);
                            Console.ResetColor();
                            break;
                        case Status.Dismissed:
                            Console.Write("");
                            break;
                        case Status.Turned:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("\t\t\t" + gameField[x, y]);
                            Console.ResetColor();
                            break;
                    }
                }
                Console.WriteLine("\n\n\n\n\n");
            }
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Attemt: " + turnCounter);
            Console.ResetColor();
        }

        public bool AllTurned()
        {
            return showedCards == size * size;
        }

        public bool ValidCard(int x, int y)
        {
            return ((x >= 0 && x < size) && (y >= 0 && y < size) && (statusField[x, y] == Status.Closed));
        }

        public void TurnFirstCard(int x, int y)
        {
            turnedCard = new Position(x, y);
            statusField[x, y] = Status.Turned;
            turnCounter++;
            DrawScreen();
        }

        public void TurnSecondCard(int x, int y)
        {
            statusField[x, y] = Status.Turned;
            turnCounter++;
            DrawScreen();

            // When we selected 1 pair of a cards:
            Thread.Sleep(1500);  // Sleept for a 1,5 seconds

            // If we have pair of same cards:
            if (gameField[turnedCard.X, turnedCard.Y] == gameField[x, y])
            {
                statusField[turnedCard.X, turnedCard.Y] = Status.Dismissed;
                statusField[x, y] = Status.Dismissed;
                showedCards += 2;
            } 
            else  // If we have pair of different cards:
            {
                statusField[turnedCard.X, turnedCard.Y] = Status.Closed;
                statusField[x, y] = Status.Closed;
            }
        }
    }
}
