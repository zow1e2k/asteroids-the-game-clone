using asteroids_the_game_clone.Utilities;
using System;

namespace asteroids_the_game_clone.GameObjects {
    public class SpaceShip {
		public const float ROTATION_DEGREE = 3.0f;

		private String name;
		private short velocity;
		private float rotation;
		private Vec2D position;

		public SpaceShip(String name, Vec2D position) {
			this.name = name;
			this.position = position;
		}

		public SpaceShip(String name, Vec2D position, short velocity)
			: this(name, position) => this.velocity = velocity;

		public SpaceShip(String name, short velocity, Vec2D position, float rotation)
			: this(name, position, velocity) => this.rotation = rotation;

		public SpaceShip(String name, Vec2D position, float rotation)
			: this(name, position) => this.rotation = rotation;

		public void setRotation(float rotation) => this.rotation = rotation;

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

		public void rotate(float angle) => this.rotation += angle;
	}
}
