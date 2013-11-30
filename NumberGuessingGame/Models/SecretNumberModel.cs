using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SecretNumber.Models
{
    public enum Outcome
    {
        Indefinite, 
        Low, 
        High, 
        Correct, 
        NoMoreGuesses, 
        PreviousGuess
    }

    public class SecretNumberModel
    {
        private int number;
        private List<int> previousGuesses;
        public const int maxNumberOfGuesses = 7;

        [DisplayName("Gissning")]
        [Required(ErrorMessage = "Du måste ange ett tal")]
        [Range(1, 100, ErrorMessage = "Ange ett heltal mellan 1 och 100")]
        public int Guess { get; set; }
        public Outcome Result { get; set; }

        public int Number
        {
            get
            {
                return number;
            }
        }

        public ReadOnlyCollection<int> PreviousGuesses
        {
            get
            {
                return previousGuesses.AsReadOnly();
            }
        }

        public bool CanMakeGuess
        {
            get
            {
                return previousGuesses.Count < MaxNumberOfGuesses;
            }
        }

        public int Count
        {
            get
            {
                return previousGuesses.Count;
            }
        }

        public int MaxNumberOfGuesses
        {
            get
            {
                return maxNumberOfGuesses;
            }
        }

        public SecretNumberModel()
        {
            previousGuesses = new List<int>();
            number = new Random().Next(1, 100);
        }

        public void MakeGuess(int guess)
        {
            Guess = guess;
            foreach (int currentGuess in previousGuesses)
            {
                if (guess == currentGuess)
                {
                    Result = Outcome.PreviousGuess;
                    return;
                }
            }

            previousGuesses.Add(guess);

            if (guess == number)
            {
                Result = Outcome.Correct;
                return;
            }

            if (CanMakeGuess)
            {
                if (guess > number)
                {
                    Result = Outcome.High;
                }
                else if (guess < number)
                {
                    Result = Outcome.Low;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                Result = Outcome.NoMoreGuesses;
            }
        }
    }
}