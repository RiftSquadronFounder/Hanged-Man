using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hanged_Man
{
    internal class Record
    {
        private string userName_;
        private string word_;
        private int faults_;
        private int pickedRight_;
        private List<int> lettersFound_ = new List<int>();
        private List<int> PrevRevealed_ = new List<int>();


        public Record(string name, string word, int faults, int rightOnes, List<int> letters, List<int> PrevRevealed)
        {
            this.userName_ = name;
            this.word_ = word;
            this.faults_ = faults;
            this.pickedRight_ = rightOnes;
            this.lettersFound_ = letters;
            this.PrevRevealed_ = PrevRevealed;
        }

        public Record()
        {
        }

        public void setName(string name) {
            userName_ = name;
        }

        public void setWord(string word) {
            word_ = word;
        }

        public void setFaults(int faults) {
            faults_ = faults;
        }

        public void setRightChoices(int rightChoices) {
            pickedRight_ = rightChoices;
        }
        public void AddPreRevealed(int index)
        {
            PrevRevealed_.Add(index);
        }
        public void AddLettersFound(int index)
        {
            lettersFound_.Add(index);
        }



        public void InfoOut()
        {
            string rWord = "";
            for (int k = 0; k < word_.Length; k++)
            {

                if (lettersFound_.Contains(k) || PrevRevealed_.Contains(k))
                {
                    rWord = $"{rWord}{word_[k]}";
                }
                else { rWord = $"{rWord}_"; }

            }
            Console.Write
                ("________________________________________ \n" +
                $"| Имя игрока        >> {userName_} \n" +
                $"| Угаданные буквы   >> {pickedRight_} \n" +
                $"| Провалено         >> {faults_} \n" +
                $"| \n" +
                $"| Изначальное слово >> {word_} \n" +
                $"| Полученное слово  >> {rWord}\n\n"); } 
        
    





        public string GetName() { return userName_; }
        public string GetWord() { return word_; }
        public int GetWrongGuesses() {  return faults_; }
        public int GetRightGuesses() { return pickedRight_; }

        public List<int> GetLetters() {  return lettersFound_; }

        public List<int> GetRevealed() { return PrevRevealed_; }

    }

}
