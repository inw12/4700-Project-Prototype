using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;              // The name of the scene to load
    public Vector2 playerPosition;    
    public Vector3 cameraPosition;      
    public Vector2 maxCameraBounds;
    public Vector2 minCameraBounds;
    public VectorValue memPlayerPosition;         // where the player will end up in the next scene 
    public Vector3Value memCameraPosition;
    public MinMaxVectorValue memCameraBounds;  // bounds of the camera when scenes are switched
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;

    private void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) // "if Player is inside the trigger zone..."
        {
            memPlayerPosition.value = playerPosition;
            memCameraPosition.value = cameraPosition;
            memCameraBounds.max = maxCameraBounds;
            memCameraBounds.min = minCameraBounds;
            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null) {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncOp.isDone) {
            yield return null;
        }
    }
}
