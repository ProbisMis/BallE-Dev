using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
public class LevelCreator : MonoBehaviour
{
    public List<GameObject> prefabList;
    public static int currLevelStarCount = 0;
    public bool editorMode = false;
    public static bool editMap = false;
    public bool normalMode = false;
    public bool LightsOutMode = false;
    public bool GyroMode = false;
    private string[] mapString;
    public int TryLevel;

    public static Color objColor;
    void Awake()
    {
        // float r = 1f / 255f * UnityEngine.Random.Range(0f, 255f);
        // float g = 1f / 255f * UnityEngine.Random.Range(0f, 255f);
        // float b = 1f / 255f * UnityEngine.Random.Range(0f, 255f);
        //objColor = new Color(r, g, b);
        //int temp = UnityEngine.Random.Range(1, 6);
        NewColor();
    }
    // void Start()
    // {

    //     int checkpointLevel;
    //     if (PlayerPrefs.HasKey("Level"))
    //         checkpointLevel = int.Parse(PlayerPrefs.GetString("Level"));
    //     else
    //     {
    //         checkpointLevel = 1;
    //     }
    //     Player.level = checkpointLevel - checkpointLevel % 5;



    //     //Player.level = TryLevel;


    // }
    //[MenuItem("Tools/Write file")]
    void WriteString()
    {
        string path = "Assets/Resources/maps.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(str);
        writer.Close();

        //Re-import the file to update the reference in the editor
        //Resources.Load(path);
        TextAsset asset = Resources.Load("maps") as TextAsset;

        //Print the text from the file
        Debug.Log(asset.text);
    }

    public void CallReadString()
    {
        if (Player.level == 0)
            Player.level = 1;
        if (!editorMode)
            ReadString();
        else
            editMap = true;
    }

    //[MenuItem("Tools/Read file")]
    void ReadString()
    {
        TextAsset asset = Resources.Load("maps") as TextAsset;
        //StreamReader reader = new StreamReader(asset);
        //asset.text;
        string[] stringSeparators = new string[] { "***Level***\n" };
        mapString = asset.text.Split(stringSeparators, StringSplitOptions.None);
        //reader.Close();
        CreateMap();
    }


