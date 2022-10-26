using asteroids_the_game_clone.Utilities;
using System;

namespace asteroids_the_game_clone.GameObjects {
    public class SpaceShip : Interfaces.IDynamicable {
		public const int ROTATION_DEGREE = 3;

		private String name;
		private short velocity;
		private int rotation;
		private Vec2D position;
		private int ammo;

		public SpaceShip(String name, Vec2D position) {
			this.name = name;
			this.position = position;
		}

		public SpaceShip(String name, Vec2D position, short velocity)
			: this(name, position) => this.velocity = velocity;

		public SpaceShip(String name, short velocity, Vec2D position, int rotation)
			: this(name, position, velocity) => this.rotation = rotation;

		public SpaceShip(String name, Vec2D position, int rotation)
			: this(name, position) => this.rotation = rotation;

		public void setRotation(int rotation) => this.rotation = rotation;

		public float getRotation() => this.rotation;

		public Vec2D getPosition() => new Vec2D(
			this.position.getCoords().ToArray()
		);

		public String getName() => this.name;

		public void setName(String name) => this.name = name;

		public short getVelocity() => this.velocity;

		public void setVelocity(short velocity) => this.velocity = velocity;

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
				this.rotation = 360 + (int)angle;
				return;
			}

			this.rotation += (int)angle;
		}
		public void setAmmo(int ammo) => this.ammo = ammo;
		public int getAmmo() => this.ammo;
		public void addAmmo(int ammo) => this.ammo += ammo;
		public void shoot() => this.ammo--;

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
