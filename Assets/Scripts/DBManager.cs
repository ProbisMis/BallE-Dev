using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using Proyecto26;
using UnityEditor;
using UnityEngine.Networking;
using System;
using System.Net;

public class DBManager : MonoBehaviour
{


    private static DBManager _db;
    public static DBManager Instance { get { return _db; } }
    private void Awake()
    {
        if (_db != null && _db != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _db = this;
        }
    }
    [Header("Player Script")]
    public Player PlayerScript;

    public Camera main;
    public GameObject canvas;
    public static string basePath = "http://ec2-3-125-201-138.eu-central-1.compute.amazonaws.com/api/v1";
    public static RequestHelper currentRequest;
    public static User fetchedUser;
    public static Leaderboard leaderboard;

    public void Start()
    {
        // PlayerPrefs.SetString("Token", "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiMDYzOGI0NzQ4ZGU5MzY4ZjdhZmQwNGIwZTc1MDViNmZkYTYzMjdhNTM2MWNlZDQwMzUwZjdmYjYyMzM1ZGYxY2E2OWY5NGFiZTEzMjQ2Y2YiLCJpYXQiOjE1NzU1NTA0MzEsIm5iZiI6MTU3NTU1MDQzMSwiZXhwIjoxNjA3MTcyODMxLCJzdWIiOiIxNCIsInNjb3BlcyI6W119.YPAOQ3FXE-f0WsJBwDcE5gN9yn8Si2EtjsusR4n4i49n6T-mG3JrU8DAUreeBSC0fp47ORgZMv0QpoKLkqLbl9UcT2H5Mo4QUqVj3H6YLWglspq_0lLkgp7XO5jpCmRLiHBRuA4ix7cvWNhen3Lvz8yFJWdeLczc20ZTOeCqW88ubxvH50lqmLdwtR1x6jyneuBWICRtjO8aOo2CpmZvVOXOZNA97F8kVYGyAf6Gawoq4Ks6r962YxyaH0EF-piF-QVA3iAbSIWcvc71wekzP9W93Lj0snfQjfqxMqmjDfIsoe2qge_42PviNm5bmktm1j_Skt__NhOPW8En3jchJcSbcPzLPWK07BoNGl6NFh5gUcHNo1evB6qypMkbJ_aMMSmVUlOcF1_78F_85Vo3sitSVZZbH4Y6_NwQkEWfSjihtnuOepmCAhfupccM2PW1GvAMQlY8gDfON9NWMAwhUQzmtvipEup0u4F7z2clWsjXDHkgv2aotbEpX4gH4Jsr3_t8_BmseGQE_QrR_uRZYrnGR-e0Xj48k3eGaXiZXVcsU2N3sqgt3kmO6oFOr4ljRu5gwQaXrMrA-2v8Iv0BWXbgHQkDSBsnTzX3jzwUvIX4O6XlTkxAb97UPK9W2qb0z-jAWJJKDOTCxOe3FlZiojk6rR1bQ5Dnv7cvAGWKpvw");
        // try
        // {
        //     if (PlayerPrefs.HasKey("Token"))
        //     {
        //         DBManager.CheckForInternetConnection(res =>
        //         {
        //             if (res)
        //             {
        //                 if (PlayerPrefs.HasKey("SyncDataAwaiting"))
        //                 {
        //                     /*
        //                         1. Update User With Sync Data 
        //                             * id
        //                             * points
        //                             * star_collected
        //                             * normal_highest
        //                             * endless_highest
        //                             * checkpoint_level
        //                     */
        //                     WWWForm www = DBManager.CreateSyncWWWForm();
        //                     DBManager.Instance.UpdateUser(www, PlayerPrefs.GetString("SyncData_UserID"));
        //                 }
        //                 else
        //                 {
        //                     Debug.Log("SyncData does not exist");
        //                 }
        //                 Debug.Log("Yes Proceed");
        //             }
        //             else
        //             {
        //                 Debug.Log("No Internet");

        //                 if (PlayerPrefs.HasKey("UserData_Username"))
        //                 {
        //                     DBManager.fetchedUser = DBManager.FetchUserPrefs();

        //                     //UI Game Scene
        //                     // GameUser.GetComponent<TextMesh>().text = DBManager.fetchedUser.username;
        //                     // ProfileUser.GetComponent<TextMesh>().text = DBManager.fetchedUser.username;
        //                     PlayerScript.usernameText.text = fetchedUser.username;
        //                     LogMessage("Success", "Logged in as" + DBManager.fetchedUser.username);

        //                     //Redirect to Game Scene

        //                     return;

        //                 }
        //                 else
        //                 {
        //                     return;
        //                 }
        //             }
        //         });

        //         //Retrieve user data
        //         currentRequest = new RequestHelper
        //         {
        //             Uri = DBManager.basePath + "/user",
        //             FormData = new WWWForm(),
        //         };

        //         //Set Authorization Headers
        //         RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + PlayerPrefs.GetString("Token");
        //         RestClient.DefaultRequestHeaders["Accept"] = "application/json";


        //         //Initiate Call
        //         RestClient.Post(currentRequest)
        //             .Then(res =>
        //             {
        //                 FetchUser(PlayerPrefs.GetString("Token"));
        //             })
        //             .Catch(err =>
        //             {
        //                 Debug.Log(err.Message);
        //                 RequestException req = err.GetBaseException() as RequestException;
        //                 Debug.Log(req.Response);
        //                 DBManager.Instance.LogMessage("Error", err.Message + err.StackTrace);
        //             });  //Central Logging System
        //     }
        //     else
        //     {
        //         Debug.Log("Token does not exist in PlayerPrefs");
        //     }
        // }
        // catch (System.Exception err)
        // {
        //     DBManager.Instance.LogMessage("Error", err.Message + err.StackTrace);
        // }

    }

