using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [Header("Rotation Variables")]
    public float mouseAngle = 0;
    public float rotationSpeed = 5.0f;

    private Vector2 mouseDirection;
    private Quaternion rotator;

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerMovement>().bReadytoFight)
        {
            mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            mouseAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            rotator = Quaternion.AngleAxis(mouseAngle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotator, rotationSpeed * Time.deltaTime);
        }

    }
}