    public void CreateMap()
    {
        NewColor();
        DeletePrevious();
        currLevelStarCount = 0;

        string temp = mapString[Player.level - 1];
        string[] currentMap = temp.Split('\n');
        for (int i = 1; i < currentMap.Length - 1; i++)
        {
            string[] currentObject = currentMap[i].Split(';');
            //Pick object type
            //Debug.Log(i);
            //Debug.Log(currentObject[0]);
            int type = int.Parse(currentObject[0]);
            GameObject obj;
            Vector3 n;
            if (type != 5)
                n = StringToVector3(currentObject[1]);
            else
                n = Vector3.zero;

            obj = prefabList[type];
            obj = Instantiate(obj, n, EulerToQuaternion(currentObject[3]));
            obj.transform.SetParent(transform);
            //}
            if (type == 5)
            {
                obj.transform.position = Vector3.zero;
                Vector3 p1 = StringToVector3(currentObject[1].Split('&')[0]);
                Vector3 p2 = StringToVector3(currentObject[1].Split('&')[1]);
                obj.transform.GetChild(0).transform.position = p1;
                obj.transform.GetChild(1).transform.position = p2;
            }
            else
            {
                obj.transform.position = n;
                obj.transform.localScale = StringToVector3(currentObject[2]);
            }
            if (type < 4)
            {
                obj.GetComponent<ObstacleSpecialty>().isMoving = StringToBool(currentObject[4]);
                obj.GetComponent<ObstacleSpecialty>().direction = StringToVector3(currentObject[5]);
                obj.GetComponent<ObstacleSpecialty>().distance = float.Parse(currentObject[6]);
            }
            if (type == 4)
                currLevelStarCount++;
        }

    }
    void DeletePrevious()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    void NewColor()
    {
        int temp;
        temp = Player.level % 5;
        if (temp == 1)
            objColor = new Color(0.232f, 0.345f, 0.673f);
        else if (temp == 2)
            objColor = new Color(0.928f, 0.755f, 0.234f);
        else if (temp == 3)
            objColor = new Color(0.902f, 0.423f, 0.381f);
        else if (temp == 4)
            objColor = new Color(0.222f, 0.849f, 0.808f);
        else if (temp == 5)
            objColor = new Color(0.105f, 0.586f, 0.953f);
        else if (temp == 6)
            objColor = new Color(0.096f, 0.257f, 0.785f);
        Debug.Log(objColor);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M) && editorMode)
        {
            SaveMap();
        }
    }
    string str;
    void SaveMap()
    {
        int type;
        str = "";
        for (int i = 0; i < transform.childCount; i++)
        {
            type = GetObjectType(transform.GetChild(i).gameObject);
            str += type + ";";
            if (type == 5)
                str += transform.GetChild(i).GetChild(0).transform.position + "&" + transform.GetChild(i).GetChild(1).transform.position + ";";
            else
                str += "(" + transform.GetChild(i).transform.position.x + "," + transform.GetChild(i).transform.position.y + "," + transform.GetChild(i).transform.position.z + ")" + ";";
            //Debug.Log("(" + transform.GetChild(i).transform.localScale.x + "," + transform.GetChild(i).transform.localScale.y + "," + transform.GetChild(i).transform.localScale.z + ")" + ";");
            str += "(" + transform.GetChild(i).transform.localScale.x + "," + transform.GetChild(i).transform.localScale.y + "," + transform.GetChild(i).transform.localScale.z + ")" + ";";
            str += "(" + transform.GetChild(i).transform.localRotation.eulerAngles.x + "," + transform.GetChild(i).transform.localRotation.eulerAngles.y + "," + transform.GetChild(i).transform.localRotation.eulerAngles.z + ")" + ";\n";
            if (type < 4)
            {
                str = str.TrimEnd('\n');
                str += transform.GetChild(i).GetComponent<ObstacleSpecialty>().isMoving + ";";
                str += transform.GetChild(i).GetComponent<ObstacleSpecialty>().direction + ";";
                str += transform.GetChild(i).GetComponent<ObstacleSpecialty>().distance + ";\n";
            }
            //str += transform.GetChild(i).GetComponent<ObstacleSpecialty>().isMoving + ";\n";
        }
        str += "***Level***";
        WriteString();

    }
    Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
    bool StringToBool(string sBool)
    {
        bool result = false;
        if (sBool == "True")
            result = true;

        return result;

    }
    Quaternion StringToQuaternion(string sQuat)
    {
        // Remove the parentheses
        if (sQuat.StartsWith("(") && sQuat.EndsWith(")"))
        {
            sQuat = sQuat.Substring(1, sQuat.Length - 2);
        }

        // split the items
        string[] sArray = sQuat.Split(',');

        // store as a Vector3
        Quaternion result = new Quaternion(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]),
            float.Parse(sArray[2]));

        return result;
    }

    Quaternion EulerToQuaternion(string sQuat)
    {
        // Remove the parentheses
        if (sQuat.StartsWith("(") && sQuat.EndsWith(")"))
        {
            sQuat = sQuat.Substring(1, sQuat.Length - 2);
        }

        // split the items
        string[] sArray = sQuat.Split(',');
        return Quaternion.Euler(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]));
    }
    int GetObjectType(GameObject obj)
    {
        if (obj.transform.name.Contains("Target"))
            return 0;
        else if (obj.transform.name == "Box")
            return 1;
        else if (obj.transform.name.Contains("LineBox"))
            return 2;
        else if (obj.transform.name.Contains("BigBox"))
            return 3;
        else if (obj.transform.name.Contains("Star"))
            return 4;
        else if (obj.transform.name.Contains("Portal"))
            return 5;
        else if (obj.transform.name.Contains("Freeze"))
            return 6;
        else if (obj.transform.name.Contains("Fire"))
            return 7;
        else if (obj.transform.name.Contains("Laser"))
            return 7;
        else if (obj.transform.name.Contains("FirePortal"))
            return 7;
        return -1;

    }


}