using UnityEngine;

public class BreakableWall_Controller : MonoBehaviour
{
    public GameObject intactWall;     
    public GameObject fracturedWall;   
    private bool isBroken = false;     
    void Start()
    {
        intactWall.SetActive(true);
        fracturedWall.SetActive(false);
    }
    public void Break()
    {
        if (isBroken) return;
        isBroken = true;

        intactWall.SetActive(false);     
        fracturedWall.SetActive(true);    

        foreach (Transform piece in fracturedWall.transform)
        {
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;   
                rb.useGravity = true;     
            }
        }
    }
}
