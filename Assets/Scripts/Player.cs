using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int Life = 5;
    public TextMesh LifeText;
    public AudioClip Ding;
    public ActivateTarget activateTarget;
    public AudioClip Buzz;
    public AudioClip Win;
    public LevelCreator Create;
    public TextMesh starText;
    public TextMesh pointsText;
    public TextMesh usernameText;
    public static int totalStars = 0;
    public static int current_streak = 0;
    public static int level = 1;
    public static bool gameOn = true;
    public static bool gameReset = false;
    public static Vector3 originalPos;
    private Vector3 starOriginalPos;
    public AdManager adManager;
    public static bool isDead = false;
    public static bool isReverse = false;

    private Vector3 decrementResizeFactor;

    private Vector3 incrementResizeFactor;

    private Vector3 defaultSphereSize;

    private SphereCollider PickRangeCollider;



    void Start()
    {
        originalPos = transform.position;
        decrementResizeFactor = new Vector3(0.2f, 0.2f, 0.2f);
        incrementResizeFactor = new Vector3(0.3f, 0.3f, 0.3f);
        defaultSphereSize = new Vector3(0.55f, 0.5f, 0.5f);

        PickRangeCollider = transform.GetChild(1).GetComponent<SphereCollider>();

    }
    GameObject starObject;

    void UpdateUserData()
    {
        /*  
           0. Calculate Points and everything 
               * Points = Stars * Level
           1. Data Sync
               * Online
                   - Send data to server
               * Offline
                   - Send data to PlayerPrefs
                   - Save locally 
           2. Game Logic
               * Reset Game Stats
               * Return checkpoint

           */
        //Debug.Log("Current Stars: " + totalStars + " Current Level: " + level);
        DBManager.fetchedUser.points += totalStars * level;
        DBManager.fetchedUser.star_collected += totalStars;

        if (DBManager.fetchedUser.normal_highest < level)
        {
            DBManager.fetchedUser.normal_highest = level;
            DBManager.fetchedUser.checkpoint_level = level - (level % 5);
        }

        WWWForm www = new WWWForm();
        www.AddField("points", DBManager.fetchedUser.points);
        www.AddField("star_collected", DBManager.fetchedUser.star_collected);
        www.AddField("normal_highest", DBManager.fetchedUser.normal_highest);
        www.AddField("checkpoint_level", DBManager.fetchedUser.checkpoint_level);

        DBManager.Instance.UpdateUser(www, () =>
        {
            //Update UI
            SetUITexts();

            //Reset Current Temp Variables
            totalStars = 0;

        });
    }

    // void OnCollisionEnter(Collision col)
    // {
    //     if (col.gameObject.name.Contains("Box"))
    //     {
    //         Die();
    //     }
    //     else if (col.gameObject.name.Contains("Target"))
    //     {
    //         if (ActivateTarget.targetActivated)
    //         {
    //             //Update User Data
    //             if (Menu.userLoggedin)
    //                 UpdateUserData();

    //             ActivateTarget.targetActivated = false;
    //             GetComponent<AudioSource>().clip = Win;
    //             level++;
    //             PlayerPrefs.SetString("Level", level.ToString());
    //             if (AudioManager.soundstatus)
    //             {
    //                 GetComponent<AudioSource>().Play();
    //             }

    //             ResetGame(false);
    //         }
    //         else
    //         {
    //             Die();
    //         }

    //     }
    //     else if (col.gameObject.name.Contains("Star"))
    //     {
    //         GetComponent<AudioSource>().clip = Ding;

    //         LevelCreator.currLevelStarCount--;
    //         if (LevelCreator.currLevelStarCount == 0)
    //             GameObject.Find("Map/Target(Clone)").GetComponent<ActivateTarget>().Activate();
    //         starObject = col.gameObject;

    //         totalStars++;

    //         starOriginalPos = starObject.transform.position;
    //         col.gameObject.transform.position += Vector3.forward * 10;
    //         if (AudioManager.soundstatus)
    //         {
    //             GetComponent<AudioSource>().Play();
    //         }
    //     }
    //     else if (col.gameObject.name.Contains("Portal"))
    //     {
    //         transform.position = col.gameObject.GetComponent<TargetPortal>().target.transform.position;
    //     }
    // }

    public void OnChildTriggerEntered(Collider collider, Vector3 pos)
    {
        Debug.Log("hello " + collider.name);
        if (collider.name.Contains("Box"))
        {
            Die();
        }
        else if (collider.gameObject.name.Contains("Target"))
        {
            MeetTarget();
        }
        else if (collider.gameObject.name.Contains("Star"))
        {
            CollectStar(collider.gameObject);
        }
        else if (collider.gameObject.name.Contains("Collect"))
        {
            //Collectibles
            if (collider.gameObject.name.Contains("CollectPickRange"))
                CollectPickRange(collider.gameObject);
            else if (collider.gameObject.name.Contains("CollectSpeed"))
                CollectSpeed(collider.gameObject);
            else if (collider.gameObject.name.Contains("CollectReverse"))
                CollectReverse(collider.gameObject);
            else if (collider.gameObject.name.Contains("CollectResize"))
                CollectResize(collider.gameObject);
            else if (collider.gameObject.name.Contains("CollectFreeze"))
                CollectFreeze(collider.gameObject);

        }
        else if (collider.gameObject.name.Contains("Portal"))
        {
            transform.position = collider.gameObject.GetComponent<TargetPortal>().target.transform.position;
        }
    }
    void Die()
    {
        GetComponent<AudioSource>().clip = Buzz;
        if (AudioManager.soundstatus)
        {
            GetComponent<AudioSource>().Play();
        }
        if (Life > 0)
        {
            Life--;
            LifeText.text = Life.ToString();
            totalStars = 0;
            if (ToggleAds.adsToggle)
                adManager.DisplayVideoAd();
            ResetGame(false);
        }
        else
        {


            Debug.Log("GAME OVER");
            totalStars = 0;
            if (Menu.userLoggedin)
                UpdateUserData();
            level = (level - level % 5);
            if (level == 0)
                level = 1;
            ResetGame(true);
        }
    }

    void SetUITexts()
    {
        //Bunlar bölüm geçince updatelencek
        starText.text = DBManager.fetchedUser.star_collected.ToString();
        pointsText.text = DBManager.fetchedUser.points.ToString();
    }
    public void ResetGame(bool is_dead)
    {

        if (is_dead)
        {
            totalStars = 0;
            Life = 5;
            current_streak = 0;
            LifeText.text = "5";
            //Reset Collectible Effects
            PickRangeCollider.radius = 0.5f;
            transform.localScale = defaultSphereSize;
            GetSwipe.ballSpeed = 3;
            isReverse = false;
        }

        GetSwipe.startedMovement = false;
        transform.position = originalPos;

        //gameOn = false;
        //StartRound.count = 0;
        //GetInput.actionOrder.Clear();

        Create.CreateMap();
        //Create.CreateNewLevel();
        //Debug.Log(GetInput.actionOrder.Count);
    }

    void PlaySound()
    {
        if (AudioManager.soundstatus)
        {
            GetComponent<AudioSource>().clip = Ding;
            GetComponent<AudioSource>().Play();
        }
    }

    public void CollectFreeze(GameObject col)
    {


        Create.GetComponent<FreezeBox>().BroadcastFreeze();

        col.transform.position += Vector3.forward * 10;

        PlaySound();


    }

    public void CollectStar(GameObject col)
    {
        LevelCreator.currLevelStarCount--;
        if (LevelCreator.currLevelStarCount == 0)
            GameObject.Find("Map/Target(Clone)").GetComponent<ActivateTarget>().Activate();
        starObject = col;

        totalStars++;

        starOriginalPos = starObject.transform.position;
        col.transform.position += Vector3.forward * 10;

        PlaySound();

    }

    public void CollectResize(GameObject col)
    {
        int x = Random.Range(0, 2);
        if (x == 0)
        {
            if (transform.localScale.x > 0.3f)
                transform.localScale -= decrementResizeFactor;
        }
        else
        {
            if (transform.localScale.x < 1f)
                transform.localScale += incrementResizeFactor;
        }

        PlaySound();
        col.transform.position += Vector3.forward * 10; //destroy somehow
        Debug.Log("Scale changed to ->" + transform.localScale);
    }
    public void CollectReverse(GameObject col)
    {
        int x = Random.Range(0, 2);
        if (x == 0)
            isReverse = true;
        else
            isReverse = false;

        PlaySound();
        col.transform.position += Vector3.forward * 10; //destroy somehow
        Debug.Log("Swipe changed to ->" + isReverse);
    }
    public void CollectSpeed(GameObject col)
    {
        int x = Random.Range(0, 2);
        if (x == 0)
            GetSwipe.ballSpeed += 0.5f;
        else
            GetSwipe.ballSpeed -= 0.5f;

        PlaySound();
        col.transform.position += Vector3.forward * 10; //destroy somehow
        Debug.Log("Speed changed to ->" + GetSwipe.ballSpeed);
    }
    public void CollectPickRange(GameObject col)
    {
        int x = Random.Range(0, 2);
        if (x == 0)
            PickRangeCollider.radius += 0.2f;
        else
            PickRangeCollider.radius -= 0.2f;
        PlaySound();
        Debug.Log("Range changed to -> " + PickRangeCollider.radius);
        col.transform.position += Vector3.forward * 10;
    }
    public void MeetTarget()
    {

        if (ActivateTarget.targetActivated)
        {
            current_streak++;
            //Update User Data
            if (Menu.userLoggedin)
                UpdateUserData();

            ActivateTarget.targetActivated = false;
            GetComponent<AudioSource>().clip = Win;
            level++;
            PlayerPrefs.SetString("Level", level.ToString());
            if (AudioManager.soundstatus)
            {
                GetComponent<AudioSource>().Play();
            }

            ResetGame(false);
        }
        else
        {
            Die();
        }
    }

    public void calculateStreakHighest(int current_streak)
    {
        if (DBManager.fetchedUser.streak_highest < current_streak)
        {
            DBManager.fetchedUser.streak_highest = current_streak;
        }
    }
}