    // public void FetchUser(string token)
    // {
    //     //Retrieve user data
    //     currentRequest = new RequestHelper
    //     {
    //         Uri = DBManager.basePath + "/user",
    //         FormData = new WWWForm(),
    //     };

    //     //Set Authorization Headers
    //     RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + token;
    //     RestClient.DefaultRequestHeaders["Accept"] = "application/json";

    //     //Initiate Call
    //     RestClient.Post(currentRequest)
    //         .Then(res =>
    //         {

    //             //UserManager.createNewUser();

    //             DBManager.fetchedUser = JsonUtility.FromJson<User>(res.Text); //Set JSON fields to object
    //             DBManager.fetchedUser.token = token; //Set the token   
    //             PlayerScript.usernameText.text = fetchedUser.username;
    //             PlayerPrefs.SetString("Token", token);

    //             //Create Prefs User Data
    //             DBManager.CreateUserPrefs(DBManager.fetchedUser);

    //             //UI Game Scene

    //             LogMessage("Success", "Logged in as" + DBManager.fetchedUser.username);

    //             //Redirect to Game Scene


    //         })
    //         .Catch(err =>
    //         {
    //             RequestException req = err.GetBaseException() as RequestException;
    //             Debug.Log(req.Response);
    //             DBManager.Instance.LogMessage("Error", err.Message);
    //         });  //Central Logging System

    // }

    public static void CheckForInternetConnection(Action<bool> callback)
    {
        currentRequest = new RequestHelper
        {
            Uri = "http://google.com/generate_204",
            Retries = 3,
            RetrySecondsDelay = 1,
            Timeout = 2
        };
        RestClient.Get(currentRequest).Then(res =>
        {
            //Debug.Log(res.Text);
            callback(true);
        }).Catch(err =>
        {
            Debug.Log(err.Message);
            callback(false);
        });
        // try
        // {
        //     using (var client = new WebClient())
        //     using (client.OpenRead("http://google.com/generate_204"))
        //         return true;
        // }
        // catch
        // {
        //     return false;
        // }
    }

    public static void ClearSyncData()
    {
        if (PlayerPrefs.HasKey("SyncDataAwaiting"))
        {
            PlayerPrefs.DeleteKey("SyncData_Star_Collected");
            PlayerPrefs.DeleteKey("SyncData_Normal_Highest");
            PlayerPrefs.DeleteKey("SyncData_Checkpoint_Level");
            PlayerPrefs.DeleteKey("SyncData_Points");
            PlayerPrefs.DeleteKey("SyncDataAwaiting");
        }
    }

