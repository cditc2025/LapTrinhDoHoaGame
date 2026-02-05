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
        public MyMesh[] objects;

        float[,] depthBuffer;
        Color[,] frame;
        int width = 0;
        int height = 0;

        private void OnDrawGizmos()
        {
            Vector3 cubeSize = new Vector3(0.1f, 0.1f, 0.2f);
            Vector3 origin = Vector3.zero;

            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            Gizmos.DrawCube(origin - Vector3.forward * cubeSize.z / 2, cubeSize);
            Gizmos.DrawFrustum(origin, fieldOfView, far, near, aspectRatio);
            Gizmos.matrix = Matrix4x4.identity;

            //draw a view plane to view image
            Vector3 viewPlanePos = origin + near * Vector3.forward;
            float viewPlaneHeight = near * Mathf.Tan(Mathf.Deg2Rad * fieldOfView / 2) * 2;
            float viewPlaneWidth = viewPlaneHeight * aspectRatio;
            Vector3 viewPlaneSize = new Vector3(viewPlaneWidth, viewPlaneHeight, 0.00001f);

            //convert all mesh to triangle
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
                width = (int)(resolution * aspectRatio);
                height = resolution;

                //primary buffer
                frame = new Color[width, height];
                //depth buffer
                depthBuffer = new float[width, height];
                ClearBuffer();

                //draw triangle on frame
                for (int i = 0; i < objects.Length; i++)
                {
                    Triangle[] triangles = objects[i].ToTriangles();
                    for (int j = 0; j < triangles.Length; j++)
                    {
                        DrawTriangle(triangles[j], objects[i]);
                    }
                }

                //draw each pixel
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        //calculate pixel pos and size
                        Vector3 pixelPos = new Vector3(pixelWidth / 2 + pixelWidth * i - viewPlaneWidth / 2,
                            pixelHeight / 2 + pixelHeight * j - viewPlaneHeight / 2,
                            viewPlanePos.z);
                        Vector3 pixelSize = new Vector3(pixelWidth, pixelHeight, 0.00001f);

                        //change color of Pixel
                        Gizmos.color = frame[i, j];

                        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
                        //render
                        if (isShowRay)
                        {
                            //Draw ray
                            Gizmos.DrawSphere(pixelPos, pixelWidth / 5f);
                            if (frame[i, j] != Color.white) Gizmos.DrawRay(origin, (pixelPos - origin).normalized * 5f);
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

        void ClearBuffer()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    frame[i, j] = Color.white;
                    depthBuffer[i, j] = Mathf.Infinity;
                }
            }
        }

        void DrawTriangle(Triangle triangle, MyMesh mesh)
        {
            //convert vertex from local coordinate to pixel coordinate

            //calculate bound of triangle

            //draw inside triangle
            for (int i = 0; i <= 0; i++)
            {
                for (int j = 0; j <= 0; j++)
                {
                    //calculate barycentric coordinate

                    //draw pixel color if pixel is inside triangle

                }
            }
        }



        Vector3 GetBarycentricCoordinate(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
        {
            //area = |a||b|sin(a,b)/2
           
            return new Vector3();
        }

        Vector3 VertexToPixel(Matrix4x4 mvp, Vector3 point)
        {
            Vector4 point4D = new Vector4(point.x, point.y, point.z, 1);
            Vector4 pointRes = point4D;

            return GetPixel(pointRes);
        }

        Vector3 GetPixel(Vector4 coord)
        {
            //calculate pixel coordinate from projection coord
            float x = 0;
            float y = 0;
            float z = 0;
            return new Vector3(x, y, z);
        }

        Matrix4x4 ModelViewProjectionMatrix(MyMesh mesh)
        {
            //calculate Model Matrix = Translate * Rotate * Scale
            Matrix4x4 modelMatrix = Matrix4x4.zero;

            //calculate View Matrix = cameraTRS ^-1
            Matrix4x4 viewMatrix = Matrix4x4.zero;

            //calculate Projection Matrix (n: near, f: far, t: top, b: bottom, r: right, l: left)
            // 2n/(r - l)    0      (r + l)/(r - l)      0
            //     0     2n/(t - b) (t + b)/(t - b)      0
            //     0         0      (f + n)/(n - f) 2nf/(n - f)
            //     0         0            -1             0

            Matrix4x4 projectionMatrix = Matrix4x4.zero;

            Matrix4x4 result = projectionMatrix * viewMatrix * modelMatrix;

            return result;
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


