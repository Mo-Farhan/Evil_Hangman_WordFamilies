using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WordGame
{
    class WordFamily
    {
        ArrayList wordFamily;
        public WordFamily ()
        {
            wordFamily = new ArrayList();
        }
        public void Add(string word)
        {
            wordFamily.Add(word);
        }
        public virtual Array ToArray()
        {
            return wordFamily.ToArray();
        }
        public int Count()
        {
            return wordFamily.Count;
        }
        public void RemoveRange(int start, int end)
        {
            wordFamily.RemoveRange(start, end);
        }
        public void AddRange(WordFamily cons)
        {
            foreach (string word in cons.wordFamily)
                this.Add(word);
        }
        public string getWord(int pos)
        {
            return wordFamily[pos].ToString();
        }
    }
}
