using UnityEngine;

public class CurveTrigger : MonoBehaviour
{
    public float desiredRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pats"))
        {
            other.GetComponent<Player>().yRotation = desiredRotation;
            Debug.Log("qqs");
        }
        
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
