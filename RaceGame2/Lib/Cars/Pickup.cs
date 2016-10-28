using System.Windows.Forms;

namespace RaceGame2.Lib.Cars
{
    public class Pickup : Car
    {
        /// <summary>
        /// Constructor of the default pickup class
        /// </summary>
        /// <param name="postionx">starting position of the car (Horiz)</param>
        /// <param name="positiony">starting position of the car (Vert)</param>
        /// <param name="rotation">starting rotation of the car (0 for car is pointing left)</param>
        /// <param name="speed">starting speed of the car</param>
        /// <param name="leftKey">the key to steer left</param>
        /// <param name="rightKey">the key to steer right</param>
        /// <param name="throttleKey">the key to throttle</param>
        /// <param name="brakeKey">the key to brake/reverse</param>
        /// <param name="carColour">the colour of the players car</param>
        /// <param name="imageLocation">the image name used to draw the car</param>
        public Pickup(int postionx, int positiony, float rotation, double speed, Keys leftKey, Keys rightKey, Keys throttleKey, Keys brakeKey, string carColour = "black") : base(postionx, positiony, rotation, speed, leftKey, rightKey, throttleKey, brakeKey, carColour)
        {
            this.imageLocation = "pickup.png";
            SetImage(carColour);
        }
    }
}