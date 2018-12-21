using UnityEngine;
using PointsOfInterest;

public class test : MonoBehaviour
{

    //public Transform spawnPoint;
    //public WeaponItem item;
    private PointOfInterest poi;
    public float importance = 0.5f;

    private void Awake()
    {
        poi = new PointOfInterest(transform, importance);
    }

    // Update is called once per frame
    void Update () {
        // if (Input.GetKeyDown("e")) item.Equip(spawnPoint);
        //if (Input.GetKeyDown("l")) SceneManager.LoadScene(0);
        if (Input.GetKeyDown("s")) Camera.main.GetComponentInParent<OrbitalCamera>().Add_POI(poi);
        else if (Input.GetKeyDown("w")) Camera.main.GetComponentInParent<OrbitalCamera>().Remove_POI(poi);
    }
}
