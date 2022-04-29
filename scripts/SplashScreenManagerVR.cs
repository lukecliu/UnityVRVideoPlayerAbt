using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManagerVR : MonoBehaviour
{
    [SerializeField] OVROverlay overlay_Background;
    [SerializeField] OVROverlay overlay_LoadingText;
    [SerializeField] GameObject centerEyeAnchor;

    [Header("Next Scene Button")]
    [SerializeField] OVRInput.Button advSceneButton;
    [SerializeField] OVRInput.Controller advSceneController;
    [SerializeField] KeyCode altAdvSceneKeyCode;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Load Splash Screen");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit Application...");
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }


        if (OVRInput.GetDown(advSceneButton, advSceneController)
                || Input.GetKeyDown(altAdvSceneKeyCode))
        {
            Debug.Log("Load Main Game");
            LoadNextScene();
        }
    }

	private void LoadNextScene()
	{
        StartCoroutine(ShowOverlayAndLoad(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator ShowOverlayAndLoad(int sceneIndex)
    {
        overlay_Background.enabled = true;
        overlay_LoadingText.enabled = true;

        overlay_LoadingText.gameObject.transform.position = centerEyeAnchor.transform.position + new Vector3(0f, 0f, 3f);

        //yield return new WaitForSeconds(5f);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        overlay_Background.enabled = false;
        overlay_LoadingText.enabled = false;

        yield return null;
    }
}
