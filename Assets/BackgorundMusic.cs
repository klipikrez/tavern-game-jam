using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgorundMusic : MonoBehaviour
{
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayAudioClipLooping(name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
