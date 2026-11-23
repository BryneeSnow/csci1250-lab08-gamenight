using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Security;

const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;

// Look for the data files next to Program.cs, however the program started.
string projectFolder = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);


// === Part 1: The Standings ===
System.Console.WriteLine("PLAYER".PadRight(11) + "POINTS");
for(int i = 0; i < players.Length; i++)
{
    System.Console.WriteLine(players[i].PadRight(11) + points[i]);
}

int leader = LeaderIndex(points);
System.Console.WriteLine($"Leader: {players[leader]} with {points[leader]}");

int totalPoints = TotalPoints(points);
System.Console.WriteLine($"Season total: {totalPoints}");


// === Part 2: Game Night === 

int bestScoreTonight = 0;
string winnerTonight = "";

for(int i = 0; i < players.Length; i++)
{
    System.Console.Write($"Points tonight for {players[i]}: ");
    int score = Convert.ToInt32(Console.ReadLine());
    while (score > 20 || score < 0)
    {
        System.Console.WriteLine("Points must be from 0 to 20.");
        Console.Write($"Points tonight for {players[i]}: ");
        score = Convert.ToInt32(Console.ReadLine());
    }

 int season = Convert.ToInt32(points[i]) + score;
    points[i] = season.ToString();

  if (score > bestScoreTonight)
    {
        bestScoreTonight = score;
        winnerTonight = players[i];
    }
}
System.Console.WriteLine($"Tonight's Winner: {winnerTonight} with {bestScoreTonight}");

File.WriteAllLines(POINTS_FILE, points);
System.Console.WriteLine("Saved points.txt");





//THESE ARE ALL MY METHODS


static int LeaderIndex(string[] points)
{
int highest = 0;

    for(int i = 1; i < points.Length; i++)
    {
        if(Convert.ToInt32(points[i]) > Convert.ToInt32(points[highest]))
        {
            highest = i;
        }
    }
return highest;
} 



static int TotalPoints(string[] points)
{
int total = 0;
    foreach(string point in points)
    {
        total += Convert.ToInt32(point);
    }
return total;
}