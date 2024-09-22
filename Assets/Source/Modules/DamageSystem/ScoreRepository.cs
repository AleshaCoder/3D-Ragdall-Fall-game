using System;
using UnityEngine;

namespace DamageSystem
{
    public class ScoreRepository : IScore
    {
        private const string ScoreData = "score";
        private int _currentScore;
        private int _highScore;

        public event Action<int> OnScoreChanged;
        public event Action<int> OnHighScoreChanged;

        public void Construct()
        {
            if (PlayerPrefs.HasKey(ScoreData))
                _highScore = PlayerPrefs.GetInt(ScoreData);
            OnScoreChanged?.Invoke(_currentScore);
            OnHighScoreChanged?.Invoke(_highScore);
        }

        public void AddScore(int score)
        {
            _currentScore += score;
            OnScoreChanged?.Invoke(_currentScore);
            if (_currentScore > _highScore)
            {
                _highScore = _currentScore;
                PlayerPrefs.SetInt(ScoreData, _highScore);
                PlayerPrefs.Save();
                OnHighScoreChanged?.Invoke(_highScore);
            }
        }

        public void ResetScore()
        {
            if (_currentScore > _highScore)
            {
                _highScore = _currentScore;
                PlayerPrefs.SetInt(ScoreData, _highScore);
                PlayerPrefs.Save();
            }
            _currentScore = 0;
            OnScoreChanged?.Invoke(_currentScore);
        }

        public int GetHighScore()
        {
            return _highScore;
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }
    }
}
