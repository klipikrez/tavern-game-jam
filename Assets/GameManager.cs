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
    public BaseBehaviour[] behaviours;
    public int currentBehaviour = 0;
    public GameObject[] portals;

    public GameObject allowedArea;
    public LayerMask objectLayer;
    private Camera mainCamera;
    private Collider2D allowedAreaCollider;
    private bool isDragging = false;
    private GameObject draggedObject;

    void Mirror()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            Vector2 flip = objects[i].transform.localScale;
            Vector2 curretPos = objects[i].transform.position;
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

            objects[i].transform.position = mirroredPos;
            objects[i].transform.localScale = flip;
        }
        bool x = !flipp;
        flipp = x;
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
    void PlaceItem()
    {
        GameObject nextPrefab = Resources.Load<GameObject>("Prefabs/obj" + i);
        if (nextPrefab != null)
        {
            GameObject newObj = Instantiate(nextPrefab, Vector3.zero, Quaternion.identity);
            if (i >= objects.Length)
            {
                // Resize the array to accommodate the new object
                Array.Resize(ref objects, i + 1);
            }
            objects[i] = newObj;
        }
        else
        {
            Debug.LogError("Prefab with name '" + "obj" + i + "' not found in Resources folder!");
        }
        i++;
        Debug.Log(objects.Length);

        //zatim dodati prefab u array objekata
    }
    void Start()
    {
        mainCamera = Camera.main;
        allowedAreaCollider = allowedArea.GetComponent<Collider2D>();
        PlaceItem();
        behaviours[currentBehaviour].StartBehaviour(this);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, objectLayer);

            if (hit.collider != null && hit.collider.gameObject == objects[i-1])
            {
                draggedObject = hit.collider.gameObject;
                isDragging = true;
            }
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            draggedObject.transform.position = new Vector2(mousePosition.x, mousePosition.y);
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        behaviours[currentBehaviour].UpdateBehaviour(this);
    }
}
