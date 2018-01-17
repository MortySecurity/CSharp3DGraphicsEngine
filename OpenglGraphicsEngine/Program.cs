using System;
using Tao.FreeGlut;
using OpenGL;

namespace OpenglGraphicsEngine
{
    class Program
    {
        private static int width = 1280, height = 720;
        private static ShaderProgram program;
        private static VBO<Vector3> square;
        private static VBO<int> SquareElements;

        static void Main(string[] args)
        {

            displayWindow();
        }


        /*
           * creating the window for the 3d opengl graphics engine
           * 
           * */
        public static void displayWindow()
        {

            Glut.glutInit();

            /*
             * Glut.GLUT_DOUBLE: Double buffer removes any tearing/flickering of the screen-
             *                  -Drawing on the back buffer (video memory)
             * Glut.GLUT_DEPTH: Location and color of the screen. Closest gets drawn.
             * */
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH); 

            Glut.glutInitWindowSize(width, height); // width and height of the engine's window 
            Glut.glutCreateWindow("3D OpenGL Graphics Engine"); // title of the engine


            Glut.glutIdleFunc(OnRenderFrame);
            Glut.glutDisplayFunc(OnDisplay);


            SquareDrawing();
            

            Glut.glutMainLoop();
        }

        public static void SquareDrawing()
        {
            program = new ShaderProgram(VertexShader, FragmentShader);

            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / height, 0.1f, 1000f));
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, new Vector3(0, 1, 0)));

            square = new VBO<Vector3>(new Vector3[] { new Vector3(-1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0) });

            SquareElements = new VBO<int>(new int[] { 0, 1, 2, 3 }, BufferTarget.ElementArrayBuffer);
        }

        private static void OnDisplay()
        {

        }

        private static void OnRenderFrame()
        {

            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            program.Use();

            //draw square
            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(0, 0, 0)));
            Gl.BindBufferToShaderAttribute(square, program, "vertexPosition");
            Gl.BindBuffer(SquareElements);

            Gl.DrawElements(BeginMode.Quads, SquareElements.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            Glut.glutSwapBuffers();
        }

        public static string VertexShader = @"

in vec3 vertexPosition;
uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;
void main(void)
{
    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
}
";

        public static string FragmentShader = @"
#version 130
out vec4 fragment;
void main(void)
{
    fragment = vec4(1, 1, 1, 1);
}
";
    }
}