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

        #endregion

        public InputManager(Keys accel, Keys left, Keys right, Keys fire)
        {
            this.accel = accel;
            this.left = left;
            this.right = right;
            this.fire = fire;

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

            if (keyboardState.IsKeyDown(left))
            {
                comandos.Add(com_left);
            }

            if (keyboardState.IsKeyDown(right))
            {
                comandos.Add(com_right);
            }

            if(keyboardState.IsKeyDown(fire) && oldKeyboadState.IsKeyUp(fire))
            {
                comandos.Add(com_fire);
            }
            #endregion

            oldKeyboadState = keyboardState;
            return comandos;
        }

    }
}
