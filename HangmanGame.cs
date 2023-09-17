new HangmanGame().Run();

public class HangmanGame
{
    int remainingGuesses = 5;
    List<char> incorrectLetters = new List<char>();
    List<char> correctLetters = new List<char>();
    int minWordLength = 5;

    Player player = new Player();
    Dictionary dictionary = new Dictionary();
    Word word;

    public HangmanGame()
    {
        word = dictionary.PickRandomWord(minWordLength);
    }

    public void Run()
    {
        while (remainingGuesses > 0)
        {
            DisplayGameState();
            List<char> guessedLetters = incorrectLetters.Concat(correctLetters).ToList();
            char guessedLetter;
            do
            {
                guessedLetter = player.PickLetter();
            } 
            while (guessedLetters.Contains(guessedLetter));
            
            Console.WriteLine();
            UpdateGameState(guessedLetter);
            if (HasWon())
            {
                DisplayFinalGameState();
                Console.WriteLine("You Won!");
                return;
            }  
        }
        DisplayFinalGameState();
        Console.WriteLine($"You Lost.");
        return;
    }

    private void DisplayGameState()
    {
        char[] letters = new char[word.Length];

        for (int i = 0; i < word.Length; i++)
        {
            letters[i] = correctLetters.Contains(word.Letters[i]) ? char.ToUpper(word.Letters[i]) : '_';
        }
        Console.Write($"Word: {string.Join(" ", letters)} | "
                    + $"Remaning: {string.Join("", remainingGuesses)} | "
                    + $"Incorrect: {string.Join("", incorrectLetters.Select(x => char.ToUpper(x)).ToList())} | "
                    + $"Guess: ");
    }

    private void DisplayFinalGameState()
    {
        Console.WriteLine($"Word: {string.Join(" ", word.Letters.Select(x => char.ToUpper(x)).ToList())} | "
                        + $"Incorrect: {string.Join("", incorrectLetters.Select(x => char.ToUpper(x)).ToList())}");
    }

    private void UpdateGameState(char letter)
    {
        if(word.Letters.Contains(letter)) correctLetters.Add(letter);
        else
        {
            remainingGuesses--;
            incorrectLetters.Add(letter);
        }
    }

    private bool HasWon()
    {
        foreach(char letter in word.Letters)
        {
            if(!correctLetters.Contains(letter)) return false;
        }
        return true;
    }
}


public class Player
{
    public char PickLetter()
    {
        while (true)
        {
            ConsoleKey key = Console.ReadKey().Key;
            char letter = key switch
            {
                ConsoleKey.A => 'a',
                ConsoleKey.B => 'b',
                ConsoleKey.C => 'c',
                ConsoleKey.D => 'd',
                ConsoleKey.E => 'e',
                ConsoleKey.F => 'f',
                ConsoleKey.G => 'g',
                ConsoleKey.H => 'h',
                ConsoleKey.I => 'i',
                ConsoleKey.J => 'j',
                ConsoleKey.K => 'k',
                ConsoleKey.L => 'l',
                ConsoleKey.M => 'm',
                ConsoleKey.N => 'n',
                ConsoleKey.O => 'o',
                ConsoleKey.P => 'p',
                ConsoleKey.Q => 'q',
                ConsoleKey.R => 'r',
                ConsoleKey.S => 's',
                ConsoleKey.T => 't',
                ConsoleKey.U => 'u',
                ConsoleKey.V => 'v',
                ConsoleKey.W => 'w',
                ConsoleKey.X => 'x',
                ConsoleKey.Y => 'y',
                ConsoleKey.Z => 'z',
                _ => ' '
            };
            if (letter == ' ') continue;
            return letter;
        }
    }
}


public class Word
{
    public string word;
    public int Length {get => word.Length;}
    public char[] Letters {get => word.ToCharArray();}

    public Word(string word)
    {
        this.word = word;
    }
}

public class Dictionary
{
    List<Word> words = new List<Word>();
    FileManager manager = new FileManager();
    
    public Dictionary()
    {
        Initialize();
    }
    public Word PickRandomWord(int minLength)
    {
        Random random = new Random();
        while (true)
        {
        int index = random.Next(words.Count);
        Word chosenWord = words[index];
        if (chosenWord.Length >= minLength) return chosenWord;
        }
    }

    private void Initialize()
    {
        string[] wordStrings = manager.LoadData();
        foreach(string word in wordStrings)
        {
            words.Add(new Word(word));
        }
    }
}

public class FileManager
{
    private const string fileName = "Words.txt";

    public string[] LoadData()
    {
        string[] wordStrings = new string[] {};
        if(File.Exists(fileName))
        {
            wordStrings = File.ReadAllLines(fileName);
        }
        return wordStrings;
    }

}
