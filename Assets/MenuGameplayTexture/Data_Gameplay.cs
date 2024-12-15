using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Gameplay
{
    public float Score {get; set;}
    public float KilledEnemies {get; set;}
    public float TimePlay {get; set;}

    public int HistoryPlay {get; set;}

    public Data_Gameplay(float score, float killedEnemies, float timePlay, int historyPlay){
        Score = score;
        KilledEnemies = killedEnemies;
        TimePlay = timePlay;
        HistoryPlay = historyPlay;
    }
}
