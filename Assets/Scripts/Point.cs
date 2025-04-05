using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject[] point;
    public GameObject spawnPoint;
    public Drag drag;
    [SerializeField] private AudioClip pointSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            // Load the next level or perform any other action
            Debug.Log("Add Point!");
            SoundManager.instance.PlaySound(pointSound);
            ScoreManager.instance.AddPoint();
            Destroy(drag.m_TargetJoint);
            SpawnObject();
            Destroy(gameObject);
            // Example: Load the next scene
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (collision.CompareTag("Obstacle"))
        {
            // Load the next level or perform any other action
            Debug.Log("You Got Hit!");
            Destroy(drag.m_TargetJoint);
            SpawnObject();
            Destroy(gameObject);
            // Example: Load the next scene
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (collision.CompareTag("OutOfBounds"))
        {
            // Load the next level or perform any other action
            Debug.Log("Wall Touched!");
            Destroy(drag.m_TargetJoint);
            SpawnObject();
            Destroy(gameObject);
            // Example: Load the next scene
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void SpawnObject()
    {
        int n = Random.Range(0, point.Length);
        Instantiate(point[n], new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y), Quaternion.identity);

    }
}
