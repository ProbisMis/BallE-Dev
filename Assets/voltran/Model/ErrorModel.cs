using System;

namespace Models
{
    [Serializable]
    public class ErrorModel
    {
        public string[] username;
        public string[] password;
        public string[] device;
        public string[] c_password;
        public string[] user_error;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
}

