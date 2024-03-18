using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject CreditsPanel;
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Credits()
    {
        CreditsPanel.SetActive(true);
    }
    public void Close()
    {
        CreditsPanel.SetActive(false);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
