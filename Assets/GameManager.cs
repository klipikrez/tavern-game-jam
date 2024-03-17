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
            Instantiate(nextPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab with name '" + "obj"+i + "' not found in Resources folder!");
        }
        i++;
        //zatim dodati prefab u array objekata
    }
    void Start()
    {
        PlaceItem();
    }


    void Update()
    {
        //behaviours[currentBehaviour].UpdateBehaviours(this);
    }
}
