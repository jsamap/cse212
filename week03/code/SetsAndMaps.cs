using System.Text.Json;
using System.Threading.Channels;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        HashSet<string> wordsSet = new(words);
        HashSet<string> pairs = new();
        foreach (string word in words)
        {
            string reversedWord = String.Join("",word.Reverse());
            if (wordsSet.Contains(reversedWord) && !pairs.Contains($"{reversedWord} & {word}") && word!=reversedWord)
                pairs.Add($"{word} & {reversedWord}");
        }
        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (degrees.ContainsKey(fields[3]))
                degrees[fields[3]] += 1;
            else 
                degrees[fields[3]] = 1;
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        Dictionary<string, int> letters1 = new();
        Dictionary<string, int> letters2 = new();
        
        word1 = word1.ToUpper().Replace(" ","");
        word2 = word2.ToUpper().Replace(" ","");

        if (word1.Length != word2.Length)
            return false;

        for (int i=0; i<word1.Length; i++)
        {
            if (letters1.ContainsKey(word1[i].ToString()))
                letters1[word1[i].ToString()] ++;
            else 
                letters1[word1[i].ToString()] = 1;

            
            if (letters2.ContainsKey(word2[i].ToString()))
                letters2[word2[i].ToString()] ++;
            else 
                letters2[word2[i].ToString()] = 1;
        }
        
        foreach (string key in letters1.Keys)
            if (!letters2.ContainsKey(key) || letters1[key] != letters2[key])
                return false;
        
        foreach (string key in letters2.Keys)
            if (!letters1.ContainsKey(key) || letters1[key] != letters2[key])
                return false;

        return true;

        // return letters1.Count == letters2.Count && 
        //     letters1.All(kv => letters2.TryGetValue(kv.Key, out var value) && 
        //                     Equals(kv.Value, value));

    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        List<string> earthquakes = new();

        foreach (Feature feature in featureCollection.Features)
        {
            earthquakes.Add($"{feature.Properties.Place} - Mag {feature.Properties.Mag}");
        }

        return earthquakes.ToArray();
    }
}