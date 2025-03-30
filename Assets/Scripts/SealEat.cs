using UnityEngine;

public class SealEat : MonoBehaviour
{
    public float rotationSpeed;
    public float rotationAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed*Time.deltaTime, Space.Self);
        rotationAngle = transform.eulerAngles.z;
        if(rotationAngle >= 30 && rotationAngle <= 330 || rotationAngle <= 330 && rotationAngle >= 30)
        {
            rotationSpeed = -rotationSpeed;
        }
    }
}
