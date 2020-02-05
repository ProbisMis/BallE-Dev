using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInput : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;

    private Vector3 direction;
    //private float clickTime;
    private float releaseTime = 0;

    public class ActionOrder
    {

        public float Duration;
        public Vector3 Direction;

        public ActionOrder(float duration, Vector3 direction)
        {
            Duration = duration;
            Direction = direction;
        }
    }

    public static List<ActionOrder> actionOrder = new List<ActionOrder>();

    void Start()
    {
        //up-0.3,right-0.2
        // Debug.Log(actionOrder.Count);
        // actionOrder.Add(new ActionOrder(14, true));
        // Debug.Log(actionOrder.Count);

        // for (int cnt = 0; cnt < actionOrder.Count; cnt++)
        // {
        //     Debug.Log("Speed: " + actionOrder[cnt].Speed + "  Enemy Action:" + actionOrder[cnt].EnemyAction;
        // }

    }
    void OnMouseDown()
    {
        //Debug.Log(actionOrder.Count);
        if (!Player.gameOn)
        {
            if (actionOrder.Count == 0)
                releaseTime = Time.time;
            startPos = Input.mousePosition;
        }
    }
    void OnMouseUp()
    {
        if (!Player.gameOn)
        {
            float diffTime = Time.time - releaseTime;
            releaseTime = Time.time;
            endPos = Input.mousePosition;

            GetDirection();

            actionOrder.Add(new ActionOrder(0, direction));
            if (actionOrder.Count > 1)
                actionOrder[actionOrder.Count - 2].Duration = diffTime;
            actionOrder[actionOrder.Count - 1].Duration = 100;
            Debug.Log(actionOrder[0].Duration + "-" + actionOrder[0].Direction);
        }
    }
    void GetDirection()
    {
        if (Mathf.Abs(endPos.x - startPos.x) > Mathf.Abs(endPos.y - startPos.y))
        {
            if (endPos.x - startPos.x > 0)
                direction = Vector2.right;
            else
                direction = -Vector2.right;
        }
        else
        {
            if (endPos.y - startPos.y > 0)
                direction = Vector2.up;
            else
                direction = -Vector2.up;
        }
    }
}
