using asteroids_the_game_clone.Utilities;
using asteroids_the_game_clone.Interfaces;
using System;

namespace asteroids_the_game_clone.GameObjects {
    public class GameObject : IDynamicable {
		public const int ROTATION_DEGREE = 3;

        private Point2D position;
        private short rotation;
		private double velocity;

        public GameObject(Point2D position, short rotation, double velocity) {
            this.position = new Point2D(position.getCoords().ToArray());
            this.rotation = rotation;
			this.velocity = velocity;
        }

        public void setPosition(Point2D position) => this.position = new Point2D(position.getCoords().ToArray());
		public Point2D getPosition() => new Point2D(
			this.position.getCoords().ToArray()
		);
		public void setRotation(short rotation) => this.rotation = rotation;
		public short getRotation() => this.rotation;

		public void setVelocity(double velocity) => this.velocity = velocity;
		public double getVelocity() => this.velocity;

        public void moveTo(Point2D vec) => this.position = new Point2D(
			vec.getX(), vec.getY()
		);

		public void moveUp() => this.position.setY(
			this.position.getY() - (int)Math.Round(this.velocity)
		);

		public void moveRight() => this.position.setX(
			this.position.getX() + (int)Math.Round(this.velocity)
		);

		public void moveLeft() => this.position.setX(
			this.position.getX() - (int)Math.Round(this.velocity)
		);

		public void moveDown() => this.position.setY(
			this.position.getY() + (int)Math.Round(this.velocity)
		);

		public void rotateRight() => this.rotate(ROTATION_DEGREE);

		public void rotateLeft() => this.rotate(-ROTATION_DEGREE);
		public void rotate(float angle) {
			if ((float)this.rotation + angle >= 360.0f) {
				this.rotation = 0;
				return;
			}
			
			if ((float)this.rotation + angle < 0.0f) {
				this.rotation = (short)(360 + angle);
				return;
			}

			this.rotation += (short)angle;
		}

		public void moveForwardWithRotation() {
			double
				x = this.position.getX(),
				y = this.position.getY();

			if (this.rotation >= 0.0f && this.rotation < 90.0f) {
				if (this.rotation >= 1) {
					x += this.velocity;
				}

				y -= this.velocity;
			} else if (this.rotation >= 90.0f && this.rotation < 180.0f) {
				if (this.rotation >= 91.0f) {
					y += this.velocity;
				}

				x += this.velocity;
			} else if (this.rotation >= 180.0f && this.rotation < 270.0f) {
				if (this.rotation >= 181.0f) {
					x -= this.velocity;
				}

				y += this.velocity;
			} else if (this.rotation >= 270.0f && this.rotation <= 360.0f) {
				if (this.rotation >= 271.0f) {
					y -= this.velocity;
				}

				x -= this.velocity;
			}

			Point2D vec = new Point2D((int)Math.Round(x), (int)Math.Round(y));
			this.moveTo(vec);
			return;
		}

        public override string ToString() => "GameObject";
    }
}
