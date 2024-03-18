using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] objects; //objekti koje player postavlja
    [SerializeField] private bool flipp = true; //flipp true rotira po y, false po x
    public int i = 0; //object index
    int screenFlippedNumber = 0;
    public BaseBehaviour[] behaviours;
    public int currentBehaviour = 0;
    public GameObject[] portals;

    public GameObject allowedArea;

    public Camera mainCamera;
    public Camera raycastingCamera;
    public GameObject player;
    public static GameManager Instance;
    public Animator animator;
    Coroutine mirrorCoroutine;
    public GameObject placeHere;
    public bool reset = false;
    public Material mat;
    public TMP_Text tajmer;
    public GameObject lose;
    public void Lose()
    {
        lose.SetActive(true);
        Time.timeScale = 0;
    }
    private void Awake()
    {
        Instance = this;
    }

    public void Mirror()
    {
        if (mirrorCoroutine != null)
        {
            StopCoroutine(mirrorCoroutine);
        }
        mirrorCoroutine = StartCoroutine(c_Mirror());
    }

    IEnumerator c_Mirror()
    {
        string animationName = "";

        switch (screenFlippedNumber % 4)
        {
            case 0:
                animationName = "leftRight";
                break;
            case 1:
                animationName = "topDown";
                break;
            case 2:
                animationName = "rightLeft";
                break;
            case 3:
                animationName = "downTop";
                break;
            default:
                Debug.Log("aaaaaaaaa-" + screenFlippedNumber + " -- " + screenFlippedNumber);
                break;
        }
        Debug.Log(animationName);
        animator.Play("chill");
        animator.Play(animationName);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < objects.Length; i++)
        {
            Vector2 flip = objects[i].transform.localScale;
            Vector2 curretPos = objects[i].transform.localPosition;
            Vector2 mirroredPos;
            if (!flipp)
            {
                mirroredPos = new Vector2(-curretPos.x, curretPos.y);
                flip.x *= -1;
            }
            else
            {
                mirroredPos = new Vector2(curretPos.x, -curretPos.y);
                flip.y *= -1;
            }

            objects[i].transform.localPosition = mirroredPos;
            objects[i].transform.localScale = flip;
        }
        bool x = !flipp;
        flipp = x;
        FlipColors();
        yield return new WaitForSeconds(1f);
        GameManager.Instance.reset = false;
        ChangeBehaviour(0);
        screenFlippedNumber++;

    }
    void FlipColors()
    {
        float b = mat.GetFloat("_mode");
        mat.SetFloat("_mode", b > 0.5f ? 0 : 1);
    }
    void NewLevel()
    {
        //place new object
        //Mirror()
    }
    void MovePortal()
    {
        //pomeriti portale po y osi u odredjenim granicama
    }

    void Start()
    {
        mainCamera = Camera.main;
        behaviours[currentBehaviour].StartBehaviour(this);
        AudioManager.Instance.PlayAudioClipLooping("muzka", 0.5f);
    }


    void Update()
    {
        behaviours[currentBehaviour].UpdateBehaviour(this);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ChangeBehaviour(int index)
    {
        Debug.Log("aaa");
        currentBehaviour = index;
        behaviours[currentBehaviour].StartBehaviour(this);
    }


}
