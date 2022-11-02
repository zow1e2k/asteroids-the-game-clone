using asteroids_the_game_clone.Utilities;
using asteroids_the_game_clone.Interfaces;

namespace asteroids_the_game_clone.GameObjects {
    public class GameObject : IDynamicable {
		public const int ROTATION_DEGREE = 3;

        private Vec2D position;
        private short rotation;
		private short velocity;

        public GameObject(Vec2D position, short rotation, short velocity) {
            this.position = new Vec2D(position.getCoords().ToArray());
            this.rotation = rotation;
			this.velocity = velocity;
        }

        public void setPosition(Vec2D position) => this.position = new Vec2D(position.getCoords().ToArray());
		public Vec2D getPosition() => new Vec2D(
			this.position.getCoords().ToArray()
		);
		public void setRotation(short rotation) => this.rotation = rotation;
		public short getRotation() => this.rotation;

		public void setVelocity(short velocity) => this.velocity = velocity;
		public short getVelocity() => this.velocity;

        public void moveTo(Vec2D vec) => this.position = new Vec2D(
			vec.getX(), vec.getY()
		);

		public void moveUp() => this.position.setY(
			this.position.getY() - this.velocity
		);

		public void moveRight() => this.position.setX(
			this.position.getX() + this.velocity
		);

		public void moveLeft() => this.position.setX(
			this.position.getX() - this.velocity
		);

		public void moveDown() => this.position.setY(
			this.position.getY() + this.velocity
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

			Vec2D vec = new Vec2D((int)x, (int)y);
			this.moveTo(vec);
			return;
		}
    }
}
