using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GXPEngine
{
    public class HighscoreSaving
    {

	    public HighscoreSaving()
	    {
	    }
	    
        public bool SaveData(string filename, int score, string name, bool removeLowest = false) {
	        if (File.Exists(filename))
	        {
		        List<Score> highScore = LoadData(filename);
		        if (removeLowest)
		        {
			        highScore = highScore.OrderByDescending(x => x.score).ToList();
			        highScore.RemoveAt(highScore.Count - 1);
		        }
		        try {
			        // StreamWriter: For writing to a text file - requires System.IO namespace:
			        // Note: the "using" block ensures that resources are released (writer.Dispose is called) when an exception occurs
			        using (StreamWriter writer = new StreamWriter(filename)) {
				        if (highScore.Count != 0)
				        {
					        foreach (Score highscore in highScore)
					        {
						        writer.WriteLine("Score=" + highscore.score + "," + highscore.name);
					        }
				        }

				        writer.WriteLine("Score=" + score + "," + name);

				        writer.Close();

				        Console.WriteLine("Scores successfully saved:  " + filename);
				        return true;
			        }
		        } catch (Exception error) {
			        Console.WriteLine("Error while trying to save: {0}",error.Message);
			        return false;
		        }
	        }
	        try {
			// StreamWriter: For writing to a text file - requires System.IO namespace:
			// Note: the "using" block ensures that resources are released (writer.Dispose is called) when an exception occurs
			using (StreamWriter writer = new StreamWriter(filename)) {

				writer.WriteLine("Score=" + score + "," + name);

				writer.Close();

				Console.WriteLine("Scores successfully saved:  " + filename);
				return true;
			}
		} catch (Exception error) {
			Console.WriteLine("Error while trying to save: {0}",error.Message);
			return false;
		}
	}

	public List<Score> LoadData(string filename)
	{
		List<Score> highScore = new List<Score>();
		if (!File.Exists(filename)) {
			Console.WriteLine("No save file found!");
			return null;
		}
		try {
			// StreamReader: For reading a text file - requires System.IO namespace:
			// Note: the "using" block ensures that resources are released (reader.Dispose is called) when an exception occurs
			using (StreamReader reader = new StreamReader(filename)) {
				string line = reader.ReadLine();
				while (line != null) {
					// Here's a demo of different string parsing methods:

					// Find the position of the first '=' symbol (-1 if doesn't exist)
					int splitPos = line.IndexOf('=');
					if (splitPos >= 0) {
						// Everything before the '=' symbol:
						string key = line.Substring(0, splitPos);
						// Everything after the '=' symbol:
						string value = line.Substring(splitPos + 1);

						// Split value up for every comma:
						string[] numbers = value.Split(',');
						
								if (numbers.Length == 2) {
									// These may trigger an exception if the string doesn't represent a float value:
									highScore.Add(new Score(numbers[1],int.Parse(numbers[0])));
								}
					}
					line = reader.ReadLine();
				}
				reader.Close();
				highScore = highScore.OrderByDescending(x => x.score).ToList();
				Console.WriteLine("Load from {0} successful ", filename);
				return highScore;
			}
		} catch (Exception error) {
			Console.WriteLine("Error while reading save file: {0}",error.Message);
		}

		return null;
	}
    }
}