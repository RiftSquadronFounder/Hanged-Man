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
        private Dictionary<int, char> letters;


        public Record(string name, string word, int faults, int rightOnes, Dictionary<int, char> letters)
        {
            this.userName_ = name;
            this.word_ = word;
            this.faults_ = faults;
            this.pickedRight_ = rightOnes;
            this.letters = letters;
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





        public string GetName() { return userName_; }
        public string GetWord() { return word_; }
        public int GetWrongGuesses() {  return faults_; }
        public int GetRightGuesses() { return pickedRight_; }

        public Dictionary<int, char> GetLetters() {  return letters; }

    }

}
