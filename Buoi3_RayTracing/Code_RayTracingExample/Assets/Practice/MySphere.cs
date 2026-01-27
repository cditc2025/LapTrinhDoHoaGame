using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Practice
{
    public class MySphere : SceneObject
    {
        public Vector3 center;
        public float radius;
        public Color color;

        private void OnDrawGizmos()
        {
            center = transform.position;
            Gizmos.color = color;
            Gizmos.DrawSphere(center, radius);
        }

        public override HitData Intersect(MyRay ray)
        {
            HitData hitData = new HitData();
            
            //check if object is front of camera

            //check if ray is intersected with object and calculate distance from camera to hit point
            //delta = b*b - 4ac

            return hitData;
        }
    }
}

