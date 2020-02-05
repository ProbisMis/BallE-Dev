using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //CreateNewLevel();
    }
    public void CreateNewLevel()
    {
        for (int i = 0; i <= transform.childCount - 4; i++)
        {

            if (transform.GetChild(i).transform.name == "Star")
                transform.GetChild(i).transform.position = new Vector3(Random.Range(12.5f, 2.5f), transform.position.y, transform.position.z);
            if (transform.GetChild(i).transform.name == "Box")
                transform.GetChild(i).gameObject.SetActive(false);

            if (i <= Player.level - 1)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                //transform.GetChild(i).GetComponent<ObstacleSpecialty>().Set(i);
            }
            // if (Player.level != 0)
            // {
            //     if (!transform.GetChild(i).gameObject.activeInHierarchy)
            //     {
            //         transform.GetChild(i).gameObject.SetActive(true);
            //         transform.GetChild(i).GetComponent<ObstacleSpecialty>().Set(i);
            //         i = transform.childCount;
            //     }
            //     else
            //         transform.GetChild(i).GetComponent<ObstacleSpecialty>().Set(i);
            // }
        }
    }
}
