using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public AudioSource clickSound;
    public GameObject creditsMenu;

    private void Awake()
    {
        creditsMenu.SetActive(false);
    }
    public void PlayButtonClick()
    {
        clickSound.Play();
        SceneManager.LoadScene("Level1");
    }
    public void CreditsButtonClick()
    {
        clickSound.Play();
        if(creditsMenu.activeSelf) 
            creditsMenu.SetActive(false);
        else
            creditsMenu.SetActive(true);
    }
    public void ExitButtonClick()
    {
        clickSound.Play();
        Application.Quit();
    }
}
