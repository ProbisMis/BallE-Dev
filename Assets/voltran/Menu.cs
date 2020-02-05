using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Models;
using UnityEngine.UI;
using System;
using UnityEditor;

public class Menu : MonoBehaviour
{
    public Camera main;
    public GameObject canvas;
    Vector3 newPos;
    public static bool closeMenu;


    public static bool userLoggedin;
    public bool isPressed = false;



    [Header("Scripts")]
    public Player PlayerScript;

    public GameObject levelcreator;
    public GameObject profile;

    [Header("UI Input Fields")]
    public InputField Username;
    public InputField Password;
    public InputField c_Password;

    [Header("UI Texts")]


    public GameObject ProfileUser;
    public GameObject ProfilePoints;
    public GameObject ProfileHighestNormal;
    public GameObject GameUIPoints;
    public GameObject GameUIStars;




    private RequestHelper currentRequest;
    private Leaderboard leaderboard;

    /*

        1. Register
            * Tutorial
        2. Login
            * Game Scene
        3. 2nd Login
            * Has Token, Has Device Registered => Auto-Login => GameScene
        4. Logged Out
            * Token should me cleared both in Server and PlayerPrefs => Login/Register Scene
    
    */
    public TextMesh errorLog;
    private void LogMessage(string title, string message)
    {
        isPressed = false; //For not pressing multiple times before response
        errorLog.text = message;
        Debug.Log(message);
    }

    void Start()
    {
        newPos = new Vector3(0, main.transform.position.y, main.transform.position.z);
        Debug.Log("1");
        if (PlayerPrefs.HasKey("Username"))
        {
            Username.text = PlayerPrefs.GetString("Username");
            Password.text = PlayerPrefs.GetString("Password");
        }
        //GameScene();
        // if (!levelcreator.GetComponent<LevelCreator>().editorMode)
        // {
        //     isPressed = true;
        //     errorLog.text = "Processing Auto-Login..";
        //     AutoLogin();
        // }

        // else
        //     GameScene();
    }
    /// <summary>
    /// First Check Login, Then Register
    /// </summary>
    public void OnMouseDown()
    {
        if (!isPressed)
        {
            isPressed = true;

            if (c_Password.text.Equals("") || c_Password.text.Equals(null))
            {
                errorLog.text = "Processing Login..";
                Login();
            }


            isPressed = false;
        }


    }
    public void GameScene()
    {
        //Check User History
        if (!CameraPosition.camMoving)
        {
            main.GetComponent<CameraPosition>().ChangeCamPos(0);
            canvas.SetActive(false);
        }
    }

    void Update()
    {
        if (closeMenu)
        {
            main.transform.position = Vector3.Lerp(main.transform.position, newPos, Time.deltaTime * 4.5f);
            if (Vector3.Distance(main.transform.position, newPos) <= 0.05f)
            {
                main.transform.position = newPos;
                closeMenu = false;
            }
        }
    }

