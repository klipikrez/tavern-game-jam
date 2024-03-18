using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PortalEnd : MonoBehaviour
{
    bool interactable = true;
    Coroutine delayCoroutine;
    
    public PhysicsMaterial2D Slippery;
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
                /*int randomObj = Random.Range(0, GameManager.Instance.objects.Length - 1);
                GameManager.Instance.objects[randomObj].GetComponent<EdgeCollider2D>().sharedMaterial = Slippery;*/

            }

        }

    }

    IEnumerator c_Delay()
    {

        yield return new WaitForSeconds(2f);
        interactable = true;
    }
}
