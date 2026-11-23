using System.ComponentModel;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Security;
/*
* Name: Brynee Snow
* Course: CSCI 1250, Section 002
* Assignment: Lab 08, Game Night
* Date: November 18, 2026
* Description: Keeps a game night leaderboard in three text files. Shows the
* season so far, records tonight's points, saves the new totals,
* and adds tonight's result to a history that grows every week.
*/


// CONSTANTS
const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;

// Look for the data files next to Program.cs, however the program started.
string projectFolder = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);


// PART 3a
string[] history = File.ReadAllLines(HISTORY_FILE);

System.Console.WriteLine("=== GAME NIGHT LEADERBOARD ===");
int tonightNumber = history.Length + 1;

if (history.Length == 0)
{
    Console.WriteLine("No nights played yet.");
}
else
{
    Console.WriteLine($"Nights played: {history.Length}");
    // foreach is used here because we need each line of the history file printed for the user
    // and the position of them does not matter so we do not need to use a for loop
    foreach (string line in history)
    {
        Console.WriteLine($"  {line}");
    }
}
Console.WriteLine($"Tonight is Night {tonightNumber}.");
System.Console.WriteLine("");

// === Part 1: The Standings ===

string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);


System.Console.WriteLine("PLAYER".PadRight(11) + "POINTS");
// for is used here opposed to for each because of how we want to display the leaderboard,
// the only way to get the players and points next to each other is having them in the same loop
// and since this lab uses two text files and calls them from position opposed to something like
// player name we are able to use the same variable i for both player and point
for(int i = 0; i < players.Length; i++)
{
    System.Console.WriteLine(players[i].PadRight(11) + points[i]);
}

int leader = LeaderIndex(points);
System.Console.WriteLine($"Leader: {players[leader]} with {points[leader]}");

int totalPoints = TotalPoints(points);
System.Console.WriteLine($"Season total: {totalPoints}");
System.Console.WriteLine("");


// === Part 2: Game Night === 

int bestTonight = 0;
string winnerTonight = "";

// the for loop is needed opposed to the foreach loop because it is dependent on the position in
// the two text files
for(int i = 0; i < players.Length; i++)
{
    System.Console.Write($"Points tonight for {players[i]}: ");
    int score = Convert.ToInt32(Console.ReadLine());
    // the while loop is needed here because it is vital that a useable number is imputed by the user
    // so while tells the user to reenter the number until it matches the range the program needs it to be
    while (score > MAX_POINTS || score < 0)
    {
        System.Console.WriteLine($"Points must be from 0 to {MAX_POINTS}.");
        Console.Write($"Points tonight for {players[i]}: ");
        score = Convert.ToInt32(Console.ReadLine());
    }

 int season = Convert.ToInt32(points[i]) + score;
    points[i] = season.ToString();

if (score > bestTonight)
    {
        bestTonight = score;
        winnerTonight = players[i];
    }
}

System.Console.WriteLine($"Tonight's winner: {winnerTonight} with {bestTonight}");
System.Console.WriteLine("");

File.WriteAllLines(POINTS_FILE, points);
// saved points.txt
Console.WriteLine($"Saved {POINTS_FILE}.");

// PART 3b

string[] entry = { HistoryEntry(tonightNumber, winnerTonight, bestTonight) };
File.AppendAllLines(HISTORY_FILE, entry);
Console.WriteLine($"Added to {HISTORY_FILE}: {entry[0]}");




//THESE ARE ALL MY METHODS


// this method hands back the position of the player with the most points / the leader
static int LeaderIndex(string[] points)
{
int highest = 0;

// the for loop is used here because we need to cycle through all the points that are stored in
// our files, but it also needs to be a for and not a foreach because we need the position of the points in the text file
    for(int i = 1; i < points.Length; i++)
    {
        if(Convert.ToInt32(points[i]) > Convert.ToInt32(points[highest]))
        {
            highest = i;
        }
    }
return highest;
} 

// this method hands back the math for adding the points to the total each time and is a return method
// so it is easier to call on in other spots of the program
static int TotalPoints(string[] points)
{
int total = 0;
// the foreach loop is used here because we need to add the scores to the total without having to
// do it one by one and by hand. using the foreach cycles through each number and adds it to the total
// for us
    foreach(string point in points)
    {
        total += Convert.ToInt32(point);
    }
return total;
}

// hands back one history line
static string HistoryEntry(int night, string winner, int points)
{
    return $"Night {night}: {winner} won with {points} points";
}
