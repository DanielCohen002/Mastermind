using System;

class Mastermind
{
    static void Main()
    {
        Random random = new Random();
        string secretCode = "";
        bool isGuessedCorrectly = false;

        // Generate a random 4-digit secret code with digits from 1 to 6
        for (int i = 0; i < 4; i++)
        {
            secretCode += random.Next(1, 7).ToString();
        }

        Console.WriteLine("Welcome to Mastermind! Try to guess the 4-digit code.");
        Console.WriteLine("Each digit ranges from 1 to 6.");

        //Start of user's turn. Take 'guess' input from user
        for (int attempt = 1; attempt <= 10; attempt++)
        {
            Console.Write($"Attempt {attempt}/10: Enter your guess: ");
            string guess = Console.ReadLine();

            if (!IsValidGuess(guess))
            {
                Console.WriteLine("Invalid input. Please enter a 4-digit number using digits 1-6.");
                attempt--;
                continue;
            }

            string feedback = GetFeedback(secretCode, guess);
            Console.WriteLine($"Hint: {feedback}");

            if (feedback == "++++")
            {
                isGuessedCorrectly = true;
                break;
            }
        }

        //Game over screen
        if (isGuessedCorrectly)
        {
            Console.WriteLine("Congratulations! You've guessed the code!");
        }
        else
        {
            Console.WriteLine($"You've run out of attempts. The secret code was: {secretCode}");
        }
    }

    //Check if 'guess' input is correctly fomatted
    static bool IsValidGuess(string guess)
    {
        if (guess.Length != 4)
        {
            return false;
        }
        foreach (char c in guess)
        {
            if (c < '1' || c > '6')
            {
                return false;
            }
        }
        return true;
    }

    //Check 'guess' against 'secretCode'. Return Hint based on number of matching digits
    static string GetFeedback(string secretCode, string guess)
    {
        int plusCount = 0;
        int minusCount = 0;
        bool[] secretUsed = new bool[4];
        bool[] guessUsed = new bool[4];

        // Count '+' for correct position and value, record already checked digits
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
        string feedback = "";

        // Add '+' signs for correct positions
        for (int i = 0; i < plusCount; i++)
        {
            feedback += "+";
        }

        // Add '-' signs for correct values in wrong positions
        for (int i = 0; i < minusCount; i++)
        {
            feedback += "-";
        }

        return feedback;
    }
}
