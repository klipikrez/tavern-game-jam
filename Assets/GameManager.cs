using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        yield return new WaitForSeconds(1f);
        GameManager.Instance.reset = false;
        ChangeBehaviour(0);
        screenFlippedNumber++;

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
    }


    void Update()
    {
        behaviours[currentBehaviour].UpdateBehaviour(this);
    }

    public void ChangeBehaviour(int index)
    {
        Debug.Log("aaa");
        currentBehaviour = index;
        behaviours[currentBehaviour].StartBehaviour(this);
    }


}
