using System;

namespace Test {
    class Program {
        static void Main(string[] args) {

            var msg     = "Hello World Hello World";
            var newMsg  = msg.Split(" ");

            var ctr     = 0;
            var newWord = "";

            foreach (var word in newMsg) {
                var cWord = word;
                if (ctr > 1) {
                    cWord = word.ToLower().Replace("he", "");
                }
                newWord += string.Format("{0} ", cWord);
                ctr++;
            }

            Console.WriteLine(newWord);
        }
    }
}
