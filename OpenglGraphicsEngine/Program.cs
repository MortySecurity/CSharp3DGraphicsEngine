using System;
using Tao.FreeGlut;
using OpenGL;

namespace OpenglGraphicsEngine
{
    class Program
    {
        private static int width = 1280, height = 720;

        static void Main(string[] args)
        {

            displayWindow();
        }

        /*
           * creating the window for the 3d opengl engine
           * */
        public static void displayWindow()
        {

            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height); // width and height of the engine's window 
            Glut.glutCreateWindow("3D OpenGL Graphics Engine"); // title of the engine

            Glut.glutIdleFunc(OnRenderFrame);
            Glut.glutDisplayFunc(OnDisplay);

            Glut.glutMainLoop();

        }

        private static void OnDisplay()
        {

        }

        private static void OnRenderFrame()
        {

            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Glut.glutSwapBuffers();
        }
    }
}