    public static WWWForm CreateSyncWWWForm()
    {
        WWWForm www = new WWWForm();
        www.AddField("star_collected", PlayerPrefs.GetInt("SyncData_Star_Collected"));
        www.AddField("normal_highest", PlayerPrefs.GetInt("SyncData_Normal_Highest"));
        www.AddField("checkpoint_level", PlayerPrefs.GetInt("SyncData_Checkpoint_Level"));
        www.AddField("points", PlayerPrefs.GetInt("SyncData_Points"));
        Debug.Log(PlayerPrefs.GetInt("SyncData_Star_Collected"));
        Debug.Log(PlayerPrefs.GetInt("SyncData_Normal_Highest"));
        Debug.Log(PlayerPrefs.GetInt("SyncData_Checkpoint_Level"));
        Debug.Log(PlayerPrefs.GetInt("SyncData_Points"));
        return www;
    }
    public static void CreateSync(int points, int stars, int nhigh, int checkpoint)
    {
        //Debug.Log("Sync Points : " + points);
        PlayerPrefs.SetString("SyncDataAwaiting", "1");
        PlayerPrefs.SetString("SyncData_UserID", DBManager.fetchedUser.id.ToString());
        PlayerPrefs.SetInt("SyncData_Points", points);
        PlayerPrefs.SetInt("SyncData_Star_Collected", stars);
        PlayerPrefs.SetInt("SyncData_Normal_Highest", nhigh);
        PlayerPrefs.SetInt("SyncData_Checkpoint_Level", checkpoint);
    }

    public static User FetchUserPrefs()
    {
        User user = new User();
        user.id = int.Parse(PlayerPrefs.GetString("SyncData_UserID"));
        user.username = PlayerPrefs.GetString("UserData_Username");
        user.points = PlayerPrefs.GetInt("UserData_Points");
        user.star_collected = PlayerPrefs.GetInt("UserData_Star_Collected");
        user.normal_highest = PlayerPrefs.GetInt("UserData_Normal_Highest");

        user.checkpoint_level = PlayerPrefs.GetInt("UserData_Checkpoint_Level");

        return user;
    }

    public static void CreateUserPrefs(User user)
    {
        PlayerPrefs.SetString("SyncData_UserID", user.id.ToString());
        PlayerPrefs.SetString("UserData_Username", user.username);
        PlayerPrefs.SetInt("UserData_Points", user.points);
        PlayerPrefs.SetInt("UserData_Star_Collected", user.star_collected);
        PlayerPrefs.SetInt("UserData_Normal_Highest", user.normal_highest);
        PlayerPrefs.SetInt("UserData_Checkpoint_Level", user.checkpoint_level);
    }
    public TextMesh errorLog;
    public string GetExceptionResponse(Exception err)
    {

        if (err.GetBaseException() is RequestException)
        {
            RequestException rq = err.GetBaseException() as RequestException;

            if (rq.Response == null)
            {
                LogMessage("Error", "Response null, " + err.Message);
                return null;
            }
            ErrorModel error = JsonUtility.FromJson<ErrorHandler>(rq.Response).error;
            string response = ErrorResponse(error);

            if (response != null)
            {
                return response;
            }
            else
            {
                LogMessage("Error", "Not Message Exception");
                return null;
            }
        }
        else
        {
            LogMessage("Error", "This is NOT RequestException -> " + err.GetBaseException());
            return null;
        }
    }

