using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Practice
{
    public class MyCamera : MonoBehaviour
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
        public SceneObject[] objects;

        private void OnDrawGizmos()
        {
            Vector3 cubeSize = new Vector3(0.1f, 0.1f, 0.2f);
            Vector3 origin = transform.position;
            Gizmos.DrawCube(origin - Vector3.forward * cubeSize.z / 2, cubeSize);
            Gizmos.DrawFrustum(origin, fieldOfView, far, near, aspectRatio);

            //Calculate view pos and size

            if (isDrawPixel == false)
            {
                //draw a view plane to view image
 
            }
            else
            {
                //calculate pixel width and height 

                //draw each pixel
                for (int i = 0; i < 0; i++)
                {
                    for (int j = 0; j < 0; j++)
                    {
                        //calculate pixel pos and size
                        

                        //Ray tracing to calculate HitData
            

                        //Change color of Pixel with HitData

                        //render pixel or ray
                        if (isShowRay)
                        {
                            //Draw ray

                        }
                        else
                        {
                            //Draw pixel color

                        }

                        //draw pixel wire frame
                    }
                }
            }
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


