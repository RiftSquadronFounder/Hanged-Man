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
        public List<Record> Records = new List<Record>();
        




        public string getName() { return UserName_; }
        public List<string> getWords() { return wordsAvialable_; }

        public List<Record> GetRecords() { return Records; }


        public void ReadRecords()
        {
            
            Records.Clear();
            if (File.Exists(recordsFileName_))
            {
                List<string> words = new List<string>();
                string FileContainings = File.ReadAllText(recordsFileName_);
                int counter = 0;
                words = FileContainings.Split('|').ToList();
                Record rec = new Record();


                for (int i = 0; i < words.Count; i++)
                {
                    counter++;
                    if (counter == 1)
                    {
                        rec.setName(words[i]);
                    }
                    else if (counter == 2)
                    {
                        rec.setWord(words[i]);
                    }
                    else if (counter == 3)
                    {
                        rec.setFaults(Int32.Parse(words[i]));
                    }
                    else if (counter == 4)
                    {
                        rec.setRightChoices(Int32.Parse(words[i]));
                    }
                    else if (counter == 5)
                    {
                        try
                        {
                            for (int k = 0; k < words[i].Length; k++)
                            {
                                rec.AddLettersFound(Int32.Parse(words[i][k].ToString()));
                            }
                        }
                        catch { }
                    }
                    else if (counter == 6)
                    {
                        try
                        {
                            for (int k = 0; k < words[i].Length; k++)
                            {
                                rec.AddPreRevealed(Int32.Parse(words[i][k].ToString()));
                            }
                        }
                        catch { }

                        counter = 0;
                        Records.Add(rec);
                        rec = new Record();
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
                    $"{string.Join("", r.GetLetters())}|{string.Join("", r.GetRevealed())}|";
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
                Console.Write($"Вы ввели слово:\n\t{word}\nВерно?\n\t1) Да, добавить слово\n\t2) Ввести заново\n\n\tQ) Отменить действие");
                var Key = Console.ReadKey(true);
                try
                {
                    choice = Int32.Parse(Key.KeyChar.ToString());
                    if (choice == 1)
                    {
                        wordsAvialable_.Add(word);
                        RewriteAvialableWords();
                        task = false;
                    }
                    else if (choice == 2)
                    {

                    }
                    
                }
                catch { }
                if (Key.KeyChar.ToString().ToLower() == "q" || Key.KeyChar.ToString().ToLower() == "й")
                { task = false; }

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
                    "\n\t2) Изменить макс. Кол-во ошибsок" +
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




        void DisplayRecords() {
            Console.WriteLine("\n\n Начало истории рекордов!!\n\n");
            for (int i = 0; i < Records.Count; i++)
            {
                Record pl = Records[i];
                string word = pl.GetWord(), rWord = "";
                for(int k = 0; k < word.Length; k++)
                {
                    /*for(int b = 0; b < pl.GetLetters().Count; b++)
                    {
                        Console.WriteLine(pl.GetLetters()[b]);

                    }*/
                    if (pl.GetLetters().Contains(k) || pl.GetRevealed().Contains(k) ) {
                        rWord = $"{rWord}{word[k]}";    
                    }
                    else { rWord = $"{rWord}_"; }

                }
                Console.Write
                    ("________________________________________ \n" +
                    $"| Имя игрока        >> {pl.GetName()} \n" +
                    $"| Угаданные буквы   >> {pl.GetRightGuesses()} \n" +
                    $"| Провалено         >> {pl.GetWrongGuesses()} \n" +
                    $"| \n" +
                    $"| Изначальное слово >> {pl.GetWord()} \n" +
                    $"| Полученное слово  >> {rWord}\n\n");
            }
            Console.WriteLine("\n\nНажмите Enter, для того, чтобы перейти обратно в меню");
            Console.ReadLine();
        
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

            while (true)
            {
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
                    if (choice == 1)
                    {
                        Game game = new Game(UserName_, faults, revealed, wordsAvialable_);
                        Record match = game.Play();

                        if (match.GetWrongGuesses() > 0 || match.GetRightGuesses() > 0)
                        {
                            Records.Add(match);
                            RewriteRecords();
                        }
                    }
                    else if (choice == 2)
                    {
                        Settings();
                    }
                    else if (choice == 3)
                    {
                        DisplayRecords();
                    }
                }
                catch { }
                if (Key.KeyChar.ToString().ToLower() == "q" || Key.KeyChar.ToString().ToLower() == "й")
                { Console.Clear(); Environment.Exit(0); }
            }
        }
    }
}
