using System;
using System.Collections.Generic;
using System.Linq;
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
        private Dictionary<int, string> letters;


        public Record(string name, string word, int faults, int rightOnes, Dictionary<int, string> letters)
        {
            this.userName_ = name;
            this.word_ = word;
            this.faults_ = faults;
            this.pickedRight_ = rightOnes;
            this.letters = letters;
        }


        public string GetName() { return userName_; }
        public string GetWord() { return word_; }
        public int GetWrongGuesses() {  return faults_; }
        public int GetRightGuesses() { return pickedRight_; }

        public Dictionary<int, string> GetLetters() {  return letters; }

    }

}
