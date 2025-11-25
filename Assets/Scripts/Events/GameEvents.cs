using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;
    public static void ScoreChanged(int score) => OnScoreChanged?.Invoke(score);
}
