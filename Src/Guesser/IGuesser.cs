using System;
using System.Collections.Generic;
using System.Text;
using WikiDataNameGuesser.Models;

namespace WikiDataNameGuesser.Guesser
{
    public interface IGuesser
    {
        string GuessGivenName(Human human);
        string GuessFamilyName(Human human);
        Human FillNames(Human human);
    }
}
