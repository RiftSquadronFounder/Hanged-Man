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

        private List<int> randomlyOpenedLetter_;
        private string userName_;
        private string word_;
        private int faultsAssigned_;
        private int LettersFound_;
        private int maxFaults_;
        private Dictionary<int, string> letters_;
        private bool isRunning_ = true;

        public Record Process_() {
            
            
            
            
            
            
            
            
            return new Record(userName_, word_, faultsAssigned_, LettersFound_, letters_ );
        }

        private void GameView() { 
        
        
        
        
        
        }

        private void TrySymbol(int index, string symbol) { }

        private void Fail() { }

        private void Win() { }

        private void AssignName() {

            Console.WriteLine
                ("Похоже на то, что вы не указали имя перед началом игры\n" +
                "Не хотели бы вы указать ваше имя для таблицы рекордов?");
        }


    }
}
