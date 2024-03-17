using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new editor beh", menuName = "gameManager/editor")]
public class EditBehaviour : BaseBehaviour
{
    public bool placePortals = true;
    public int currentPortal = 0;
    public override void StartBehaviour(GameManager manager)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateBehaviour(GameManager manager)
    {
        if (placePortals)
        {
            manager.portals[currentPortal].transform.position = new Vector3(manager.portals[currentPortal].transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        }
        if (Input.GetMouseButtonDown(0))
        {
            currentPortal++;
            if (currentPortal > 1)
            {
                placePortals = false;
            }
        }
    }
    public override void EndBehaviour(GameManager manager)
    {
        throw new System.NotImplementedException();
    }
}