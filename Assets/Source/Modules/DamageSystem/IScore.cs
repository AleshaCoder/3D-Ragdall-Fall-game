using System;

namespace DamageSystem
{
    public interface IScore
    {
        event Action<int> OnHighScoreChanged;
        event Action<int> OnScoreChanged;
    }
}