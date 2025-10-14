using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject playerGameObject;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - playerGameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerGameObject.transform.position + offset;
    }
}
