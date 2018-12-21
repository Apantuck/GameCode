using UnityEngine;

namespace PointsOfInterest
{
    public struct PointOfInterest
    {
        public Transform pTransform;
        public float weight;

        // constructor
        public PointOfInterest(Transform t, float w)
        {
            pTransform = t;
            weight = Mathf.Clamp(w, 0, 1);
        }
    }
}

