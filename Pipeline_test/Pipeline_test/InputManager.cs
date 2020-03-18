using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pipeline_test.Messages;
using Pipeline_test.Commands;
using Microsoft.Xna.Framework.Input;

namespace Pipeline_test
{
    class InputManager
    {
        #region Vars
        #region Keys
        private Keys accel;

        public Keys Accel
        {
            get { return accel; }
            set { accel = value; }
        }

        private Keys left;

        public Keys Left
        {
            get { return left; }
            set { left = value; }
        }

        private Keys right;

        public Keys Right
        {
            get { return right; }
            set { right = value; }
        }

        private Keys fire;

        public Keys Fire
        {
            get { return fire; }
            set { fire = value; }
        }

        private Keys faceDown;

        public Keys FaceDown
        {
            get { return faceDown; }
            set { faceDown = value; }
        }

        private Keys faceUp;

        public Keys FaceUp
        {
            get { return faceUp; }
            set { faceUp = value; }
        }

        private Keys deAccel;

        public Keys DeAccel
        {
            get { return deAccel; }
            set { deAccel = value; }
        }
        //load and save

        private Keys save;

        public Keys Save
        {
            get { return save; }
            set { save = value; }
        }

        private Keys load;

        public Keys Load
        {
            get { return load; }
            set { load = value; }
        }

        #endregion

        private List<Command> comandos;

        public List<Command> Comandos
        {
            get { return comandos; }
            set { comandos = value; }
        }

        private KeyboardState keyboardState;
        private KeyboardState oldKeyboadState;

        private Command com_accel = new Accelarate();
        private Command com_left = new TurnLeft();
        private Command com_right = new TurnRight();
        private Command com_fire = new FireRocket();
        private Command com_faceUp = new FaceUp();
        private Command com_faceDown = new FaceDown();
        private Command com_deAccel = new DeAccelarate();
        private Command com_save = new Save();
        private Command com_load = new Load();

        #endregion

        public InputManager(Keys accel, Keys left, Keys right, Keys fire, Keys faceUp, Keys faceDown, Keys deAccel, Keys save, Keys load)
        {
            this.accel = accel;
            this.left = left;
            this.right = right;
            this.fire = fire;
            this.faceUp = faceUp;
            this.faceDown = faceDown;
            this.deAccel = deAccel;
            this.save = save;
            this.load = load;

            oldKeyboadState = Keyboard.GetState();

            comandos = new List<Command>(50);
        }

        public List<Command> UpdateInputManager()
        {
            keyboardState = Keyboard.GetState();
            comandos.Clear();

            #region ifsTodos
            if (keyboardState.IsKeyDown(accel))
            {
                comandos.Add(com_accel);
            }

            if (keyboardState.IsKeyDown(deAccel))
            {
                comandos.Add(com_deAccel);
            }

            if (keyboardState.IsKeyDown(left))
            {
                comandos.Add(com_left);
            }

            if (keyboardState.IsKeyDown(right))
            {
                comandos.Add(com_right);
            }

            if (keyboardState.IsKeyDown(faceUp))
            {
                comandos.Add(com_faceUp);
            }

            if (keyboardState.IsKeyDown(faceDown))
            {
                comandos.Add(com_faceDown);
            }

            if (keyboardState.IsKeyDown(fire) && oldKeyboadState.IsKeyUp(fire))
            {
                comandos.Add(com_fire);
            }

            if (keyboardState.IsKeyDown(save))
            {
                comandos.Add(com_save);
            }
            if (keyboardState.IsKeyDown(load))
            {
                comandos.Add(com_load);
            }
            #endregion

            oldKeyboadState = keyboardState;
            return comandos;
        }

    }
}
