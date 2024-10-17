using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public int pathLength = 20;  // Length of the path
    private float scale = 0.563f;      // Scale of the Perlin noise
    public GameObject[] platformPrefab; // Prefab for the platform

    private Vector3 lastPosition = Vector3.zero;
    void Start()
    {
        GeneratePath();
    }

    void GeneratePath()
    {
        Vector3 newPosition = Vector3.right * findGameObjectHalfWidth(Instantiate(platformPrefab[Random.Range(0, platformPrefab.Length)], Vector3.zero, Quaternion.identity));

        for (int i = 0; i < pathLength; i++)
        {
            // float x = i;
            float y = Mathf.PerlinNoise(i * 8.5f, 0) * 50;
            float z = Mathf.PerlinNoise(i * 17, 0) * 50;

            GameObject prefab = platformPrefab[Random.Range(0,platformPrefab.Length)];

            float leftLength = newPosition.x + Mathf.Abs(prefab.transform.GetChild(0).localPosition.x) + prefab.transform.GetChild(0).gameObject.GetComponent<Renderer>().bounds.extents.x;

            Instantiate(prefab, newPosition + Vector3.right * findGameObjectHalfWidth(prefab), Quaternion.identity);
            // Instantiate(prefab, newPosition + new Vector3(findGameObjectHalfWidth(prefab),y,z), Quaternion.identity);
            newPosition += Vector3.right * findGameObjectHalfWidth(prefab) * 2;
                
        }
    }

    float findGameObjectHalfWidth(GameObject gameObject)
    {
        if (gameObject.transform.childCount > 0)
        {
            GameObject firstGameObject = gameObject.transform.GetChild(0).gameObject;
            GameObject lastGameObject = gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject;
            float objectWidth = (lastGameObject.transform.position.x + lastGameObject.GetComponent<Renderer>().bounds.extents.x) - (firstGameObject.transform.position.x - firstGameObject.GetComponent<Renderer>().bounds.extents.x) ;

            return objectWidth / 2f ;
        }

        return -1f;
    }
}