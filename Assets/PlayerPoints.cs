using Points;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPoints
{
    private static float _value;
    public static float Value {
        get => _value;
        set { _value = value; ValueChanged?.Invoke(_value); }
    }

    private static Highscores _highscores;
    public static Highscores Highscores
    {
        get
        {
            if (_highscores is null)
            {
                string jsonString = PlayerPrefs.GetString("highscoreTable");
                Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
                if (highscores is null)
                {
                    _highscores = new Highscores();
                }
                else
                {
                    _highscores = highscores;
                }
            }

            _highscores.Values.Sort();

            return _highscores;
        }
    }

    public delegate void ValueChangedEventHandler(float value);
    public static event ValueChangedEventHandler ValueChanged;

    public static void Reset()
    {
        Value = 0;
    }

    public static void AddHighscoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // Add new entry to Highscores
        Highscores.Values.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(_highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
}

namespace Points
{
    public class Highscores
    {
        public List<HighscoreEntry> Values = new List<HighscoreEntry>();
    }

    [Serializable]
    public class HighscoreEntry: IComparable<HighscoreEntry>
    {
        public int score;
        public string name;

        public int CompareTo(HighscoreEntry other) => -score.CompareTo(other.score);
    }
}