    public void AutoLogin()
    {
        try
        {
            if (PlayerPrefs.HasKey("Token"))
            {
                //LogMessage("Error", "AL0");
                DBManager.CheckForInternetConnection(res =>
                {
                    //LogMessage("Error", "AL1");
                    if (res)
                    {
                        if (PlayerPrefs.HasKey("SyncDataAwaiting"))
                        {
                            /*
                                1. Update User With Sync Data 
                                    * id
                                    * points
                                    * star_collected
                                    * normal_highest
                                    * endless_highest
                                    * checkpoint_level
                            */
                            WWWForm www = DBManager.CreateSyncWWWForm();
                            DBManager.Instance.UpdateUser(www, PlayerPrefs.GetString("SyncData_UserID"));
                        }
                        else
                        {
                            //Debug.Log("SyncData does not exist");
                        }
                        //Debug.Log("Yes Proceed");
                    }
                    else
                    {
                        Debug.Log("No Internet");
                        errorLog.text = "No Internet";
                        // if (PlayerPrefs.HasKey("UserData_Username"))
                        // {
                        //     DBManager.fetchedUser = DBManager.FetchUserPrefs();

                        //     //UI Game Scene
                        //     //GameUser.GetComponent<TextMesh>().text = DBManager.fetchedUser.username;
                        //     ProfileUser.GetComponent<TextMesh>().text = DBManager.fetchedUser.username;
                        //     ProfilePoints.GetComponent<TextMesh>().text = DBManager.fetchedUser.points.ToString();
                        //     ProfileHighestNormal.GetComponent<TextMesh>().text = DBManager.fetchedUser.normal_highest.ToString();


                        //     LogMessage("Success", "Logged in as" + DBManager.fetchedUser.username);

                        //     //Redirect to Game Scene
                        //     GameScene();
                        //     return;

                        // }
                        // else
                        {
                            return;
                        }
                    }
                });

                //Retrieve user data
                currentRequest = new RequestHelper
                {
                    Uri = DBManager.basePath + "/user",
                    FormData = new WWWForm(),
                    Retries = 5,
                    RetrySecondsDelay = 1,
                    Timeout = 1,
                };

                //Set Authorization Headers
                RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + PlayerPrefs.GetString("Token");
                RestClient.DefaultRequestHeaders["Accept"] = "application/json";


                //Initiate Call
                RestClient.Post(currentRequest)
                    .Then(res =>
                    {
                        FetchUser(PlayerPrefs.GetString("Token"));
                    })
                    .Catch(err =>
                    {
                        string response = DBManager.Instance.GetExceptionResponse(err);
                        if (response != null)
                        {
                            LogMessage("Request Exception", response);

                        }
                        else
                        {
                            LogMessage("Error", err.Message);
                        }
                    });  //Central Logging System
            }
            else
            {
                LogMessage("Error", "Token does not exist");
            }
        }
        catch (System.Exception err)
        {
            LogMessage("Error", err.Message + err.StackTrace);
        }

    }

    public void FetchUser(string token)
    {
        //Retrieve user data
        currentRequest = new RequestHelper
        {
            Uri = DBManager.basePath + "/user",
            FormData = new WWWForm(),
            Retries = 5,
            RetrySecondsDelay = 1,
        };
        //Set Authorization Headers
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + token;
        RestClient.DefaultRequestHeaders["Accept"] = "application/json";
        //Initiate Call
        RestClient.Post(currentRequest)
            .Then(res =>
            {

                DBManager.fetchedUser = JsonUtility.FromJson<User>(res.Text); //Set JSON fields to object
                DBManager.fetchedUser.token = token; //Set the token   
                Debug.Log(DBManager.fetchedUser.is_ad_bought);
                PlayerPrefs.SetString("Token", token);

                //Create Prefs User Data
                DBManager.CreateUserPrefs(DBManager.fetchedUser); //no longer needed

                //UI Game Scene
                GameUIPoints.GetComponent<TextMesh>().text = DBManager.fetchedUser.points.ToString();
                GameUIStars.GetComponent<TextMesh>().text = DBManager.fetchedUser.star_collected.ToString();

                //UI Profile Page
                ProfileUser.GetComponent<TextMesh>().text = DBManager.fetchedUser.username;
                ProfilePoints.GetComponent<TextMesh>().text = DBManager.fetchedUser.points.ToString();
                ProfileHighestNormal.GetComponent<TextMesh>().text = DBManager.fetchedUser.normal_highest.ToString();

                //                Player.checkpoint_level = DBManager.fetchedUser.checkpoint_level;
                Player.level = DBManager.fetchedUser.normal_highest - (DBManager.fetchedUser.normal_highest % 5);
                LogMessage("Success", "Logged in as " + DBManager.fetchedUser.username);
                PlayerPrefs.SetString("Username", DBManager.fetchedUser.username);
                PlayerPrefs.SetString("Password", DBManager.fetchedUser.password);
                // levelcreator.GetComponent<LevelCreator>().CreateMap();
                userLoggedin = true;
                levelcreator.GetComponent<LevelCreator>().CallReadString();
                GameScene();//Redirect to Game Scene

            })
            .Catch(err =>
            {
                string response = DBManager.Instance.GetExceptionResponse(err);
                if (response != null)
                {
                    LogMessage("Request Exception", response);
                }
                else
                {
                    LogMessage("Error", err.Message);
                }

            });  //Central Logging System

    }
    public void Login()
    {
        //Register Fields to be send
        WWWForm www = new WWWForm();
        www.AddField("username", Username.text);
        www.AddField("password", Password.text);

        //Set Path etc.
        currentRequest = new RequestHelper
        {
            Uri = DBManager.basePath + "/login",
            FormData = www,
            Retries = 5,
            RetrySecondsDelay = 1
        };

        //Initiate Call
        RestClient.Post(currentRequest)
            .Then(res =>
            {
                errorLog.text = res.Text;
                string token = JsonUtility.FromJson<SuccessToken>(res.Text).success.token;
                FetchUser(token);
            })
            .Catch(err =>
            {

                string response = DBManager.Instance.GetExceptionResponse(err);
                if (response != null)
                {
                    //LogMessage("Request Exception", response);
                    errorLog.text = "Processing Register..";

                    Register();

                }
                else
                {
                    LogMessage("Error", err.Message);
                }
            }); //Central Logging System
                //        Debug.Log("Returning false");

    }