    public void Leaderboard(Action callback)
    {
        //Retrieve user data
        currentRequest = new RequestHelper
        {
            Uri = DBManager.basePath + "/users/leader",
            FormData = new WWWForm(),
            Retries = 7,
        };

        //Set Authorization Headers
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + PlayerPrefs.GetString("Token");
        RestClient.DefaultRequestHeaders["Accept"] = "application/json";


        //Initiate Call
        RestClient.Post(currentRequest)
            .Then(res =>
            {
                //Debug.Log(res.Text);
                leaderboard = JsonUtility.FromJson<Leaderboard>(res.Text); //Set JSON fields to object
                callback();
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

    public void UpdateUser(WWWForm www, Action callback)
    {
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + PlayerPrefs.GetString("Token");
        RestClient.DefaultRequestHeaders["Accept"] = "application/json";

        //Set Path etc.
        currentRequest = new RequestHelper
        {
            Uri = DBManager.basePath + "/user/edit",
            FormData = www,
            Retries = 7,
            RetrySecondsDelay = 1
        };

        RestClient.Post<User>(currentRequest)
         .Then(res =>
         {
             Debug.Log(res.ToString());
             fetchedUser = res;
             callback();
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


         });
    }

    public void UpdateUser(WWWForm www, string ID)
    {

        //Set Path etc.
        currentRequest = new RequestHelper
        {
            Uri = DBManager.basePath + "/user/edit/" + ID,
            FormData = www,
            Retries = 7,
        };

        RestClient.Post<User>(currentRequest)
         .Then(res =>
         {
             Debug.Log(res.ToString());
             fetchedUser = res;

             DBManager.ClearSyncData();

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


         });
    }

    public void DeleteUser()
    {
        if (PlayerPrefs.HasKey("Token"))
        {
            //Retrieve user data
            currentRequest = new RequestHelper
            {
                Uri = DBManager.basePath + "/user/delete",
                FormData = new WWWForm(),
                Retries = 7,
            };

            //Set Authorization Headers
            RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + PlayerPrefs.GetString("Token");
            RestClient.DefaultRequestHeaders["Accept"] = "application/json";


            //Initiate Call
            RestClient.Post(currentRequest)
                .Then(res =>
                {
                    Debug.Log(res.Text);

                    //Clear Game Data

                    PlayerScript.ResetGame(true);

                    //Change Camera Position
                    main.GetComponent<CameraPosition>().ChangeCamPos(10.33f);
                    canvas.SetActive(true);

                })
                .Catch(err =>
                {
                    Debug.Log(err.Message);
                    Debug.Log(DBManager.Instance);
                    DBManager.Instance.LogMessage("Error", err.Message + err.StackTrace);
                });  //Central Logging System
        }
        else
        {
            Debug.Log("Token does not exist in PlayerPrefs");
        }
    }

    public void LogoutUser()
    {
        if (PlayerPrefs.HasKey("Token"))
        {
            //Retrieve user data
            currentRequest = new RequestHelper
            {
                Uri = DBManager.basePath + "/user/logout",
                FormData = new WWWForm(),
                Retries = 7,
            };

            //Set Authorization Headers
            RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + PlayerPrefs.GetString("Token");
            RestClient.DefaultRequestHeaders["Accept"] = "application/json";


            //Initiate Call
            RestClient.Post(currentRequest)
                .Then(res =>
                {
                    Debug.Log(res.Text);
                    //Sync-Data To Server, Clear Local , Set Prefs is update failed for further sync

                    PlayerScript.ResetGame(true);


                    main.GetComponent<CameraPosition>().ChangeCamPos(10.33f);
                    canvas.SetActive(true);

                })
                .Catch(err =>
                {
                    LogMessage("Error", err.Message);
                });  //Central Logging System
        }
        else
        {
            Debug.Log("Token does not exist in PlayerPrefs");
        }
    }

    public string ErrorResponse(ErrorModel model)
    {
        //Which Error To Display?
        if (model.username == null)
        {
            //            Debug.Log("Username is null");
        }
        else
        {
            //            Debug.Log(string.Format("username  with lenght {0}", model.username.Length));
            return model.username[0];
        }

        if (model.device == null)
        {
            //   Debug.Log(string.Format("Device is null with lenght {0}", 0));
        }
        else
        {
            //  Debug.Log(string.Format("Passowrd is null with lenght {0}", 0));
            return model.device[0];
        }

        if (model.password == null)
        {
            // Debug.Log(string.Format("Passowrd is null with lenght {0}", 0));

        }
        else
        {
            //  Debug.Log(string.Format("Passowrd with lenght {0}", model.password.Length));
            return model.password[0];
        }


        if (model.c_password == null)
        {
            //   Debug.Log(string.Format("Passowrd is null with lenght {0}", 0));

        }
        else
        {
            // Debug.Log(string.Format("c_password with lenght {0}", model.c_password.Length));
            return model.c_password[0];
        }

        //False Alarm
        return null;
    }

    public void LogMessage(string title, string message)
    {
        // #if UNITY_EDITOR
        //         EditorUtility.DisplayDialog(title, message, "Ok");
        // #else
        errorLog.text = message;
        Debug.Log(message);
        // #endif
    }
    public void Get()
    {
        // We can add default request headers for all requests
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiODEwMmE0NmVmZDE4MTQ5YTk2MDlhNmQ2NGZmMzUzOTZhNDEwYzJmOGViNmNiYzA4MTg4ZTkzMTk1YzIzNzE2ZTFlZTg3NTA3MGNhZTgyOTIiLCJpYXQiOjE1NzQzNDIzOTQsIm5iZiI6MTU3NDM0MjM5NCwiZXhwIjoxNjA1OTY0Nzk0LCJzdWIiOiIxIiwic2NvcGVzIjpbXX0.e0P5Qwa0IbfD7d78cuP8afOK86V-OL5ANacgvvNEUiVVOl4ylwOkddtlJtlAsJ8TPv1i8FN0rn_JrjOzhY_AKYKSsakG8junSKfJoWvUSHVVKl3CkbAi-t1kA1igmltYrhbjUmLmI65en6HvfHIO7XTVlArGQeQHJEQsUle-Wih6qbH6VaPDD7CvbeA9pwn_pbwfJu_KkllH3x6lkpY_SH5-UFFVIiWgjGXT5r_EZ7q5Tn_XYI0hmOSfdL6FAHl37XFu_4BYRJEP-6sx8yFEoRZyEKEfyk1e2i5Z-zs6a6oGQlGMQxi9znlxLu0EbjrzsWFDtyDHg9yVRaj_b8SvozN7T9ZITPcGpdfC3ckeNlxk3gy-GFTzn2OQKlSEMk_SarIbfu5zbblxd97xRkyHX8vcc4krGKSRhMud1RnwB7_xdTkE3-qjPVMvV_X2M9iFN6F3AGokT7GqpL3NwZxtqFtHjr5fEXl-56JLJzc0nP5kjI1rN6U98mspjbafqoIgli9uTMjRvjS9U9gSmjADzLbZSrMmQqgDxPiCcVF7YrfFLkdTWPaIA3ZvgwOCGk5plhHIfhBCI5qmBOJ8kQ4ZxyIaD1gH4ZOA7c1X4VsBLzOeqnhXOZsr5pr_R2OPZQERqoecS9gAIJohVBLFpzWX7xNWdlxFvBGmT0vRzzQcK6s";
        RestClient.DefaultRequestHeaders["Accept"] = "application/json";
        RestClient.DefaultRequestHeaders["Content-Type"] = "application/json";

        // RequestHelper requestOptions = null;

        RestClient.Get(basePath + "/users").Then(res =>
       {
           Debug.Log(res.Text);
       }).Catch(err => Debug.Log("Error :" + err.Message));

        // RestClient.GetArray<Post>(basePath + "/posts").Then(res =>
        // {

        //     this.LogMessage("Posts", JsonHelper.ArrayToJsonString<Post>(res, true));
        //     return RestClient.GetArray<Todo>(basePath + "/todos");
        // }).Then(res =>
        // {
        //     this.LogMessage("Todos", JsonHelper.ArrayToJsonString<Todo>(res, true));
        //     return RestClient.GetArray<User>(basePath + "/users");
        // }).Then(res =>
        // {
        //     this.LogMessage("Users", JsonHelper.ArrayToJsonString<User>(res, true));

        //     // We can add specific options and override default headers for a request
        //     requestOptions = new RequestHelper
        //     {
        //         Uri = basePath + "/photos",
        //         Headers = new Dictionary<string, string> {
        //             { "Authorization", "Other token..." }
        //         },
        //         EnableDebug = true
        //     };
        //     return RestClient.GetArray<Photo>(requestOptions);
        // }).Then(res =>
        // {
        //     this.LogMessage("Header", requestOptions.GetHeader("Authorization"));

        //     // And later we can clean the default headers for all requests
        //     RestClient.CleanDefaultHeaders();

        // }).Catch(err => this.LogMessage("Error", err.Message));
    }

    public void Post()
    {
        /*
            1. API Path,
            2. From Data
                * With Model, Without Model
            3. Bearer Token
            4. Accept => application/json
            5. 
        */
        // WWWForm www = new WWWForm();
        // www.AddField("name", "burak13");
        // www.AddField("email", "bar1@bar.cok");
        // www.AddField("password", "123456");
        // www.AddField("c_password", "123456");

        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiODEwMmE0NmVmZDE4MTQ5YTk2MDlhNmQ2NGZmMzUzOTZhNDEwYzJmOGViNmNiYzA4MTg4ZTkzMTk1YzIzNzE2ZTFlZTg3NTA3MGNhZTgyOTIiLCJpYXQiOjE1NzQzNDIzOTQsIm5iZiI6MTU3NDM0MjM5NCwiZXhwIjoxNjA1OTY0Nzk0LCJzdWIiOiIxIiwic2NvcGVzIjpbXX0.e0P5Qwa0IbfD7d78cuP8afOK86V-OL5ANacgvvNEUiVVOl4ylwOkddtlJtlAsJ8TPv1i8FN0rn_JrjOzhY_AKYKSsakG8junSKfJoWvUSHVVKl3CkbAi-t1kA1igmltYrhbjUmLmI65en6HvfHIO7XTVlArGQeQHJEQsUle-Wih6qbH6VaPDD7CvbeA9pwn_pbwfJu_KkllH3x6lkpY_SH5-UFFVIiWgjGXT5r_EZ7q5Tn_XYI0hmOSfdL6FAHl37XFu_4BYRJEP-6sx8yFEoRZyEKEfyk1e2i5Z-zs6a6oGQlGMQxi9znlxLu0EbjrzsWFDtyDHg9yVRaj_b8SvozN7T9ZITPcGpdfC3ckeNlxk3gy-GFTzn2OQKlSEMk_SarIbfu5zbblxd97xRkyHX8vcc4krGKSRhMud1RnwB7_xdTkE3-qjPVMvV_X2M9iFN6F3AGokT7GqpL3NwZxtqFtHjr5fEXl-56JLJzc0nP5kjI1rN6U98mspjbafqoIgli9uTMjRvjS9U9gSmjADzLbZSrMmQqgDxPiCcVF7YrfFLkdTWPaIA3ZvgwOCGk5plhHIfhBCI5qmBOJ8kQ4ZxyIaD1gH4ZOA7c1X4VsBLzOeqnhXOZsr5pr_R2OPZQERqoecS9gAIJohVBLFpzWX7xNWdlxFvBGmT0vRzzQcK6s";
        RestClient.DefaultRequestHeaders["Accept"] = "application/json";
        RestClient.DefaultRequestHeaders["Content-Type"] = "application/json";
        // RestClient.DefaultRequestHeaders["Content-Type"] = "application/json";


        currentRequest = new RequestHelper
        {
            Uri = basePath + "/getUser"
            // Body = new User
            // {
            //     name = "burak31111",
            //     email = "bar111@2bar.cok",
            //     password = "123456",
            //     c_password = "123456"
            // }
        };
        // JsonUtility.ToJson(res, true)
        RestClient.Post(currentRequest)


        .Then(res => Debug.Log(res.Text))
        .Catch(err => this.LogMessage("Error", err.Message + err.InnerException));
    }
    IEnumerator PostData()
    {
        WWWForm www = new WWWForm();
        www.AddField("name", "burak3");
        www.AddField("email", "bar@bar.cok");
        www.AddField("password", "123456");
        www.AddField("c_password", "123456");
        UnityWebRequest wwwwR = UnityWebRequest.Post(basePath + "/register", www);
        yield return wwwwR.SendWebRequest();

        Debug.Log(wwwwR.error);
        Debug.Log(wwwwR.downloadHandler.text);

        yield return 0;




    }

    public void Put()
    {

        currentRequest = new RequestHelper
        {
            Uri = basePath + "/posts/1",
            Body = new Post
            {
                title = "foo",
                body = "bar",
                userId = 1
            },
            Retries = 5,
            RetrySecondsDelay = 1,
            RetryCallback = (err, retries) =>
            {
                Debug.Log(string.Format("Retry #{0} Status {1}\nError: {2}", retries, err.StatusCode, err));
            }
        };

        RestClient.Put<Post>(currentRequest, (err, res, body) =>
        {
            if (err != null)
            {
                this.LogMessage("Error", err.Message);
                Debug.Log(err.StackTrace);
            }
            else
            {
                this.LogMessage("Success", JsonUtility.ToJson(body, true));
            }
        });
    }

    public void Delete()
    {

        RestClient.Delete(basePath + "/posts/1", (err, res) =>
        {
            if (err != null)
            {
                this.LogMessage("Error", err.Message);
            }
            else
            {
                this.LogMessage("Success", "Status: " + res.StatusCode.ToString());
            }
        });
    }

    public void AbortRequest()
    {
        if (currentRequest != null)
        {
            currentRequest.Abort();
            currentRequest = null;
        }
    }

    public void DownloadFile()
    {

        var fileUrl = "https://raw.githubusercontent.com/IonDen/ion.sound/master/sounds/bell_ring.ogg";
        var fileType = AudioType.OGGVORBIS;

        RestClient.Get(new RequestHelper
        {
            Uri = fileUrl,
            DownloadHandler = new DownloadHandlerAudioClip(fileUrl, fileType)
        }).Then(res =>
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = ((DownloadHandlerAudioClip)res.Request.downloadHandler).audioClip;
            audio.Play();
        }).Catch(err =>
        {
            this.LogMessage("Error", err.Message);
        });
    }
}
