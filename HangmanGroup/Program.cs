using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGroup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] listOfWords = { "apple", "cat", "banana", "elephant"};
            Random random = new Random();
            string secretWord = listOfWords[random.Next(listOfWords.Length)];
            //currentWord as secretWord but all _
            string currentWord = new String('_', secretWord.Length);
            string input = "";
            int guesses = 0;
            while (guesses < 6 && !secretWord.Equals(currentWord))
            {
                Console.WriteLine($"Word to guess is {secretWord}");
                Console.WriteLine($"Word to guess is {currentWord}");
                Console.WriteLine("Enter a letter to guess!");
                input = Console.ReadLine();
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
                    guesses++;
                }
                drawHangman(guesses);
            }
            if (secretWord.Equals(currentWord))
            {
                Console.WriteLine($"Well done! You guessed {secretWord}!");
            }
            else
            {
                Console.WriteLine($"Unlucky, the word was {secretWord}.");
            }
            Console.ReadLine();
        }

        static bool checkGuess(string a, string b)
        {
            if (a.Contains(b))
            {
                return true;
            }
            return false    ;
        }

        static void drawHangman(int guesses)
        {
            Console.WriteLine($"Lives remaining: {6-guesses}");
            string[] Hangman = { "", 
                                 "   \n   \n   \n   \n___|___\n",
                                 "",
                                 "",
                                 "",
                                 "",
                                 ""};
            //Console.WriteLine(Hangman[guesses]);
            //draw Hangman stick figure
        }

    }
}
