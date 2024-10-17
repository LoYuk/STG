using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class NewBehaviourScript : MonoBehaviour
{
    public Button StartButton = null;
    public Button ContinueButton = null;
    public Button HelpButton = null;
    public Button SettingsButton = null;
    public Button QuitButton = null;
    // private Selectable[] buttons = null;

    private void LoadingStartGameScene()
    {
        SceneManager.LoadScene("StartGameScene");
        Debug.Log("The current Scene is: " + SceneManager.GetActiveScene());
    }

    private void LoadingContinueGameScene()
    {
        SceneManager.LoadScene("ContinueGameScene");
        Debug.Log("The current Scene is: " + SceneManager.GetActiveScene());
    }

    private void LoadingSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
        Debug.Log("The current Scene is: " + SceneManager.GetActiveScene());
    }

    private void LoadingHelpScene()
    {
        SceneManager.LoadScene("HelpScene");
        Debug.Log("The current Scene is: " + SceneManager.GetActiveScene());
    }

    private void QuittingGame()
    {
        Application.Quit();
        // EditorApplication.isPlaying = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        // buttons = new Selectable[] { StartButton, ContinueButton, HelpButton, SettingsButton, QuitButton };

        StartButton.onClick.AddListener(LoadingStartGameScene);
        ContinueButton.onClick.AddListener(LoadingContinueGameScene);
        SettingsButton.onClick.AddListener(LoadingSettingsScene);
        HelpButton.onClick.AddListener(LoadingHelpScene);
        QuitButton.onClick.AddListener(QuittingGame);
    }
}
