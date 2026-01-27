using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Answer
{
    public class MyCameraAnswer : MonoBehaviour
    {
        [Header("Camera Settings")]
        [Range(0, 120f)]
        public float fieldOfView = 30f;
        [Range(1, 1.5f)]
        public float aspectRatio = 1; //width devide by height
        public float near = 0;
        public float far = 0;

        [Header("View Screen Settings")]
        [Range(1, 150)]
        public int resolution = 0; //
        public bool isDrawPixel = false;
        public bool isShowRay = false;

        [Header("Scene Object")]
        public SceneObjectAnswer[] objects;

        private void OnDrawGizmos()
        {
            Vector3 cubeSize = new Vector3(0.1f, 0.1f, 0.2f);
            Vector3 origin = transform.position;
            Gizmos.DrawCube(origin - Vector3.forward * cubeSize.z / 2, cubeSize);
            Gizmos.DrawFrustum(origin, fieldOfView, far, near, aspectRatio);

            //draw a view plane to view image
            Vector3 viewPlanePos = origin + near * Vector3.forward;
            float viewPlaneHeight = near * Mathf.Tan(Mathf.Deg2Rad * fieldOfView / 2) * 2;
            float viewPlaneWidth = viewPlaneHeight * aspectRatio;
            Vector3 viewPlaneSize = new Vector3(viewPlaneWidth, viewPlaneHeight, 0.00001f);
            if (isDrawPixel == false)
            {
                //draw view screen
                Gizmos.color = Color.white;
                Gizmos.DrawCube(viewPlanePos, viewPlaneSize);
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(viewPlanePos, viewPlaneSize);
            }
            else
            {
                float pixelHeight = viewPlaneHeight / resolution;
                float pixelWidth = viewPlaneWidth / (resolution * aspectRatio);

                //draw each pixel
                for (int i = 0; i < resolution * aspectRatio; i++)
                {
                    for (int j = 0; j < resolution; j++)
                    {
                        //calculate pixel pos and size
                        Vector3 pixelPos = new Vector3(pixelWidth / 2 + pixelWidth * i - viewPlaneWidth / 2,
                            pixelHeight / 2 + pixelHeight * j - viewPlaneHeight / 2,
                            viewPlanePos.z);
                        Vector3 pixelSize = new Vector3(pixelWidth, pixelHeight, 0.00001f);

                        //Ray tracing to calculate HitData
                        Vector3 direction = (pixelPos - origin).normalized;
                        MyRay ray = new MyRay(origin, direction);
                        HitData targetData = null;
                        for (int k = 0; k < objects.Length; k++)
                        {
                            HitData data = objects[k].Intersect(ray);
                            if (data != null)
                            {
                                if (targetData == null) targetData = data;
                                else
                                {

                                    if (data.distance < targetData.distance)
                                    {
                                        targetData = data;
                                    }
                                }
                            }
                        }

                        //change color of Pixel
                        if (targetData != null)
                        {
                            Gizmos.color = targetData.color;
                        }
                        else
                        {
                            Gizmos.color = Color.white;
                        }

                        //render
                        if (isShowRay)
                        {
                            //Draw ray
                            ray.Draw();
                            Gizmos.DrawSphere(pixelPos, pixelWidth / 5f);
                        }
                        else
                        {
                            //Draw pixel
                            Gizmos.DrawCube(pixelPos, pixelSize);
                        }
                        Gizmos.color = Color.green;
                        Gizmos.DrawWireCube(pixelPos, pixelSize);
                    }
                }
            }

            //
        }


    }


    public class MyRay
    {
        public Vector3 origin;
        public Vector3 direction;

        public MyRay(Vector3 _origin, Vector3 _direction)
        {
            origin = _origin; direction = _direction;
        }

        public void Draw()
        {
            Gizmos.DrawRay(origin, direction * 5f);
        }

    }

}


