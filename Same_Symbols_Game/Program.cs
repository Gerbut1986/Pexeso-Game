namespace Same_Symbols_Game
{
    using System;

    class Program
    {  
        static Game gameScreen; // Representation of GameScreen class

        static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t\t\t\t\t\t Welcome in The Mind Game");
                Console.ResetColor();
                Console.WriteLine(new string('-', 120));
                Console.WriteLine("\t\t\t\t\t\t   1 - Standard game");
                Console.WriteLine("\t\t\t\t\t\t   2 - Individual game");
                Console.WriteLine("\t\t\t\t\t\t   3 - About");
                Console.WriteLine("\t\t\t\t\t\t   0 - Exit.\n");
                Console.WriteLine(new string('-', 120));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n\t\t\t\t\t        Please make your choice..\n\n");
                Console.Write("\n\t\t\t\t\t\t\t   ");
                Console.ResetColor();
                choice = int.Parse(Console.ReadLine());  // User input without showing on the screen:
                switch (choice)
                {
                    case 1:
                        gameScreen = new Game(); // Game by Default (4 X 4)
                        Play();
                        break;
                    case 2:
                        Console.Clear();
                        int gameFieldSize = 0;
                        char key = '0';
                        // Until user ended choosing:
                        do
                        {
                            Console.WriteLine("\t\t\t\t\t      How many cards you wanna play?");
                            Console.WriteLine("\n\t\t\t\t\t\t  Press 'a' for 4 cards.");
                            Console.WriteLine("\n\t\t\t\t\t\t  Press 'b' for 16 cards.");
                            Console.WriteLine("\n\t\t\t\t\t\t  Press 'c' for 36 cards.");
                            Console.WriteLine("\n\t\t\t\t\t\t  Press 'n' - Go Back");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("\n\t\t\t\t\t        Please make your choice..\n\n");
                            Console.Write("\n\t\t\t\t\t\t\t   ");
                            Console.ResetColor();
                            key = char.Parse(Console.ReadLine());
                            switch (key)
                            {
                                case 'a': gameFieldSize = 2; break;
                                case 'b': gameFieldSize = 4; break;
                                case 'c': gameFieldSize = 6; break;
                                case 'n': break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nWrong key.");
                                    Console.ResetColor();
                                    break;
                            }

                            gameScreen = new Game(gameFieldSize);
                            Play();
                        } while (key != 'n');
                        break;
                    case 3:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n\n\n\n\t\t\t\t       This Application Writing by ---  Andriy Gerbut  ---                              ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\n\n\t\t\t\t\t       Press any Key to Continue..\n\n");
                        Console.Write("\n\t\t\t\t\t\t\t   ");
                        Console.ReadKey();
                        break;
                    default: return;
                }
            } while (choice != 0);

            // End of game:
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n\n\t\t\t\t\t      Congratulations, You WIN!!!!");
            Console.ReadKey();
        }

        static void Play()   // Function for Main game loop
        {
            while (!gameScreen.AllTurned())
            {
                gameScreen.DrawScreen();
                int x = -1, y = -1;
                // Getting coordinates of first card from user:
                do
                {
                    Console.WriteLine("Write coordinates of the first card, each of them to a new line.");
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Column: ");
                        x = int.Parse(Console.ReadLine());
                        Console.Write("   Row: ");
                        y = int.Parse(Console.ReadLine());
                        Console.ResetColor();
                    }
                    catch (FormatException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Write only numbers.");
                        Console.ResetColor();
                    }
                    catch (OverflowException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Some mistake occures.");
                        Console.ResetColor();
                    }

                    if (!gameScreen.ValidCard(x, y))
                    {
                        Console.WriteLine("Mistakenly write coordinates or it is not hidden card.");
                    }
                } while (!gameScreen.ValidCard(x, y));
                gameScreen.TurnFirstCard(x, y);
                x = -1;
                y = -1;

                // Getting coordinates of second card from user:
                do
                {
                    Console.WriteLine("Write coordinates of the second card, each of them to a new line.");
                    try
                    {
                        Console.Write("Column: ");
                        x = int.Parse(Console.ReadLine());
                        Console.Write("   Row: ");
                        y = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Write only numbers.");
                        Console.ResetColor();
                    }
                    catch (OverflowException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Some mistake occures.");
                        Console.ResetColor();
                    }

                    if (!gameScreen.ValidCard(x, y))
                    {
                        Console.WriteLine("Mistakenly write coordinates or it is NOT hidden card.");
                    }
                } while (!gameScreen.ValidCard(x, y));
                gameScreen.TurnSecondCard(x, y);
            }
        }
    }
}
