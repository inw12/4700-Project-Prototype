using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private Vector2 cameraChange;    // how much to move camera
    [SerializeField] private Vector3 playerChange;    // how much to move player
    private CameraMovement cam;

    void Start() {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) // "if Player is inside the trigger zone..."
        {
            cam.minPosition += cameraChange;            // update cam's min bounds
            cam.maxPosition += cameraChange;            // update cam's max bounds
            other.transform.position += playerChange;   // move the character a bit; the camera should then follow the player
        }
    }
}
