using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace RaceGameBase
{
    public class Camera
    {
        //Members
        public Vector2 m_CameraPosition;
        private float m_Zoom;
        private float m_Rotation;
        private Matrix m_Transform;
 
        public Camera()
        {
            m_Zoom = 1;
            m_Rotation = 0;
            m_CameraPosition = new Vector2(0,0);
        }
 
        #region Set/Get
 
        /// <summary>
        /// Camera Zoom amount
        /// </summary>
        public float Zoom
        {
            get { return m_Zoom; }
            set { m_Zoom = value; if (m_Zoom < 0.1f) m_Zoom = 0.1f; } // Negative zoom will flip image
        }
 
        /// <summary>
        /// Camera Rotation amount
        /// </summary>
        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }
 
        /// <summary>
        /// Moves the camera with the input amount
        /// </summary>
        /// <param name="amount"></param>
        public void Move(Vector2 amount)
        {
            m_CameraPosition += amount;
        }
 
        /// <summary>
        /// Camera Postion
        /// </summary>
        public Vector2 Position
        {
            get { return m_CameraPosition; }
            set { m_CameraPosition = value; }
        }
 
        #endregion
 
        /// <summary>
        /// Updates the cameras transform
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <returns></returns>
        public Matrix Transform(GraphicsDevice graphicsDevice)
        {
            float ViewportWidth = graphicsDevice.Viewport.Width;
            float ViewportHeight = graphicsDevice.Viewport.Height;
 
            m_Transform =
              Matrix.CreateTranslation(new Vector3(-m_CameraPosition.X, -m_CameraPosition.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
                                         Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));
            return m_Transform;
        }

        public static void CameraControlls(Camera Camera)
        {
            KeyboardState keyState = Keyboard.GetState();

            //Camera movement
            if (keyState.IsKeyDown(Keys.D))
                Camera.Move(new Vector2(-1, 0));
            if (keyState.IsKeyDown(Keys.A))
                Camera.Move(new Vector2(1, 0));
            if (keyState.IsKeyDown(Keys.W))
                Camera.Move(new Vector2(0, 1));
            if (keyState.IsKeyDown(Keys.S))
                Camera.Move(new Vector2(0, -1));

            //Camera zoom
            if (keyState.IsKeyDown(Keys.Z))
                Camera.Zoom += 0.1f;
            if (keyState.IsKeyDown(Keys.X))
                Camera.Zoom -= 0.1f;

            //Camera rotation
            if (keyState.IsKeyDown(Keys.C))
                Camera.Rotation += 0.1f;
            if (keyState.IsKeyDown(Keys.V))
                Camera.Rotation -= 0.1f;
        }
    }
}