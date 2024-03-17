using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PortalEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //NewLevel
            //RotationAnim();
        }

    }
}
