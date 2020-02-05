using System;

namespace Models
{
    [Serializable]
    public class Token
    {
        public string token;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
}

