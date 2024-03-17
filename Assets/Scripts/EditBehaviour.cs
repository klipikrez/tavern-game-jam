using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Functions;

[CreateAssetMenu(fileName = "new editor beh", menuName = "gameManager/editor")]
public class EditBehaviour : BaseBehaviour
{
    public bool placePortals = true;
    public bool placeItem = false;
    public int currentPortal = 0;
    SpriteRenderer portalSprite;
    public override void StartBehaviour(GameManager manager)
    {
        currentPortal = 0;
        placePortals = true;
        isDragging = false;
        placeItem = false;
        if (portalSprite == null)
        {
            portalSprite = manager.portals[0].transform.Find("spawn").GetComponent<SpriteRenderer>();
        }
    }

    public override void UpdateBehaviour(GameManager manager)
    {
        if (placePortals)
        {
            PlacePortal(manager);
        }
        else
        {
            if (!placeItem)
            {
                placeItem = true;
                isDragging = true;
                draggedObject = PlaceItem(manager);
            }
            DragObject(manager);
        }
    }

    void PlacePortal(GameManager manager)
    {

        if (currentPortal == 0)
        {
            portalSprite.color = Color.white;
            DrawBox(manager.portals[0].transform.position + new Vector3(0.5f, 0, 0), quaternion.identity, Vector2.one, Color.red);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(manager.portals[0].transform.position + new Vector3(0.5f, 0, 0), Vector2.one, 0f);
            string aa = "";
            foreach (Collider2D col in colliders)
            {
                if (col != null && col.gameObject.layer != LayerMask.NameToLayer("Player") && col.gameObject.name != "portal start")
                {
                    aa += col.gameObject.name;
                    portalSprite.color = Color.red;
                }
            }
            Debug.Log(aa);
        }
        manager.portals[currentPortal].transform.position = new Vector3(manager.portals[currentPortal].transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if (Input.GetMouseButtonDown(0) && portalSprite.color != Color.red)
        {
            currentPortal++;
            if (currentPortal > 1)
            {
                placePortals = false;
            }
        }



    }

    public LayerMask objectLayer;
    private GameObject draggedObject;
    private bool isDragging = false;
    void DragObject(GameManager manager)
    {

        bool canPlace = true;


        Vector2 mousePosition = manager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        draggedObject.transform.position = new Vector2(mousePosition.x, mousePosition.y);
        Bounds bounds = draggedObject.GetComponent<EdgeCollider2D>().bounds;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0);
        foreach (Collider2D col in colliders)
        {
            EdgeCollider2D eCol = (EdgeCollider2D)col;
            if (eCol == null)
            {
                continue;
            }
            if (CheckIntersection(draggedObject.GetComponent<EdgeCollider2D>(), eCol))
            {
                canPlace = false;
            }
        }
        SpriteRenderer rend = draggedObject.GetComponent<SpriteRenderer>();
        if (canPlace)
        {
            rend.color = Color.white;
        }
        else
        {
            rend.color = Color.red;
        }


        if (isDragging && Input.GetMouseButtonDown(0) && canPlace)
        {
            isDragging = false;
        }
    }
    GameObject PlaceItem(GameManager manager)
    {
        GameObject nextPrefab = Resources.Load<GameObject>("Prefabs/obj" + manager.i);
        if (nextPrefab != null)
        {
            GameObject newObj = Instantiate(nextPrefab, Vector3.zero, Quaternion.identity);
            if (manager.i >= manager.objects.Length)
            {
                // Resize the array to accommodate the new object
                Array.Resize(ref manager.objects, manager.i + 1);
            }
            manager.objects[manager.i] = newObj;
            manager.i++;
            return newObj;
        }
        else
        {
            Debug.LogError("Prefab with name '" + "obj" + manager.i + "' not found in Resources folder!");
        }
        return null;

        //zatim dodati prefab u array objekata
    }
    public override void EndBehaviour(GameManager manager)
    {
        throw new System.NotImplementedException();
    }
}