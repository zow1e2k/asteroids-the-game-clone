using System;
using System.Drawing;

namespace asteroids_the_game_clone.Utilities {
    public static class Visual {
        public static Bitmap RotateImage(Image image, float angle) {
            return RotateImage(image, new PointF((float)image.Width / 2, (float)image.Height / 2), angle);
        }

		public static Bitmap RotateImage(Image image, PointF offset, float angle) {
			if (image == null) {
				throw new ArgumentNullException("image");
			}

			//create a new empty bitmap to hold rotated image

			Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);

			rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			//make a graphics object from the empty bitmap
			Graphics g = Graphics.FromImage(rotatedBmp);

			//Put the rotation point in the center of the image

			g.TranslateTransform(offset.X, offset.Y);

			//rotate the image
			g.RotateTransform(angle);

			//move the image back
			g.TranslateTransform(-offset.X, -offset.Y);
			//draw passed in image onto 
			g.DrawImage(image, new PointF(0, 0));

			return rotatedBmp;
		}
	}
}
