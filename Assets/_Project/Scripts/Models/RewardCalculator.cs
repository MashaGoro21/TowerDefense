using System;

namespace Models
{
    public static class RewardCalculator
    {
        public static int Calculate(int wavesCompleted, int enemiesKilled, int moneyPerWave, int moneyPerKill)
        {
            if (wavesCompleted < 0 || enemiesKilled < 0 || moneyPerWave < 0 || moneyPerKill < 0)
                throw new ArgumentOutOfRangeException("Reward values cannot be negative");

            return wavesCompleted * moneyPerWave + enemiesKilled * moneyPerKill;
        }
    }
}