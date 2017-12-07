using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WikiDataNameGuesser.Models;

namespace WikiDataNameGuesser.Guesser
{
    public class Guesser : IGuesser
    {
        public Human FillNames(Human human)
        {
            if (human == null)
            {
                return null;
            }
            human.FamilyName = GuessFamilyName(human);
            human.GivenName = GuessGivenName(human);
            return human;
        }

        public string GuessFamilyName(Human human)
        {
            if(human == null)
            {
                return null;
            }
            if (!string.IsNullOrWhiteSpace(human.FamilyName) ||
                (string.IsNullOrEmpty(human.Label) && string.IsNullOrEmpty(human.BirthName)))
            {
                //either already set, or no label and no birthname
                return human.FamilyName;
            }

            var fromLabel = DeduceFamilyName(human.Label, human);
            if (string.IsNullOrEmpty(human.BirthName))
            {
                //no need to check if the birthname gives a useful value as that's null
                return fromLabel;
            }
            var fromBirthName = DeduceFamilyName(human.BirthName, human);


            if (fromLabel == fromBirthName || string.IsNullOrEmpty(human.Label))
            {
                //either they both gave the same answer, or label is null so should be ignored anyway
                return fromBirthName;
            }

            return null;
        }

        public string GuessGivenName(Human human)
        {
            if (human == null)
            {
                return null;
            }
            if (!string.IsNullOrWhiteSpace(human.GivenName) || 
                (string.IsNullOrEmpty(human.Label) && string.IsNullOrEmpty(human.BirthName)))
            {
                //either already set, or no label and no birthname
                return human.GivenName;
            }

            var fromLabel = DeduceGivenName(human.Label, human);
            if (string.IsNullOrEmpty(human.BirthName))
            {
                //no need to check if the birthname gives a useful value as that's null
                return fromLabel;
            }
            var fromBirthName = DeduceGivenName(human.BirthName, human);


            if(fromLabel == fromBirthName || string.IsNullOrEmpty(human.Label))
            {
                //either they both gave the same answer, or label is null so should be ignored anyway
                return fromBirthName;
            }

            return null;
        }

        private string DeduceGivenName(string label, Human human)
        {
            if (string.IsNullOrEmpty(label))
            {
                return null;
            }

            var parts = label.Split().Select(part => part.Trim());

            if(!string.IsNullOrEmpty(human.FamilyName))
                parts = parts.Where(p => p != human.FamilyName.Trim());

            return parts.FirstOrDefault();
        }

        private string DeduceFamilyName(string label, Human human)
        {
            if (string.IsNullOrEmpty(label))
            {
                return null;
            }

            var parts = label.Split().Select(part => part.Trim());

            if (!string.IsNullOrEmpty(human.GivenName))
                parts = parts.Where(p => p != human.GivenName.Trim());

            return parts.LastOrDefault();
        }
    }
}
