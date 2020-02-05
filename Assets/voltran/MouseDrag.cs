using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    private bool isScrolling = false;
    private Vector3 firstMousePos;
    private Vector3 vel = Vector3.zero;
    private bool afterEffect = false;
    private float startTime;
    private float endTime;
    private float timeDiff;
    private float movePos;
    public Camera cam;
    private float toplimit = 7f;
    private float bottomlimit = 3f;
    private Vector3 afterEffectPos = Vector3.zero;

    public GameObject dragObj;

    Vector3 startPosition;

    float minPos;
    float maxPos;
    void OnDisable()
    {
        ResetPos();
    }

    public void ResetPos()
    {
        afterEffect = false;
        dragObj.transform.position = startPosition;
    }

    void Start()
    {
        startPosition = dragObj.transform.position;
        //////Debug.Log(leaderstodrag.transform.position);
        //if (transform.name == "RulesDrag")
        //    toplimit = 24.3f;
        //if (transform.name == "MBADrag")
        //    toplimit = 136;
        if (transform.name == "LeaderDrag")
        {
            toplimit = -1;
            bottomlimit = 300f;
        }
        if (transform.name == "ShopDrag")
        {
            toplimit = -1;
            bottomlimit = 300f;
        }
        //if (transform.name == "Nasıl-Oynanır")
        //{
        //    toplimit = 8.46f;
        //    bottomlimit = -10.51f;
        //    if (CamRatio.ratio < 0.5f)
        //    {
        //        Vector3 temp = transform.localPosition;
        //        temp.y = -12.87f;
        //        transform.localPosition = temp;
        //        transform.localScale *= 0.85f;
        //        toplimit *= 0.80f;
        //        bottomlimit *= 0.80f;
        //    }
        //}
    }
    private void OnMouseDown()
    {
        //Debug.Log("Mousedown");
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("FIRST TOUCH Y " + hit.point.y);
            firstMousePos = hit.point;
        }
        startTime = Time.time;
    }

    void OnMouseDrag()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            movePos = hit.point.y - firstMousePos.y;
            //Debug.Log("POS1 " + firstMousePos.y);
            //Debug.Log("POS2 " + hit.point.y);
            //Debug.Log("movepos " + movePos);
        }

        Vector3 pos_move = new Vector3(dragObj.transform.position.x, dragObj.transform.position.y + movePos, dragObj.transform.position.z);

        dragObj.transform.position = Vector3.SmoothDamp(dragObj.transform.position, pos_move, ref vel, Time.deltaTime);


        Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;
        if (Physics.Raycast(ray, out hit2))
        {
            //Debug.Log("After Drag pos " + hit2.point.y);
            firstMousePos = hit.point;
        }
    }
    void OnMouseUp()
    {
        endTime = Time.time;
        timeDiff = endTime - startTime;
        afterEffect = true;
        afterEffectPos = dragObj.transform.position;
        afterEffectPos.y += (movePos * 2) / timeDiff;
        //Debug.Log(leaderstodrag.transform.position);
        //Debug.Log(afterEffectPos);
    }
    void Update()
    {
        if (afterEffect)
        {

            if (transform.name == "LeaderDrag")
            {
                minPos = 0.5f;
                maxPos = 121f;
            }
            if (transform.name == "ShopDrag")
            {
                minPos = 0.8f;
                maxPos = 27.3f;
            }

            if (afterEffectPos.y < minPos)
                afterEffectPos.y = minPos;
            if (afterEffectPos.y > maxPos)
                afterEffectPos.y = maxPos;
            dragObj.transform.position = Vector3.SmoothDamp(dragObj.transform.position, afterEffectPos, ref vel, Time.deltaTime * 20);

            if (Vector3.Distance(dragObj.transform.position, afterEffectPos) < 0.02f)
            {
                afterEffect = false;
                afterEffectPos = dragObj.transform.position;
            }
        }
    }
}