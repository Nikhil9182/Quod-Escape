using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private IEnumerator coroutine;

    public GameObject player;
    public NavMeshAgent[] enemy;
    public GameObject[] key;
    public Animator playerAnimator;
    public Light[] pointLight;
    public ParticleSystem bloodEffect;
    public Text textDisplay;
    public Image[] keysImage;
    public Animator timer;
    public Canvas gameUI, levelWinUI, levelLooseUI;

    [SerializeField]
    private float TimeForLevelEnd;

    public float initialSpeed;
    public float fovAngle = 45f;
    float waitSeconds = 2.0f;

    public int keyCount = 0;
    public bool slidePillar = false;
    public bool openDoor = false;
    public bool switchLasers = true;
    public bool isWon = false;
    public bool canMove = true;
    public bool isSeen = false;
    public bool isloose = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        textDisplay.text = TimeForLevelEnd.ToString();
        timer.SetBool("onTimer", true);
    }

    void Update()
    {
        if(!isWon)
        {
            if (TimeForLevelEnd > 0)
            {
                TimeForLevelEnd -= Time.deltaTime;
                textDisplay.text = Mathf.Round(TimeForLevelEnd).ToString();
            }
        }
        if (TimeForLevelEnd < 0)
        {
            TimeForLevelEnd = 0f;
            timer.SetBool("onTimer", false);
            canMove = false;
            playerAnimator.SetBool("isrunning", false);
            playerAnimator.SetBool("isdefeated", true);
            isloose = true;
        }
        if (isSeen && (keyCount < 1))
        {
            canMove = false;
            playerAnimator.SetBool("isrunning", false);
            playerAnimator.SetBool("isdefeated", true);
            isloose = true;
        }
        if(isloose)
        {
            coroutine = OnLoosing(waitSeconds);
            StartCoroutine(coroutine);
        }
        if(isWon)
        {
            coroutine = OnWinning(waitSeconds);
            StartCoroutine(coroutine);
        }
    }

    public void KeyVisibilityUI()
    {
        keysImage[(keyCount - 1)].color = Color.white;
    }

    IEnumerator OnWinning(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        gameUI.enabled = false;
        levelWinUI.enabled = true;
    }

    IEnumerator OnLoosing(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        gameUI.enabled = false;
        levelLooseUI.enabled = true;

    }
}
