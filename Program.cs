using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace WordGame
{
    
    class Program
    {
        static void Main(string[] args)
        {
            int length, chances, counter = 0;
            string chosenWord = "";
            Random r = new Random();
            char choice = 'y', ch, level;

            do
             {
                counter = 0;
                length = r.Next(4, 12);
   
                string[] list = File.ReadAllLines("dictionary.txt");
                WordFamily wordFamily = new WordFamily();

                foreach (string word in list)
                {
                    int len = word.Length;
                    if (length == len)
                        wordFamily.Add(word);
                }
                do
                {
                    Console.WriteLine("Welcome to Guess The Word Game....Good Luck :)");
                    Console.WriteLine();

                    Console.WriteLine("Choose level : Easy or Hard (e/h) : ");
                    bool val = Char.TryParse(Console.ReadLine(), out level);
                    try
                    {
                        if (!val)
                            throw new InvalidInputException();
                        else
                        {
                            level = Character.toLower(level);
                            break;
                        }
                    }
                    catch (InvalidInputException)
                    {
                        ///Console.WriteLine(e.Message);
                    }
                } while (true);

                    //=====================================Difficult Level========================================================
                    if (level == 'h')
                {
                    WordFamily conchars = new WordFamily();
                    //scan the list if entered character presents in the word consecutively, add it to new family
                    foreach (string word in wordFamily.ToArray())
                    {
                        for (int x = 0; x < length - 1; x++)
                        {
                            if(Character.Compare(word[x], 'j') || Character.Compare(word[x], 'q') || 
                            Character.Compare(word[x], 'x') || Character.Compare(word[x], 'z') )
                            {
                                conchars.Add(word);
                                break;
                            }
                        }
                    }
                    //make it a main family , if it has words
                    if (conchars.Count() > 0)
                    {
                        wordFamily.RemoveRange(0, wordFamily.Count());
                        wordFamily.AddRange(conchars);
                    }
                }

                chances = length * 2;
                IDictionary<int, char> correctGuessedCharsWithPos = new Dictionary<int, char>();

                ArrayList correctGuessedChars = new ArrayList();
                ArrayList incorrectGuessedChars = new ArrayList();

                WordFamily A = new WordFamily();
                WordFamily A0 = new WordFamily();
                WordFamily A1 = new WordFamily();
                WordFamily A2 = new WordFamily();
                WordFamily A3 = new WordFamily();
                WordFamily A4 = new WordFamily();
                WordFamily A5 = new WordFamily();
                WordFamily A6 = new WordFamily();

                while (counter < chances)
                {
                    Console.WriteLine("You have " + (chances - counter) + " guesses left.");

                    Console.Write("Used letters: ");
                    List<char> usedLetters = new List<char> { };
                    foreach (char c in correctGuessedChars)
                        usedLetters.Add(c);
                    foreach (char c in incorrectGuessedChars)
                        usedLetters.Add(c);

                    usedLetters.Sort();
                    foreach (char c in usedLetters)
                        Console.Write(c + " ");
                    Console.WriteLine();

                    //Console.WriteLine("Number of words in word family is : " + wordFamily.Count);
                    if (wordFamily.Count() > 1)
                    {
                            int no = r.Next(0, wordFamily.Count() - 1);
                            chosenWord = (string)wordFamily.getWord(no);
                    }
                    //Console.WriteLine(chosenWord);

                    //display the chosen word to user in hidden form
                    Console.Write("Word: ");
                    int i = 0;
                    foreach (char c in chosenWord)
                    {
                            if (correctGuessedChars.Contains(c))
                            {
                                Console.Write(c);           //expose the character to user
                                if (!correctGuessedCharsWithPos.ContainsKey(i))
                                    correctGuessedCharsWithPos.Add(i, c);
                            }
                            else
                                Console.Write("-");          // hide the character
                            i++;
                    }
                    Console.WriteLine();
                    
                    do
                    {
                        Console.Write("Enter guess: ");
                        bool val = Char.TryParse(Console.ReadLine(), out ch);
                        try
                        {
                            if (!val)
                                throw new InvalidInputException();
                            else
                                break;
                        }
                        catch(InvalidInputException)
                        {
                            ///Console.WriteLine(e.Message);
                        }
                    } while (true);
                        //filter the words depending on the number of times the character is present in the word to different arraylist
                        //the successiding number in the name of the array list is the number of times the character is present in it.
                        //Arraylist A is to store the list with maximum number of words

                    //empty all sub word families

                    A.RemoveRange(0, A.Count());
                    A0.RemoveRange(0, A0.Count());
                    A1.RemoveRange(0, A1.Count());
                    A2.RemoveRange(0, A2.Count());
                    A3.RemoveRange(0, A3.Count());
                    A4.RemoveRange(0, A4.Count());
                    A5.RemoveRange(0, A5.Count());
                    A6.RemoveRange(0, A6.Count());

                    foreach (string word in wordFamily.ToArray())
                    {
                        int number = 0;
                            foreach (var c in correctGuessedCharsWithPos)
                            {
                                int y = c.Key;
                                char x = c.Value;
                                for (int a = 0; a < word.Length; a++)
                                {
                                   if(Character.Compare(word[a], x) && a == y)
                                    {
                                        number++;
                                    }
                                }
                            }
                            if (number == correctGuessedCharsWithPos.Count)
                                A.Add(word);
                    }
                    if (A.Count() != 0)
                    {
                            wordFamily.RemoveRange(0, wordFamily.Count());
                            wordFamily.AddRange(A);
                            A.RemoveRange(0, A.Count());
                    }
                    if (wordFamily.Count() > 1)
                    {
                            foreach (string word in wordFamily.ToArray())
                            {
                                int cnt = 0;
                                foreach (char c in word)
                                {
                                    if (c == ch)
                                        cnt++;
                                }
                                switch (cnt)
                                {
                                    case 0: A0.Add(word); break;
                                    case 1: A1.Add(word); break;
                                    case 2: A2.Add(word); break;
                                    case 3: A3.Add(word); break;
                                    case 4: A4.Add(word); break;
                                    case 5: A5.Add(word); break;
                                    case 6: A6.Add(word); break;
                                }
                            }//end of foreach loop

                            //find out the list with maximum number of words and assign it to list A. 
                            int max = 0;
                            int listNo = 0;
                            if (max < A6.Count())
                            {
                                max = A6.Count();
                                listNo = 6;
                            }
                            if (max < A5.Count())
                            {
                                max = A5.Count();
                                listNo = 5;
                            }
                            if (max < A4.Count())
                            {
                                max = A4.Count();
                                listNo = 4;
                            }
                            if (max < A3.Count())
                            {
                                max = A3.Count();
                                listNo = 3;   
                            }
                            if (max < A2.Count())
                            {
                                max = A2.Count();
                                listNo = 2;
                            }
                            if (max < A1.Count())
                            {
                                max = A1.Count();
                                listNo = 1;
                            }
                            if (max < A0.Count())
                            {
                                max = A0.Count();
                                listNo = 0;
                            }
                            switch(listNo)
                            {
                                case 0: A = A0;
                                        incorrectGuessedChars.Add(ch);
                                        Console.WriteLine("Sorry, there are no " + ch + "'s.");
                                        break;
                                case 1: A = A1;
                                        correctGuessedChars.Add(ch);
                                        Console.WriteLine("Yes, there is 1 copy of " + ch + ".");
                                        break;
                                case 2: A = A2;
                                        correctGuessedChars.Add(ch);
                                        Console.WriteLine("Yes, there are 2 copies of " + ch + ".");
                                        break;
                                case 3: A = A3;
                                        correctGuessedChars.Add(ch);
                                        Console.WriteLine("Yes, there are 3 copies of " + ch + ".");
                                        break;
                                case 4: A = A4;
                                        correctGuessedChars.Add(ch);
                                        Console.WriteLine("Yes, there are 4 copies of " + ch + ".");
                                        break;
                                case 5: A = A5;
                                        correctGuessedChars.Add(ch);
                                        Console.WriteLine("Yes, there are 5 copies of " + ch + ".");
                                        break;
                                case 6: A = A6;
                                        correctGuessedChars.Add(ch);
                                        Console.WriteLine("Yes, there are 6 copies of " + ch + ".");
                                        break;
                            }
                    }
                    else
                    {
                            int temp = 0;
                            foreach (char c in chosenWord)
                            {
                                if (c == ch)
                                    temp++;
                            }
                            if(temp != 0)
                            {
                                Console.WriteLine("Yes, there is/are " + temp + " copies of " + ch + ".");
                                correctGuessedChars.Add(ch);
                            }
                            else
                            {
                                Console.WriteLine("There are no copies of " + ch + "'s.");
                                incorrectGuessedChars.Add(ch);
                            }
                    }
                        
                    Console.WriteLine();
                    wordFamily.RemoveRange(0, wordFamily.Count());
                    wordFamily.AddRange(A);

                    //check if user has guessed the word!
                    if (correctGuessedCharsWithPos.Count == length)
                    {
                        Console.WriteLine("Congratulations!!!! You guessed the word correct!!!");
                        goto replay;
                    }

                    counter++;
                }//end of while
                if (counter == chances)
                {
                    if(wordFamily.Count() > 1)
                    {
                        int no = r.Next(0, wordFamily.Count() - 1);
                        chosenWord = (string)wordFamily.getWord(no);
                    }
                    Console.WriteLine("You lose!!! The word was " + chosenWord);
                }
                replay:
                Console.WriteLine();
                Console.WriteLine("Play again !!! (y/n) : ");
                choice = Char.Parse(Console.ReadLine());
            }while(choice != 'n');
        }
    }
}
