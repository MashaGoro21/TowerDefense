using UnityEngine;


[CreateAssetMenu(fileName = "GameOverConfig", menuName = "TowerDefense/GameOverConfig")]
public class GameOverConfig : ScriptableObject
{
    public int moneyPerWave = 10;
    public int moneyPerKill = 2;
}
