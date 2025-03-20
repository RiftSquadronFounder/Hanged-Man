using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hanged_Man
{
    internal class Game
    {

        int faultPlace = -1;
        int pointerPlace = 0;
        int heading = 0;


        private List<int> randomlyOpenedLetter_ = new List<int>();
        private string userName_;
        private string word_;
        private string lastMessage = "";
        private List<string> words_;
        private int faultsAssigned_;
        private int LettersFound_;
        private int maxFaults_;
        private int lettersRevealed_;
        private List<int> lettersFound_ = new List<int>();
        private bool isRunning_ = true;




        public Game(string userName, int maxFaults, int lettersRevealed, List<string> words)
        {
            this.userName_ = userName;
            this.maxFaults_ = maxFaults;
            this.lettersRevealed_ = lettersRevealed;
            words_ = words;
        }



        public Record Play() { 
            int choice = 0;
            bool task = true;

            while (task)
            {
                Console.Clear();
                Console.Write("Выберите опцию:" +
                    "\n\t1) Играть на рандом (Слово берется из словаря)" +
                    "\n\t2) Играть с игроком (Другой человек задает слово)" +
                    "\n\n\tQ) Назад\n\n");
                
                var Key = Console.ReadKey(true);
                try
                {
                    choice = Int32.Parse(Key.KeyChar.ToString());
                    if (choice == 1)
                    {
                        Random rnd = new Random();
                        word_ = words_[rnd.Next(0, words_.Count)];
                        int randomLetter = rnd.Next(0, word_.Length);
                        for(int i = 0; i < lettersRevealed_; i++)
                        {
                            while (randomlyOpenedLetter_.Contains(randomLetter))
                            {
                                randomLetter = rnd.Next(0, word_.Length);
                            }

                            randomlyOpenedLetter_.Add(randomLetter);
                            
                        }
                        try
                        {
                            return Process_();
                        }catch(Exception e) { Console.WriteLine(e); Console.ReadLine(); }
                    }
                    else if (choice == 2)
                    {
                        {
                            string word = "";
                            bool task1 = true;

                            while (task1)
                            {
                                Console.Clear();
                                int choice1 = 0;
                                Console.Write("Введите слово:\n\t");
                                word = Console.ReadLine();

                                Console.Clear();
                                Console.Write($"Вы ввели слово:\n\t{word}\nВерно?\n\t1) Да, продолжить\n\t2) Ввести заново\n\n\tQ) Отменить действие");
                                var Key1 = Console.ReadKey(true);
                                try
                                {
                                    choice1 = Int32.Parse(Key1.KeyChar.ToString());
                                    if (choice1 == 1)
                                    {
                                        task1 = false;
                                        word_ = word;

                                        OpenLetters();
                                        try
                                        {
                                            return Process_();
                                        }
                                        catch {}

                                    }

                                }
                                catch {
                                    if (Key1.KeyChar.ToString().ToLower() == "q" || Key1.KeyChar.ToString().ToLower() == "й")
                                    { task1 = false; task = false;
                                        try
                                        {
                                            return null;
                                        }
                                        catch  { }
                                    }
                                }
                                
                            }

                        }

                    }

                 
                }
                catch { return new Record(); }
                if (Key.KeyChar.ToString().ToLower() == "q" || Key.KeyChar.ToString().ToLower() == "й")
                { task = false; }
            }
            try
            {
                return Process_();
            }
            catch (Exception ex) { Console.WriteLine(ex); Console.ReadLine(); return Process_(); }
        }



        private void OpenLetters() {
            bool task = true;
            while (task)
            {
                Console.Clear();
                Console.WriteLine($"[ << {word_} >> ]");

                

                if(pointerPlace + heading > -1 && pointerPlace + heading < word_.Length)
                {
                    pointerPlace += heading;
                    heading = 0;
                }
                Console.Write($"[ >> ");
                for (int i = 0; i < word_.Length; i ++)
                {
                    if (randomlyOpenedLetter_.Contains(i))
                    {
                        Console.Write(word_[i]);
                    }
                    else
                    {
                        Console.Write("_");
                    }
                    
                }
                Console.Write($" << ]\n");
                Console.Write($"     ");
                for (int i = 0; i < word_.Length; i++)
                {
                    
                    if (i == pointerPlace)
                    {
                        Console.Write("^");
                    }
                    else { Console.Write("-"); }
                }
                Console.WriteLine("\n\n(Нажмите на пробел, чтобы раскрыть букву)\n(Нажмите Enter для подтверждения)");
                var Key = Console.ReadKey(true);
                if (Key.Key == ConsoleKey.LeftArrow || Key.Key == ConsoleKey.RightArrow)
                {
                    if (Key.Key == ConsoleKey.LeftArrow) { heading = -1; }
                    else if (Key.Key == ConsoleKey.RightArrow) { heading = 1; }
                }
                else if (Key.Key == ConsoleKey.Spacebar)
                {
                    if (randomlyOpenedLetter_.Contains(pointerPlace))
                    {
                        randomlyOpenedLetter_.Remove(pointerPlace);
                    }
                    else { randomlyOpenedLetter_.Add(pointerPlace); }
                }
                else if(Key.Key == ConsoleKey.Enter) { task = false; }

            }
        }



        private Record Process_() {
            Console.Clear();
            Console.WriteLine("Игра полностью подготовлена, нажмите Enter, чтобы начать");
            Console.ReadLine();
            Console.Clear();
            while (isRunning_)
            {
                string isWord = "";
                if (pointerPlace + heading > -1 && pointerPlace + heading < word_.Length)
                {
                    pointerPlace += heading;
                    heading = 0;
                }
                GameView();
                var Key = Console.ReadKey(true);
                if (Key.Key == ConsoleKey.LeftArrow || Key.Key == ConsoleKey.RightArrow)
                {
                    if (Key.Key == ConsoleKey.LeftArrow) { heading = -1; }
                    else if (Key.Key == ConsoleKey.RightArrow) { heading = 1; }
                }
                else {
                    Console.WriteLine("\n\n");
                    if (word_[pointerPlace].ToString().ToLower() == Key.KeyChar.ToString().ToLower() && !randomlyOpenedLetter_.Contains(pointerPlace)&& !lettersFound_.Contains(pointerPlace))
                    {
                        lastMessage = ("Буква найдена!, поздравляю!");
                        LettersFound_ += 1;
                        lettersFound_.Add(pointerPlace);
                    }
                    else if (randomlyOpenedLetter_.Contains(pointerPlace) || lettersFound_.Contains(pointerPlace))
                    {
                        lastMessage = ("Эта буква уже отктрыта.");
                    }
                    else
                    {
                        lastMessage = "Увы, вы промахнулись...";
                        faultPlace = pointerPlace;
                        faultsAssigned_++;
                    }
                }
                for (int i = 0; i < word_.Length; i++)
                {
                    if (lettersFound_.Contains(i) || randomlyOpenedLetter_.Contains(i))
                    {
                        isWord = $"{isWord}{word_[i]}";
                    }
                }

                if (word_ == isWord)
                {
                    isRunning_ = false;
                    Win();
                }
                if (maxFaults_ - faultsAssigned_ == 0)
                {
                    isRunning_ = false;
                    Fail();
                }
            }
            if (userName_ == "")
            {
                AssignName();
            }
            return new Record(userName_, word_, faultsAssigned_, LettersFound_, lettersFound_ , randomlyOpenedLetter_) ;
        }

        private void GameView() {
            
            Console.Clear();
            Console.Write($"[ << ");
            for (int i = 0; i < word_.Length; i++)
            {
                if (randomlyOpenedLetter_.Contains(i) || lettersFound_.Contains(i))
                {
                    Console.Write(word_[i]);
                }
                else if(i == faultPlace)
                {
                    Console.Write("#");
                }
                else { Console.Write("_"); }
            }
            

            Console.Write($" >> ]\n");
            Console.Write($"     ");
            for (int i = 0; i < word_.Length; i++)
            {

                if (i == pointerPlace)
                {
                    Console.Write("^");
                }
                else { Console.Write("-"); }
            }
            Console.WriteLine($"\n\n{lastMessage}\n");
            Console.Write($"Оставшееся кол-во попыток: {maxFaults_ - faultsAssigned_}");

        }


        private void Fail() {
            Console.Clear();
            Console.Write("Вашей шее не позавидуешь...\n\n(Нажмите Enter)\n\n");

            new Record(userName_, word_, faultsAssigned_, LettersFound_, lettersFound_, randomlyOpenedLetter_).InfoOut();
            Console.ReadLine();
            
            
                AssignName();
            
        }

        private void Win() {
            Console.Clear();
            Console.Write("Поздравляю! Вы полностью отгадали слово!\n\n(Нажмите Enter)\n\n");

            new Record(userName_, word_, faultsAssigned_, LettersFound_, lettersFound_, randomlyOpenedLetter_).InfoOut();
            Console.ReadLine();
                AssignName();
            
        }

        private void AssignName() {

            Console.Clear();
            Console.Write
                ("Похоже на то, что вы не указали имя перед началом игры\n" +
                "Не хотели бы вы указать ваше имя для таблицы рекордов?\n\n(Введите имя, если таковое имеется) >>> ");
            userName_ = Console.ReadLine();
            try
            {
                userName_ = userName_.Trim();
            }
            catch { }

            if(userName_.Trim() == "")
            {
                userName_ = "[Не указано]";
            }
        }


    }
}
