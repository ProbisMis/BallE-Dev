using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpecialty : MonoBehaviour
{
    public bool isMoving = false;
    public Vector3 direction = Vector3.right;
    public float distance = 0;
    private Vector3 originalPos;

    void Start()
    {
        Set();
    }
    void Set()
    {
        // if (transform.position.x > 0)
        // {
        //     direction = -Vector3.right;
        // }
        // else
        //     direction = Vector3.right;

        // distance = Mathf.Abs(transform.position.x) + 1f;
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && !LevelCreator.editMap)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime);
            if (Vector3.Distance(originalPos, transform.position) > distance)
            {
                direction = -direction;
                originalPos = transform.position;
            }
        }
    }

    public void Freeze()
    {
        if (isMoving)
        {
            isMoving = false;
            StartCoroutine(BreakFreeze());
            //Make Some Ice Animation
        }

    }

    IEnumerator BreakFreeze()
    {
        yield return new WaitForSeconds(2f);
        isMoving = true;
    }
}
