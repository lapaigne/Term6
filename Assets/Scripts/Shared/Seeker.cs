using UnityEngine;
using UnityEditor;
using System.Linq;

// component has no use at the moment
public class Seeker : MonoBehaviour
{
    // use data from elsewhere instead to avoid duplicates
    private PlayerData playerData;

    [SerializeField]
    public string targetTag = "Interactable";
    
    // yet figure out what to do with Tags.cs
    public string seekerTag;

    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
        seekerTag = gameObject.tag;   
    }

    private void Start()
    {
        //FindObjects(5f, "Interactable");
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// Finds all <c>GameObject</c>s with <c>searchTag</c> within <c>searchDistance</c>
    /// </summary>
    /// <returns>Array of <c>GameObject</c></returns>
    GameObject[] FindObjects(float searchDistance, string searchTag)
    {
        GameObject[] result = GameObject.FindGameObjectsWithTag(searchTag)
            .Where(go =>
            {
                float distance = Vector3.Distance(go.transform.position, gameObject.transform.position);
                Debug.Log(distance);
                return (distance <= searchDistance);
            })
            .ToArray();

        return result;
    }
}
