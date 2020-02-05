using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using System;

namespace Models
{
    [Serializable]
    public class ErrorHandler
    {

        public ErrorModel error;


        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
}



