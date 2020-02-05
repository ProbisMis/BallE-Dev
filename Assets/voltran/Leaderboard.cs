using System;

namespace Models
{
    [Serializable]
    public class Leaderboard
    {
        public User[] success;

        public int myRank;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
}

