using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HangmanGroup
{
    internal class Program
    {
        private static string[] listOfWords = { "apple", "cat", "banana", "elephant", "football"
        , "tree", "dancer", "gymnast", "temperature", "wedding", "chimney", "zebra", "glass"};
        private static string secretWord = "";
        private static string currentWord;
        private static string input = "";
        private static int lives = 0;
        private static List<string> guesses = new List<string>();

        static void Main(string[] args)
        {
            //gets the word to guess
            getWord();
            //main game loop - keeps running until word guessed or 9 lives used
            while (lives < 9 && !secretWord.Equals(currentWord))
            {
                //several methods that need to run in loop, getting users guess, checking guess
                //printing status of game etc
                Console.WriteLine();
                Console.WriteLine($"Word to guess is {string.Join("", currentWord)}");
                getGuess();
                //print list of guesses
                showGuesses();
                Console.WriteLine();
                //checks guess from user, if user guessed a letter correctly, replace _ with letter
                if (checkGuess(secretWord, input))
                {
                    string tempString = "";
                    //replace _ with input at correct index 
                    for (int i=0; i <secretWord.Length; i++)
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
                else
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

        static bool checkGuess(string a, string b)
        {
            //method to check if users guess is correct, returns bool
            if (a.Equals(b))
            {
                //ends game as word has been guessed
                currentWord = secretWord;
            }
            else if (a.Contains(b) && b.Length == 1) 
            { 
                return true; 
            }
            return false;
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
            if (lives > 0)
            {
                Console.WriteLine(Hangman[guesses]);
            }
        }

        static void getWord()
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
                    //currentWord as secretWord but all _
                    currentWord = new String('_', secretWord.Length);
                }
                else if (option.Equals("2"))
                {
                    //reads all words from dictionary.txt, generates a random word
                    string[] lines = System.IO.File.ReadAllLines("dictionary.txt");
                    secretWord = lines[random.Next(lines.Length)].ToLower();
                    currentWord = new String('_', secretWord.Length);
                }
                else
                {
                    Console.WriteLine("Invalid input, must enter 1 or 2");
                }
            }
        }

        static void getGuess()
        {
            input = "";
            while (input.Equals(""))
            {
                Console.WriteLine("Enter a word or letter to guess:");
                //if letter word is valid a/z and not already guessed, then is valid guess
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
                }
                else 
                {
                    Console.WriteLine("Invalid guess, must guess a letter or word of correct length");
                    input = "";
                }
            }
        }
        
        static void restart()
        {
            Console.WriteLine("Enter y to play again, anything else to quit");
            if (Console.ReadLine().ToLower().Equals("y"))
            {
                //reset all vars, run main again
                input = "";
                lives = 0;
                guesses.Clear();
                secretWord = "";
                Main(new string[] { });
            }
            else
            {
                Environment.Exit(0);
            }
        }

        static void showGuesses()
        {
            Console.WriteLine("Guesses: ");
            foreach (var g in guesses)
            {
                Console.Write(g + " ");
            }
        }
    }
}
