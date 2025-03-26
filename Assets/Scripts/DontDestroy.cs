using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [HideInInspector]
    public string objectID;

    private void Awake()
    {
        objectID = name + transform.position.ToString() + transform.eulerAngles.ToString();
    }

    void Start()
    {
        for (int i = 0; i < Object.FindObjectsByType<DontDestroy>(FindObjectsInactive.Include, FindObjectsSortMode.None).Length; i++)
        {
            if (Object.FindObjectsByType<DontDestroy>(FindObjectsInactive.Include, FindObjectsSortMode.None)[i] != this)
            {
                if (Object.FindObjectsByType<DontDestroy>(FindObjectsInactive.Include, FindObjectsSortMode.None)[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }
        }
        
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
