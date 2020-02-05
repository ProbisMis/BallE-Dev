using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LethalColliderDelegate : MonoBehaviour
{

    Player Parent;

    public static int multipleCollision = 0;

    bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        Parent = transform.parent.gameObject.GetComponent<Player>();
    }



    void OnTriggerEnter(Collider other)
    {

        if (isColliding) return;
        isColliding = true;


        if (!Player.isDead)
        {
            if (!other.gameObject.name.Contains("Star"))
            {
                Parent.OnChildTriggerEntered(other, transform.position);
            }
        }
        // Rest of the code
        StartCoroutine(Reset());
    }


    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }
}
