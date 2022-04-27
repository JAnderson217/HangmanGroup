using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HangmanGroup
{
    internal class Program
    {
        private static string[] listOfWords = { "apple", "cat", "banana", "elephant", "football"
        , "tree", "dancer", "gymnast", "temperature", "wedding", "chimney", "zebra", "glass"};
        
        static void Main(string[] args)
        {
            //Gets word to guess, and create new var with _ replacing each letter
            string secretWord = getWord("");
            string currentWord = new String('-', secretWord.Length);
            //vars to store guess, list of guesses and lives used
            List<string> guesses = new List<string>();
            int lives = 0;
            string input = "";
            //main game loop - keeps running until word guessed or 9 lives used
            while (lives < 9 && !secretWord.Equals(currentWord))
            {
                //Main loop keeps running to play game, continually asks user for their guess
                //checks guess, updates game status, prints relevant game info, hangman, lives, etc
                Console.WriteLine();
                Console.Write($"Word to guess is: ");
                foreach(var letter in currentWord){ Console.Write(letter + " "); }
                Console.WriteLine();
                input = getGuess(input, secretWord, guesses);
                showGuesses(guesses);
                Console.WriteLine();
                string tempWord = currentWord;
                currentWord = checkGuess(secretWord, currentWord, input);
                //if word is unchanged, means guess was incorrect so life is used
                if (tempWord == currentWord)
                {
                    lives++;
                }
                drawHangman(lives);
            }
            if (secretWord.Equals(currentWord))
            {
                Console.WriteLine($"Well done! You guessed {secretWord}!");
            }
            else
            {
                Console.WriteLine($"Unlucky, the word was {secretWord}.");
            }
            restart();
        }

        static string checkGuess(string secretWord, string currentWord, string input)
        {
            //Updates word if letter guess was correct, ends game if word guess was correct
            if (secretWord.Equals(input))
            {
                //guessed entire word correctly, end game and main loop
                return secretWord;
            }
            else if (secretWord.Contains(input) && input.Length == 1)
            {
                string tempString = "";
                //replace - with input at correct index 
                for (int i = 0; i < secretWord.Length; i++)
                {
                    if (secretWord[i].Equals(input[0]))
                    {
                        tempString += input[0];
                    }
                    else
                    {
                        tempString += currentWord[i];
                    }
                }
                currentWord = tempString;
            }
            return currentWord;
        }

        static void drawHangman(int guesses)
        {
            //draws Hangman figure, has each stage stored in array corresponding to lives used
            Console.WriteLine($"Lives remaining: {9-guesses}");
            string[] Hangman = new string[] { "",
                "\n" + "\n | " + "\n | " + "\n | " + "\n | " + "\n | " + "\n |_______________________\n",
                "\n___________________" + "\n|" + "\n|" + "\n|" + "\n|" + "\n|" + "\n|_______________________\n",
                "\n___________________" + "\n|                  |" + "\n|" + "\n|" + "\n|" + "\n|" + "\n|_______________________\n",
                "\n___________________" + "\n|                  |" + "\n|                  O" + "\n|" + "\n|" + "\n|" + "\n|_______________________\n",
                "\n___________________" + "\n|                  |" + "\n|                  O" + "\n|                  |" + "\n|" + "\n|" + "\n|_______________________\n",
                "\n___________________" + "\n|                  |" + "\n|                  O" + "\n|               ---|" + "\n|" + "\n|" + "\n|_______________________\n",
                "\n___________________" + "\n|                  |" + "\n|                  O" + "\n|               ---|---" + "\n|" + "\n|" + "\n|_______________________\n",
                "\n___________________" + "\n|                  |" + "\n|                  O" + "\n|               ---|---" + "\n|                  /" + "\n|                 /" + "\n|_______________________\n",
                "\n___________________" + "\n|                  |" + "\n|                  O" + "\n|               ---|---" + "\n|                  /\\" + "\n|                 /  \\" + "\n|_______________________"};
            //cleaner to not print blank line if 0 lives lost
            if (guesses > 0)
            {
                Console.WriteLine(Hangman[guesses]);
            }
        }

        static string getWord(string secretWord)
        {
            //gets word to guess, either randomly from listOfWords or array or from a dictionary.txt
            while (secretWord.Equals(""))
            {
                string option = "";
                Console.WriteLine("Enter 1 for random word from array, 2 for word from dictionary");
                option = Console.ReadLine();
                Random random = new Random();
                if (option.Equals("1"))
                {
                    secretWord = listOfWords[random.Next(listOfWords.Length)].ToLower();
                }
                else if (option.Equals("2"))
                {
                    //reads all words from dictionary.txt to array, gets a random word
                    string[] lines = System.IO.File.ReadAllLines("dictionary.txt");
                    secretWord = lines[random.Next(lines.Length)].ToLower();
                }
                else
                {
                    Console.WriteLine("Invalid input, must enter 1 or 2");
                }
            }
            return secretWord;
        }

        static string getGuess(string input, string secretWord, List<string> guesses)
        {
            input = "";
            while (input.Equals(""))
            {
                Console.WriteLine("Enter a word or letter to guess:");
                input = Console.ReadLine().ToLower();
                //valid guess has to be a-z, not used before, and one letter or word of correct length
                if (Regex.IsMatch(input, "[a-z]", RegexOptions.IgnoreCase) && !guesses.Contains(input)
                    && ((input.Length == 1) || (input.Length == secretWord.Length)))
                {
                    guesses.Add(input);
                }
                else if (guesses.Contains(input))
                {
                    Console.WriteLine("Already guessed!");
                    input = "";
                }
                else 
                {
                    Console.WriteLine("Invalid guess, must guess a letter or word of correct length");
                    input = "";
                }
            }
            return input;
        }
        
        static void restart()
        {
            Console.WriteLine("Enter y to play again, anything else to quit");
            if (Console.ReadLine().ToLower().Equals("y"))
            {
                Main(new string[] { });
            }
            else
            {
                Environment.Exit(0);
            }
        }

        static void showGuesses(List<string> guesses)
        {
            Console.WriteLine("Guesses: ");
            foreach (string g in guesses)
            {
                Console.Write(g + " ");
            }
        }
    }
}
