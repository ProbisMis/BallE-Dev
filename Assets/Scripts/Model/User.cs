using System;

namespace Models
{
    [Serializable]
    public class User
    {
        public int id;

        public string username;

        public string device;

        public int points;
        public int total_xp;
        public string user_code;
        public int star_collected;
        public int normal_highest;
        public int streak_highest;
        public int endless_highest;
        public int is_ad_bought;
        public int checkpoint_level;

        public string password;

        public string c_password;

        public string token;

        public string updated_at;



        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
}

