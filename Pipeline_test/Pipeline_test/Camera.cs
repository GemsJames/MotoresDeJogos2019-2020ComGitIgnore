using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline_test
{
    enum TipoCamera
    {
        Free,
        ThirdPerson
    }

    static class Camera
    {

        //Matrizes World, View, Projection
        static public Matrix View, Projection, World;

        //Posição, direção e target
        static private Vector3 position, positionAnterior, direction, target;

        //Position getter
        static public Vector3 getPosition()
        {
            return position;
        }

        //Velocidade do movimento (translação)
        static private float moveSpeed = 3f;

        //Velocidade da rotação
        static private float rotationSpeed = 0.005f;

        //Orientação da camara
        static private float yaw = 0, pitch = 0;

        //Quantidade de pixeis que o rato se moveu
        static private Vector2 mouseMoved;

        //Matriz de rotação da camara
        static private Matrix cameraRotation;

        //Posição original do rato
        static private MouseState mouseStateOriginal;

        //Posição anterior do rato
        static private MouseState mouseStateAnterior;

        //Estado anterior do teclado
        static KeyboardState keyStateAnterior;

        //Near e far plane
        static public float nearPlane = 0.1f;
        static public float farPlane;

        //RasterizerStates para solid / wireframe
        static RasterizerState rasterizerStateSolid;
        static RasterizerState rasterizerStateWireFrame;
        static public RasterizerState currentRasterizerState;

        //Desenhar normais do terreno
        static public bool drawNormals = false;

        //Tipo de camara (FPS, livre)
        static public TipoCamera tipoCamera;

        //Frustum da camara
        static public BoundingFrustum frustum;


        static public void Initialize(GraphicsDevice graphics)
        {
            tipoCamera = TipoCamera.Free;

            //farPlane = Terrain.altura + (Terrain.altura / 2);

            farPlane = 10000f;

            //Posição inicial da camâra
            position = new Vector3(30, 20, 30);

            //Vector de direção inicial
            direction = new Vector3(0, 0, -1f);

            //Colocar o rato no centro do ecrã
            Mouse.SetPosition(graphics.Viewport.Height / 2, graphics.Viewport.Width / 2);

            //Guardar a posição original do rato
            mouseStateOriginal = Mouse.GetState();

            //Inicializar as matrizes world, view e projection
            World = Matrix.Identity;
            Foward();
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45),
                graphics.Viewport.AspectRatio,
                nearPlane,
                farPlane);

            //Criar e definir os resterizerStates a utilizar para desenhar a geometria
            //SOLID
            rasterizerStateSolid = new RasterizerState();
            rasterizerStateSolid.CullMode = CullMode.None;
            rasterizerStateSolid.MultiSampleAntiAlias = true;
            rasterizerStateSolid.FillMode = FillMode.Solid;
            rasterizerStateSolid.SlopeScaleDepthBias = 0.1f;
            graphics.RasterizerState = rasterizerStateSolid;

            //WIREFRAME
            rasterizerStateWireFrame = new RasterizerState();
            rasterizerStateWireFrame.CullMode = CullMode.None;
            rasterizerStateWireFrame.MultiSampleAntiAlias = true;
            rasterizerStateWireFrame.FillMode = FillMode.WireFrame;

            currentRasterizerState = rasterizerStateSolid;

            frustum = new BoundingFrustum(Matrix.Identity);
        }

        static private void Foward()
        {
            position = position + moveSpeed * direction;
            target = position + direction;
        }

        static private void Backward()
        {
            position = position - moveSpeed * direction;
            target = position + direction;
        }

        static private void Up()
        {
            position = position + moveSpeed * Vector3.Up;
            target = position + direction;
        }

        static private void Down()
        {
            position = position + moveSpeed * Vector3.Down;
            target = position + direction;
        }

        static private void rotateLeftRight()
        {
            yaw -= mouseMoved.X * rotationSpeed;
        }

        static private void rotateUpDown()
        {
            pitch -= mouseMoved.Y * rotationSpeed;
        }


        static private void strafeLeft(GameTime gameTime, float strafe)
        {
            strafe = strafe + moveSpeed * gameTime.ElapsedGameTime.Milliseconds;
            position = position - moveSpeed * Vector3.Cross(direction, Vector3.Up);
            target = position + direction;
        }

        static private void strafeRight(GameTime gameTime, float strafe)
        {
            strafe = strafe + moveSpeed * gameTime.ElapsedGameTime.Milliseconds;
            position = position + moveSpeed * Vector3.Cross(direction, Vector3.Up);
            target = position + direction;
        }

        static public void Update(GameTime gameTime, GraphicsDevice graphics, Ship ship)
        {

            KeyboardState kb = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (tipoCamera == TipoCamera.Free)
            {
                //Controlos do teclado
                if (kb.IsKeyDown(Keys.W))
                {
                    Foward();
                }
                if (kb.IsKeyDown(Keys.S))
                {
                    Backward();

                }
                if (kb.IsKeyDown(Keys.A))
                {
                    strafeLeft(gameTime, moveSpeed / 2);

                }
                if (kb.IsKeyDown(Keys.D))
                {
                    strafeRight(gameTime, moveSpeed / 2);
                }
                if (kb.IsKeyDown(Keys.Q))
                {
                    Up();
                }
                if (kb.IsKeyDown(Keys.E))
                {
                    Down();
                }

                if (mouseState.ScrollWheelValue > mouseStateAnterior.ScrollWheelValue)
                {
                    if (moveSpeed < 2f)
                        moveSpeed += (mouseState.ScrollWheelValue - mouseStateAnterior.ScrollWheelValue)
                            / 10000f;
                }
                if (Mouse.GetState().ScrollWheelValue < mouseStateAnterior.ScrollWheelValue)
                {
                    if (moveSpeed > 0.05f)
                        moveSpeed -= (mouseStateAnterior.ScrollWheelValue - mouseState.ScrollWheelValue)
                            / 10000f;
                }

                //Controlo da rotação com o rato
                if (mouseState != mouseStateOriginal)
                {
                    mouseMoved.X = mouseState.Position.X - mouseStateOriginal.Position.X;
                    mouseMoved.Y = mouseState.Position.Y - mouseStateOriginal.Position.Y;
                    rotateLeftRight();
                    rotateUpDown();
                    try
                    {
                        Mouse.SetPosition(graphics.Viewport.Height / 2, graphics.Viewport.Width / 2);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            }

            if (kb.IsKeyDown(Keys.O) && !keyStateAnterior.IsKeyDown(Keys.O))
            {
                if (graphics.RasterizerState == rasterizerStateSolid)
                {
                    graphics.RasterizerState = rasterizerStateWireFrame;
                    currentRasterizerState = rasterizerStateWireFrame;
                }
                else
                {
                    graphics.RasterizerState = rasterizerStateSolid;
                    currentRasterizerState = rasterizerStateSolid;
                }
            }
            if (kb.IsKeyDown(Keys.C) && !keyStateAnterior.IsKeyDown(Keys.C))
            {
                switch (tipoCamera)
                {
                    case TipoCamera.Free:
                        tipoCamera = TipoCamera.ThirdPerson;
                        break;
                    case TipoCamera.ThirdPerson:
                        tipoCamera = TipoCamera.Free;
                        break;
                    default:
                        tipoCamera = TipoCamera.Free;
                        break;
                }

            }
            if (kb.IsKeyDown(Keys.N) && !keyStateAnterior.IsKeyDown(Keys.N))
            {
                drawNormals = !drawNormals;
            }

            UpdateViewMatrix(ship);
            mouseStateAnterior = mouseState;
            keyStateAnterior = kb;

            positionAnterior = position;
        }

        static private void UpdateViewMatrix(Ship ship = null)
        {


            if (tipoCamera == TipoCamera.Free)
            {
                cameraRotation = Matrix.CreateFromYawPitchRoll(yaw, 0, pitch);
                World = cameraRotation;
                direction = Vector3.Transform(new Vector3(1, 0, 0), cameraRotation);
                target = position + direction;
                View = Matrix.CreateLookAt(position, target, Vector3.Up);
            }

            if (tipoCamera == TipoCamera.ThirdPerson)
            {
                Vector3 scale;
                Quaternion rotation;
                Vector3 shipPosition;
                ship.World.Decompose(out scale, out rotation, out shipPosition);
                Matrix rotationMatrix = Matrix.CreateFromQuaternion(rotation);

                //Vector3 thirdPersonReference = new Vector3(0, 5f, 8f);
                Vector3 thirdPersonReference = new Vector3(0, 25f, 100f);

                Vector3 transformedReference =
                    Vector3.Transform(thirdPersonReference, rotationMatrix);

                Vector3 cameraPosition = transformedReference + ship.Position;

                View = Matrix.CreateLookAt(cameraPosition, ship.Position + rotationMatrix.Forward * 3,
                    Vector3.Cross(rotationMatrix.Left, transformedReference));

                position = cameraPosition;
                direction = ship.Position - position;

            }

            frustum.Matrix = View * Projection;


        }
    }
}

//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Pipeline_test
//{
//    public class Camera
//    {
//        private Matrix view;

//        public Matrix View
//        {
//            get { return view; }
//            set { view = value; }
//        }

//        private Matrix projection;

//        public Matrix Projection
//        {
//            get { return projection; }
//            set { projection = value; }
//        }

//        private Vector3 position;

//        public Vector3 Position
//        {
//            get { return position; }
//            set { position = value; }
//        }

//        public Camera(Vector3 position, GraphicsDeviceManager graphics)
//        {
//            view = Matrix.CreateLookAt(position, Vector3.Forward, Vector3.Up);
//            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(50f), graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight, 1, 5000);
//        }
//    }
//}
