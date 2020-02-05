using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColliderDelegate : MonoBehaviour
{

    Player Parent;
    // Start is called before the first frame update
    void Start()
    {
        Parent = transform.parent.gameObject.GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered from sphere");
        if (other.gameObject.name.Contains("Star"))
        {
            Parent.OnChildTriggerEntered(other, transform.position);
        }

    }
}
