using UnityEngine;
using UnityEditor;
using Models;
using Proyecto26;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;

public class MainScript : MonoBehaviour
{

    private readonly string basePath = "http://localhost:8000/api/v1";
    private RequestHelper currentRequest;

    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        EditorUtility.DisplayDialog(title, message, "Ok");
#else
		Debug.Log(message);
#endif
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