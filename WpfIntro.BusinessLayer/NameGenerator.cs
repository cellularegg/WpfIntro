using System;
using System.Windows.Markup;

namespace WpfIntro.BusinessLayer
{
    public class NameGenerator
    {
        public static string GenerateName(int length)
        {
            Random rand = new Random();
            string[] consonants =
            {
                "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v",
                "w", "x"
            };
            string[] vowels = {"a", "e", "i", "o", "u", "ae", "y"};
            string Name = string.Empty;
            Name += consonants[rand.Next(consonants.Length)].ToUpper();
            Name += vowels[rand.Next(vowels.Length)];
            //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            int characterCount = 2;
            while (characterCount < length)
            {
                Name += consonants[rand.Next(consonants.Length)];
                characterCount++;
                Name += vowels[rand.Next(vowels.Length)];
                characterCount++;
            }

            return Name;
        }
    }
}