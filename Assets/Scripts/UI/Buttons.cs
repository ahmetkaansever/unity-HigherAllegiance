using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] AudioClip clicked;
    [SerializeField] AudioClip released;
    [SerializeField] AudioSource source;
    public void OnPointerUp(PointerEventData eventData)
    {
        source.PlayOneShot(released);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        source.PlayOneShot(clicked);
    }

    public void StartButtonClicked()
    {
        Invoke("LoadNextScene", 0f);
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(2);
        //int currentScene = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentScene + 1);
    }
    

}
