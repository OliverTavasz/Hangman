namespace Hangman
{
    public class HangmanGame
    {
        private static readonly string[] HangmanGraphic = ["       \n       \n       \n       \n       \n       \n       \n",
                                                           "       \n|      \n|      \n|      \n|      \n|      \n|      \n",
                                                           "       \n|/     \n|      \n|      \n|      \n|      \n|      \n",
                                                           "______ \n|/     \n|      \n|      \n|      \n|      \n|      \n",
                                                           "______ \n|/   | \n|      \n|      \n|      \n|      \n|      \n",
                                                           "______ \n|/   | \n|    O \n|      \n|      \n|      \n|      \n",
                                                           "______ \n|/   | \n|    O \n|    | \n|    | \n|      \n|      \n",
                                                           "______ \n|/   | \n|    O \n|   /| \n|    | \n|      \n|      \n",
                                                           "______ \n|/   | \n|    O \n|   /|\\\n|    | \n|      \n|      \n",
                                                           "______ \n|/   | \n|    O \n|   /|\\\n|    | \n|   /  \n|      \n",
                                                           "______ \n|/   | \n|    O \n|   /|\\\n|    | \n|   / \\\n|      \n",
        ];
        private bool Win;
        private int Progress;
        private List<string> Guesses;
        private string Word;
        private char[] Wordprogress;
        public HangmanGame() 
        {
            Reload();
        }
        private void Reload()
        {
            string[] words = File.ReadAllLines("Words.txt");
            Word = words[new Random().Next(0, words.Length)].ToLower();
            Wordprogress = [.. Enumerable.Repeat('_', Word.Length)];
            Progress = 0;
            Win = false;
            Guesses = [];
        }
        public void Run()
        {
            Console.WriteLine("Welcome to hangman!\nPress enter to begin\n");
            Console.ReadLine();
            while (true)
            {
                while (!Win)
                {
                    Draw();
                    string input = Console.ReadLine() ?? "";
                    if (!HandleInput(input.ToLower()))
                        Progress++;
                    if (Progress >= HangmanGraphic.Length - 1)
                        break;
                }
                Draw();
                Console.WriteLine("\n\nYou " + (Win ? "won" : "lost") + "! The word was '" + Word + "'");
                Console.WriteLine("Enter 'q' to quit or nothing to play again");
                if (Console.ReadLine() == "q")
                    break;
                Reload();
            }
        }

        private void Draw()
        {
            Console.Clear();
            string wp = "";
            for (int i = 0; i < Wordprogress.Length; i++)
            {
                wp += Wordprogress[i] + " ";
            }
            Console.WriteLine("Guess the word:\n" + wp + "\n");
            Console.WriteLine(HangmanGraphic[Progress]);
            string guesses = "";
            if (Guesses.Count != 0)
            {
                Console.WriteLine("\nGuesses:");
                for (int i = 0; i < Guesses.Count; i++)
                {
                    guesses += Guesses[i] + ", ";
                }
            }
            Console.Write(guesses + "\n\n> ");
        }
        /// <summary>
        /// </summary>
        /// <param name="input">User input</param>
        /// <returns>True if user's guess was correct and has not already been guessed.</returns>
        private bool HandleInput(string input)
        {
            if (input.Length == 1)
            {
                if (Word.Contains(input))
                {
                    for (int i = 0; i < Word.Length; i++)
                    {
                        if (Word[i] == input[0])
                        {
                            Wordprogress[i] = input[0];
                        }
                    }
                    if (new string(Wordprogress) == Word)
                        Win = true;
                    return true;
                }
                else if (!Guesses.Contains(input) && input != "")
                {
                    Guesses.Add(input);
                    return false;
                }
                else
                    return true;
            }
            else if (input == Word)
            {
                Win = true;
                Wordprogress = [.. Word];
                return true;
            }
            return false;
        }
    }
}
