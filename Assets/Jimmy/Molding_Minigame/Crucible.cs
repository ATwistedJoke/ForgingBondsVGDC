using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Crucible : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float maxTilt = 90f;
    [SerializeField] private float minTilt = 0f;

    public GameObject crucible;

    void Update()
{
    RectTransform crucibleRect = crucible.GetComponent<RectTransform>();

    float tiltInput = 0f;

    //rotate left, using A; right, using D
    if (Input.GetKey(KeyCode.A)) tiltInput = 1f;
    if (Input.GetKey(KeyCode.D)) tiltInput = -1f;

    crucibleRect.localRotation *= Quaternion.Euler(0, 0, tiltInput * rotationSpeed * Time.deltaTime);
    float z = crucibleRect.localEulerAngles.z;
    if (z > 180) z -= 360;

    z = Mathf.Clamp(z, -maxTilt, minTilt);
    crucibleRect.localEulerAngles = new Vector3(0, 0, z);

}
}