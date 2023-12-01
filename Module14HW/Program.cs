using System;
using System.Collections.Generic;
using System.Linq;

class Karta
{
    public string Suit { get; }
    public string Rank { get; }

    public Karta(string suit, string rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

class Player
{
    public List<Karta> Hand { get; } = new List<Karta>();

    public void AddKarta(Karta karta)
    {
        Hand.Add(karta);
    }

    public void ShowHand()
    {
        Console.WriteLine("Player's Hand:");
        foreach (var karta in Hand)
        {
            Console.WriteLine(karta);
        }
    }
}

class Game
{
    private List<Karta> deck;
    private List<Player> players;
    private Random random = new Random();

    public Game(int numberOfPlayers)
    {
        players = new List<Player>(numberOfPlayers);
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players.Add(new Player());
        }

        InitializeDeck();
        ShuffleDeck();
        DealCards();
    }

    private void InitializeDeck()
    {
        deck = new List<Karta>();
        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] ranks = { "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                deck.Add(new Karta(suit, rank));
            }
        }
    }

    private void ShuffleDeck()
    {
        deck = deck.OrderBy(card => random.Next()).ToList();
    }

    private void DealCards()
    {
        int numberOfPlayers = players.Count;
        int cardsPerPlayer = deck.Count / numberOfPlayers;

        for (int i = 0; i < numberOfPlayers; i++)
        {
            List<Karta> playerHand = deck.Skip(i * cardsPerPlayer).Take(cardsPerPlayer).ToList();
            players[i].Hand.AddRange(playerHand);
        }
    }

    public void Play()
    {
        int currentPlayerIndex = 0;
        while (deck.Any())
        {
            Karta topCard = deck.First();
            Console.WriteLine($"Player {currentPlayerIndex + 1} plays: {topCard}");
            players[currentPlayerIndex].AddKarta(topCard);
            deck.RemoveAt(0);
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

        Console.WriteLine("Game Over!");
        for (int i = 0; i < players.Count; i++)
        {
            Console.WriteLine($"Player {i + 1}'s Hand:");
            players[i].ShowHand();
        }
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter the number of players: ");
        int numberOfPlayers = int.Parse(Console.ReadLine());

        Game game = new Game(numberOfPlayers);
        game.Play();
        Console.ReadKey();
    }
}
