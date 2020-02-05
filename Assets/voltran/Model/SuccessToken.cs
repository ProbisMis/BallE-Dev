using System;

namespace Models
{
    [Serializable]
    public class SuccessToken
    {
        public Token success;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
}

