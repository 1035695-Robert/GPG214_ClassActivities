using Mono.Cecil;
using UnityEngine;

public class SpecialFolders : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        if (playerPrefab == null)
        {
           playerPrefab = Resources.Load<GameObject>("Player");
        }
       Instantiate(playerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
