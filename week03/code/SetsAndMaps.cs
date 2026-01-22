using System.Text.Json;

public static class SetsAndMaps
 {  // Problem 1: FindPairs
public static string[] FindPairs(string[] words)
{
    // Encode two chars into one int: (c0 << 16) | c1
    static int Encode(string s) => (s[0] << 16) | s[1];
    static int ReverseCode(int code) => ((code & 0xFFFF) << 16) | (code >> 16);

    var seen = new HashSet<int>(words.Length);
    var results = new List<string>();

    foreach (var word in words)
    {
        // Skip words like "aa"
        if (word[0] == word[1]) continue;

        int code = Encode(word);
        int rev = ReverseCode(code);

        if (seen.Contains(rev))
        {
            // Only allocate when a pair is found
            var reversed = new string(new[] { word[1], word[0] });
            results.Add($"{word} & {reversed}");
        }

        seen.Add(code);
    }

    return results.ToArray();
}

    

    // Problem 2: SummarizeDegrees
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            var degree = fields[3].Trim();

            if (!degrees.ContainsKey(degree))
            {
                degrees[degree] = 0;
            }
            degrees[degree]++;
        }

        return degrees;
    }

    // Problem 3: IsAnagram
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize: remove spaces, lowercase
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        if (word1.Length != word2.Length) return false;

        var dict = new Dictionary<char, int>();

        foreach (var c in word1)
        {
            if (!dict.ContainsKey(c)) dict[c] = 0;
            dict[c]++;
        }

        foreach (var c in word2)
        {
            if (!dict.ContainsKey(c)) return false;
            dict[c]--;
            if (dict[c] < 0) return false;
        }

        return dict.Values.All(v => v == 0);
    }

    // Problem 5: EarthquakeDailySummary
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

        var results = new List<string>();
        foreach (var feature in featureCollection.Features)
        {
            var place = feature.Properties.Place;
            var mag = feature.Properties.Mag;
            results.Add($"{place} - Mag {mag}");
        }

        return results.ToArray();
    }
}
