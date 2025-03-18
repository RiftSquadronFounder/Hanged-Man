using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace Hanged_Man
{
    internal class Menu
    {


        private int faults = 3, revealed = 2;
        private string UserName_;
        private string recordsFileName_ = "records.txt";
        private string wordsFileName_ = "words.txt";
        private List<string> wordsAvialable_ = new List<string>();
        public List<Record> Records;




        public string getName() { return UserName_; }
        public List<string> getWords() { return wordsAvialable_; }

        public List<Record> GetRecords() { return Records; }



        public void ReadRecords()
        {
            if (File.Exists(recordsFileName_))
            {
                string FileContainings = File.ReadAllText(recordsFileName_), word = "", dictionary = "";
                int counter = 0;
                bool dictMode = false;
                Dictionary<int, char> letters = new Dictionary<int, char>();
                Record rec = new Record();

                for (int i = 0; i < FileContainings.Length; i++)
                {
                    if (!dictMode)
                    {
                        if (FileContainings != "|")
                        {
                            word = word + FileContainings[i];
                        }
                        else
                        {
                            if (counter == 0)
                            {
                                rec.setName(word);
                            }
                            else if (counter == 1)
                            {
                                rec.setWord(word);
                            }
                            else if (counter == 2)
                            {
                                rec.setFaults(Int32.Parse(word));
                            }
                            else if (counter == 3)
                            {
                                rec.setRightChoices(Int32.Parse(word));
                            }
                            else if (counter == 4)
                            {
                                counter = -1;
                                dictMode = true;
                            }
                            counter++;
                            word = "";
                        }
                    }
                    else
                    {
                        if (FileContainings != "|")
                        {
                            dictionary = dictionary + FileContainings[i];
                        }
                        else
                        {
                            dictMode = false;
                            for (int k = 0; k < dictionary.Length; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    try
                                    {
                                        letters.Add(Int32.Parse(dictionary), dictionary[k + 1]);
                                    }
                                    catch { Console.WriteLine("Error: (Не удалось извлечь символ)"); }
                                }
                            }

                            Records.Add(rec);
                            rec = new Record();
                        }
                    }

                }
            }
        }

        public void RewriteRecords()
        {
            string saveLine = "";


            for (int i = 0; i < Records.Count; i++)
            {
                Record r = Records[i];
                saveLine = $"{saveLine}{r.GetName()}|{r.GetWord()}|{r.GetWrongGuesses()}|{r.GetRightGuesses()}|" +
                    $"{string.Join("", r.GetLetters().Select(kv => $"{kv.Key}{kv.Value}"))}";
            }


            File.WriteAllText(recordsFileName_, saveLine);
        }

        public void ReadAvialableWords()
        {
            if (File.Exists(wordsFileName_))
            {
                string FileContainings = File.ReadAllText(wordsFileName_), word = "";

                for (int i = 0; i < FileContainings.Length; i++)
                {
                    if (FileContainings[i] != '|')
                    {
                        word = word + FileContainings[i];
                    }
                    else
                    {
                        wordsAvialable_.Add(word);

                        word = "";
                    }
                }


            }
        }

        public void RewriteAvialableWords()
        {

            string line = "";

            for (int i = 0; i < wordsAvialable_.Count; i++) { line = $"{line}{wordsAvialable_[i]}|"; }

            File.WriteAllText(wordsFileName_, line);

        }
        public void addANewWord()
        {
            string word = "";
            bool task = true;
            
            while (task) {
                Console.Clear();
                int choice = 0;
                //for (int i = 0; i < wordsAvialable_.Count; i++) { Console.Write($" {wordsAvialable_[i]} |"); }
                Console.Write("Введите слово:\n\t");
                word = Console.ReadLine();

                Console.Clear();
                Console.Write($"Вы ввели слово:\n\t{word}\nВерно?\n\t1) Да, добавить слово\n\t2) Ввести заново\n\t3) Отменить действие");
                var Key = Console.ReadKey(true);
                try
                {
                    choice = Int32.Parse(Key.KeyChar.ToString());
                    if (choice == 1)
                    {
                        wordsAvialable_.Add(word);
                        RewriteAvialableWords();
                    }
                    else if (choice == 2)
                    {

                    }
                    else if (choice == 3)
                    {
                        task = false;
                    }
                }
                catch { }
                if (Key.KeyChar.ToString().ToLower() == "q" || Key.KeyChar.ToString().ToLower() == "й")
                { Console.Clear(); Environment.Exit(0); }

            }
        }

        public void ChangeFaults() {
            int changeFaults = faults;
            bool task = true;

            while (task)
            {
                Console.Clear();
                int choice = 0;
                if (faults == changeFaults)
                {
                    Console.Write($"Текущее количество ошибок до провала\nустановлено на значении: {changeFaults}\n\t");
                }
                else {
                    Console.Write($"Текущее количество ошибок до провала\nустановлено на значении: {changeFaults} (Ранее: {faults})\n\t");
                }
                Console.Write($"Вы бы хотели:\n\t1) Сохранить текущее значение\n\t2) Изменить значение\n\n\tQ) Отменить действие");
                var Key = Console.ReadKey(true);
                try
                {
                    choice = Int32.Parse(Key.KeyChar.ToString());
                    if (choice == 1)
                    {
                        faults = changeFaults;
                    }
                    else if (choice == 2)
                    {
                        try
                        {
                            Console.Clear();
                            Console.Write("Введите значение: ");
                            if (changeFaults > 0)
                            {
                                changeFaults = Int32.Parse(Console.ReadLine());
                            }
                        }
                        catch { Console.Write("Введеное вами значение не подходит под данный параметр"); Console.ReadLine(); }
                    }
                }
                catch { }
                if (Key.KeyChar.ToString().ToLower() == "q" || Key.KeyChar.ToString().ToLower() == "й")
                { task = false; }

            }
        }

        public void ChangeRevealed() {
            int changeRevealed = revealed;
            bool task = true;

            while (task)
            {
                Console.Clear();
                int choice = 0;
                if (revealed == changeRevealed)
                {
                    Console.Write($"Текущее количество открытах букв\nустановлено на значении: {changeRevealed}\n\t");
                }
                else
                {
                    Console.Write($"Текущее количество открытах букв\nустановлено на значении: {changeRevealed} (Ранее: {faults})\n\t");
                }
                Console.Write($"Вы бы хотели:\n\t1) Сохранить текущее значение\n\t2) Изменить значение\n\n\tQ) Отменить действие");
                var Key = Console.ReadKey(true);
                try
                {
                    choice = Int32.Parse(Key.KeyChar.ToString());
                    if (choice == 1)
                    {
                        revealed = changeRevealed;
                    }
                    else if (choice == 2)
                    {
                        try
                        {
                            Console.Clear();
                            Console.Write("Введите значение: ");
                            if (changeRevealed > 0)
                            {
                                changeRevealed = Int32.Parse(Console.ReadLine());
                            }
                        }
                        catch { Console.Write("Введеное вами значение не подходит под данный параметр"); Console.ReadLine(); }
                    }
                }
                catch { }
                if (Key.KeyChar.ToString().ToLower() == "q" || Key.KeyChar.ToString().ToLower() == "й")
                { task = false; }

            }
        }

        public void Settings() {
            int choice = 0;
            bool task = true;
            while (task)
            {
                Console.Clear();
                Console.Write("Выберите опцию:" +
                    "\n\t1) Изменить кол-во открытых букв" +
                    "\n\t2) Изменить макс. Кол-во ошибок" +
                    "\n\t3) Добавить новое слово" +
                    "\n\n\tQ) Назад\n\n");

                var Key = Console.ReadKey(true);
                try
                {
                    choice = Int32.Parse(Key.KeyChar.ToString());
                    if (choice == 1)
                    {
                        ChangeRevealed();
                    }
                    else if (choice == 2)
                    {
                        ChangeFaults();
                    }
                    else if (choice == 3)
                    {
                        addANewWord();
                    }
                }
                catch { }
                if (Key.KeyChar.ToString().ToLower() == "q" || Key.KeyChar.ToString().ToLower() == "й")
                { task = false; }
            }
        }




        public void Out()
        {
            int choice = -1;

            if (File.Exists(wordsFileName_))
            {
                ReadAvialableWords();
            }

            if (File.Exists(recordsFileName_))
            {
                ReadRecords();
            }
            
            Console.Clear();
            
            Console.WriteLine(" ____  _   _  ____ _____ _____    _   _  _    _        _    ");
            Console.WriteLine("| __ )| | | |/ ___| ____|_ _  |  | | | || |  | |      / \\   ");
            Console.WriteLine("|  _ \\| | | | |   |  _|  | || |  | | | || |  | |     / _ \\  ");
            Console.WriteLine("| |_) | |_| | |___| |___ | || |_ | |_| || |__| |__  /____ \\ ");
            Console.WriteLine("|____/ \\____|\\____|_____|__|\\___| \\____||_________|/_/   \\_\\");





            Console.Write("\n\nДобро пожаловать... На кровавую... Арену смерти!\n");
            Console.Write("Выберите пункт:\n\t1) Начать игру\n\t2) Параметры\n\t3) Рекорды\n\n\tQ) Выход\n\n");
            
            var Key = Console.ReadKey(true);
            try
            {
                choice = Int32.Parse(Key.KeyChar.ToString());
                if(choice == 1)
                {

                }
                else if(choice == 2)
                {
                    Settings();
                }
                else if(choice == 3)
                {

                }
            }
            catch { }
            if (Key.KeyChar.ToString().ToLower() == "q" || Key.KeyChar.ToString().ToLower() == "й") 
            { Console.Clear();  Environment.Exit(0); }
        }
    }
}
