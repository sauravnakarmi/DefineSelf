using UnityEngine;

public class MouseClickSound : MonoBehaviour
{
    [SerializeField] private AudioClip mouseClick;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SoundManager.instance.PlaySound(mouseClick);
        }
    }
}
