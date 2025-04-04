// This script should be an a empty gameobject somewhere in the scene
// DontDestroyOnLoad would be great (like for the most manager)

using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // Assign the prefab in the inspector
    public GameObject EnemyPrefab;
    //Singleton
    private static PrefabManager m_Instance = null;
    public static PrefabManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (PrefabManager)FindFirstObjectByType(typeof(PrefabManager));
            }
            return m_Instance;
        }
    }
}
