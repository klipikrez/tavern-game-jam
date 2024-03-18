using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PortalEnd : MonoBehaviour
{
    bool interactable = true;
    Coroutine delayCoroutine;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.sharedMaterial.name == "feet")
        {

            if (interactable)
            {
                if (delayCoroutine != null)
                    StopCoroutine(delayCoroutine);
                GameManager.Instance.reset = true;
                GameManager.Instance.Mirror();
                AudioManager.Instance.PlayAudioClip("end");
                delayCoroutine = StartCoroutine(c_Delay());
                interactable = false;

            }

        }

    }

    IEnumerator c_Delay()
    {

        yield return new WaitForSeconds(2f);
        interactable = true;
    }
}
