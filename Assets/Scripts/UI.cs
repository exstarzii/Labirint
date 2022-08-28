using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject pausePanel;
    public Text goalText;
    GameObject finish;
    GameObject player;
    bool isPaused = false;
    public float distanceWin;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        finish = GameObject.FindGameObjectWithTag("Finish");
        player = GameObject.FindGameObjectWithTag("Player");
        goalText.gameObject.SetActive(true);
        StartCoroutine(CoroutineText());
        StartCoroutine(CoroutineCheck());
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SetPause();
        }
    }
    public void SetPause()
    {
        isPaused = !isPaused;
        Cursor.lockState = isPaused? CursorLockMode.Confined:CursorLockMode.Locked;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Exit()
    {
        Application.Quit();
    }
    IEnumerator CoroutineText()
    {
        yield return new WaitForSeconds(2f);
        goalText.gameObject.SetActive(false);
    }
    IEnumerator CoroutineCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (Vector3.Distance(player.transform.position, finish.transform.position)< distanceWin)
            {
                StartCoroutine(CoroutineWin());
                break;
            }
        }
        
    }
    IEnumerator CoroutineWin()
    {
        goalText.text = "Вы выиграли";
        goalText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Menu");
    }

}
