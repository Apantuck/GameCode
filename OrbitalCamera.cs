using System.Collections.Generic;
using UnityEngine;
using PointsOfInterest;
using UnityStandardAssets.CrossPlatformInput;

/* Alex Pantuck
 * Orbital follow camera for 3rd person character.
 * 
 * The actual camera should be a child of the gameobject
 * containing this component (otherwise camerashake
 * will get funky).
 * 
 */

public class OrbitalCamera : MonoBehaviour
{
    [Header("Position")]
    public Transform target;
    public Transform lockedOnPosition;
    public float distance = 5.0f;
    public float verticalOffset = 0.0f;
    public float maxHeight = 2.0f; // relative to position + vertical offset
    public float minHeight = -1.0f;

    [Header("Movement Speed")]
    public float xSpeed = 5.0f;
    public float ySpeed = 5.0f;
    public float rotationDampTime = 0.3f;
    public float movementDampTime = 0.3f;

    [Header("Camera Shake")]
    public float maxAngleX = 5.0f;
    public float maxAngleY = 5.0f;
    public float maxAngleZ = 5.0f;
    public float shakeFrequency = 10.0f;
    public float decaySpeed = 0.7f;

    private Transform myCamera;
    private Vector3 velocity = Vector3.zero;
    private float prev_y;
    private static float trauma = 0;
    private List<PointOfInterest> pointsOfInterest = new List<PointOfInterest>();
    private PointOfInterest lockTarget;
    private LayerMask mask;
    private bool isLockedOn = false;

    private void Start()
    {
        if (transform.childCount < 1)
        {
            Debug.Log("Warning, OrbitalCamera requires the camera gameobject to be a child of its transform.");
        }
        myCamera = transform.GetChild(0);
        prev_y = target.position.y;
        // Ignore the player and enemy layers
        mask = ~(LayerMask.NameToLayer("Player") | LayerMask.NameToLayer("Enemy"));
    }

    private void LateUpdate ()
    {
        Vector3 targetPos = target.position;
        targetPos.y += verticalOffset;

        Adjust_Height(targetPos);
        Follow_Player(targetPos);
        Orbit(targetPos);
        Camera_Shake();
    }

    // As target ascends/descends, move up/down by the same amount to keep up
    private void Adjust_Height(Vector3 targetPos)
    {
        float cur_y = targetPos.y;
        float diff = cur_y - prev_y;
        prev_y = cur_y;

        transform.Translate(Vector3.up * diff);
    }

    // Move towards player and look towards player. Adjust for points of interest / lock on
    private void Follow_Player(Vector3 targetPos)
    {
        Vector3 fromDir = (transform.position - targetPos).normalized; // direction from target to camera
        Vector3 new_pos = (isLockedOn) ? lockedOnPosition.position : (fromDir * distance) + targetPos;
        transform.position = Vector3.SmoothDamp(transform.position, new_pos, ref velocity, movementDampTime);

        Vector3 toDir = fromDir * -1; // direction from camera to target
        Adjust_Focus(ref toDir);
        transform.forward = Vector3.SmoothDamp(transform.forward, toDir, ref velocity, rotationDampTime);
    }

    // controls for moving the camera
    private void Orbit(Vector3 targetPos)
    {
        if (isLockedOn) return;

        float rot_x = CrossPlatformInputManager.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        float rot_y = CrossPlatformInputManager.GetAxis("Mouse Y") * xSpeed * Time.deltaTime * -1;

        transform.RotateAround(targetPos, Vector3.up, rot_x);
        transform.RotateAround(targetPos, Vector3.right, rot_y);

        float verticalBoundary;
        float heightDiff = transform.position.y - targetPos.y;
        verticalBoundary = (heightDiff > maxHeight) ? -heightDiff : 0;
        verticalBoundary = (heightDiff < minHeight) ? -heightDiff : verticalBoundary;
        //transform.RotateAround(targetPos, Vector3.right, verticalBoundary);
    }

    private void Wall_Detection()
    {

    }

    /// <summary>
    /// Camera Shake
    /// </summary>

    private void Camera_Shake()
    {
        float intensity = Mathf.Pow(trauma, 2.0f);
        if (trauma > 0) trauma -= (Time.deltaTime * decaySpeed);
        else trauma = 0;

        float seed = transform.rotation.z;
        float rotX = maxAngleX * intensity * Noise(seed + 1);
        float rotY = maxAngleY * intensity * Noise(seed + 2);
        float rotZ = maxAngleZ * intensity * Noise(seed + 3);

        myCamera.rotation = transform.rotation;
        myCamera.Rotate(rotX, rotY, rotZ);
    }

    public static void Add_Trauma(float amount)
    {
        trauma += amount;
        trauma = Mathf.Clamp(trauma, 0, 1);
    }

    private float Noise(float seed)
    {
        float frequency = Time.time * shakeFrequency;
        return Map(Mathf.PerlinNoise(seed, frequency), 0, 1, -1, 1);
    }

    private float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    /// <summary>
    /// Points of interest
    /// </summary>
    /// <param name="dir"></param>

    // in which direction should the camera look?
    private void Adjust_Focus(ref Vector3 dir)
    {
        // Lerp between player and locked target by weight % and look towards that position
        if (lockTarget.pTransform != null)
        {
            dir = (Vector3.Lerp(target.position, lockTarget.pTransform.position, lockTarget.weight) - transform.position).normalized;
            return;
        }

        // Else, give the weighted position of all points of interest
        foreach(PointOfInterest poi in pointsOfInterest)
        {
            dir += (poi.pTransform.position - transform.position).normalized * poi.weight;
        }
    }

    public void Add_POI(PointOfInterest poi)
    {
        if (!pointsOfInterest.Contains(poi))
            pointsOfInterest.Add(poi);
    }

    public void Remove_POI(PointOfInterest poi)
    {
        if (pointsOfInterest.Contains(poi))
            pointsOfInterest.Remove(poi);
    }
}