    public void Register()
    {

        //Register Fields to be send
        WWWForm www = new WWWForm();
        www.AddField("username", Username.text);
        www.AddField("password", Password.text);
        www.AddField("c_password", c_Password.text);
        www.AddField("device", SystemInfo.deviceUniqueIdentifier);

        //Set Path etc.
        currentRequest = new RequestHelper
        {
            Uri = DBManager.basePath + "/register",
            FormData = www,
            Retries = 5,
            RetrySecondsDelay = 1
        };

        RestClient.Post(currentRequest)
             .Then(res =>
             {

                 errorLog.text = res.Text;
                 string token = JsonUtility.FromJson<SuccessToken>(res.Text).success.token;

                 FetchUser(token);

             })
            .Catch(err =>
            {
                string response = DBManager.Instance.GetExceptionResponse(err);
                if (response != null)
                {
                    LogMessage("Request Exception", response);
                }
                else
                {
                    LogMessage("Error", err.Message);
                }
            });  //Central Logging System

    }
}




// public void MyRank()
// {
//     //Retrieve user data
//     currentRequest = new RequestHelper
//     {
//         Uri = DBManager.basePath + "/user/rank",
//         FormData = new WWWForm(),
//     };

//     //Set Authorization Headers
//     RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + PlayerPrefs.GetString("Token");
//     RestClient.DefaultRequestHeaders["Accept"] = "application/json";


//     //Initiate Call
//     RestClient.Post(currentRequest)
//         .Then(res =>
//         {
//             Debug.Log(res.Text);
//             leaderboard = JsonUtility.FromJson<Leaderboard>(res.Text); //Set JSON fields to object
//             Debug.Log(leaderboard.success[0].id);
//             Debug.Log(leaderboard.myRank);
//         }) //!Redirect to Game Scene
//         .Catch(err =>
//         {
//             Debug.Log(err.Message);
//             Debug.Log(DBManager.Instance);
//             DBManager.Instance.LogMessage("Error", err.Message + err.StackTrace);
//         });  //Central Logging System
// }








// private void loadScene(int scene)
// {
//     //        Debug.Log("Load Scene Called");
//     switch (scene)
//     {
//         case 1:
//             //Tutorial
//             Debug.Log("Loading Tutorial Scene");
//             break;
//         case 2:
//             //Game Scene
//             Debug.Log("Loading Game Scene");
//             GameScene();
//             break;
//         default:
//             Debug.Log("Loading Defualt  Scene");
//             //Game Scene or Login/Register Scene
//             break;
//     }

// }