using System;

class Mastermind
{
    static void Main()
    {
        Random random = new Random();
        string secretCode = "";

        // Generate a random 4-digit secret code with digits from 1 to 6
        for (int i = 0; i < 4; i++)
        {
            secretCode += random.Next(1, 7).ToString();
        }

        Console.WriteLine("Welcome to Mastermind! Try to guess the 4-digit code.");
        Console.WriteLine("Each digit ranges from 1 to 6.");

        bool isGuessedCorrectly = false;

        for (int attempt = 1; attempt <= 10; attempt++)
        {
            Console.Write($"Attempt {attempt}/10: Enter your guess: ");
            string guess = Console.ReadLine();

            if (guess.Length != 4 || !IsValidGuess(guess))
            {
                Console.WriteLine("Invalid input. Please enter a 4-digit number using digits 1-6.");
                attempt--;
                continue;
            }

            string feedback = GetFeedback(secretCode, guess);
            Console.WriteLine("Hint: " + feedback);

            if (feedback == "++++")
            {
                isGuessedCorrectly = true;
                break;
            }
        }

        if (isGuessedCorrectly)
        {
            Console.WriteLine("Congratulations! You've guessed the code!");
        }
        else
        {
            Console.WriteLine("You've run out of attempts. The secret code was: " + secretCode);
        }
    }

    static bool IsValidGuess(string guess)
    {
        foreach (char c in guess)
        {
            if (c < '1' || c > '6')
            {
                return false;
            }
        }
        return true;
    }

    static string GetFeedback(string secretCode, string guess)
    {
        int plusCount = 0;
        int minusCount = 0;
        bool[] secretUsed = new bool[4];
        bool[] guessUsed = new bool[4];

        // Count '+' for correct position and value
        for (int i = 0; i < 4; i++)
        {
            if (guess[i] == secretCode[i])
            {
                plusCount++;
                secretUsed[i] = true;
                guessUsed[i] = true;
            }
        }

        // Count '-' for correct value but wrong position
        for (int i = 0; i < 4; i++)
        {
            if (guessUsed[i]) continue;

            for (int j = 0; j < 4; j++)
            {
                if (!secretUsed[j] && guess[i] == secretCode[j])
                {
                    minusCount++;
                    secretUsed[j] = true;
                    break;
                }
            }
        }

        return new string('+', plusCount) + new string('-', minusCount);
    }
}
