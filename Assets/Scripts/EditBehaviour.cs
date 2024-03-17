using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Functions;

[CreateAssetMenu(fileName = "new editor beh", menuName = "gameManager/editor")]
public class EditBehaviour : BaseBehaviour
{
    public bool placePortals = true;
    public int currentPortal = 0;
    SpriteRenderer portalSprite;
    public override void StartBehaviour(GameManager manager)
    {
        currentPortal = 0;
        placePortals = true;
        if (portalSprite == null)
        {
            portalSprite = manager.portals[0].transform.Find("spawn").GetComponent<SpriteRenderer>();
        }
    }

    public override void UpdateBehaviour(GameManager manager)
    {
        if (placePortals)
        {
            if (currentPortal == 0)
            {
                portalSprite.color = Color.white;
                DrawBox(manager.portals[0].transform.position + new Vector3(0.5f, 0, 0), quaternion.identity, Vector2.one, Color.red);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(manager.portals[0].transform.position + new Vector3(0.5f, 0, 0), Vector2.one, 0f);
                foreach (Collider2D col in colliders)
                {
                    if (col != null && col.gameObject.layer != LayerMask.NameToLayer("Player"))
                    {
                        portalSprite.color = Color.red;
                    }
                }
            }
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