using asteroids_the_game_clone.Utilities;
using System;

namespace asteroids_the_game_clone.GameObjects {
    public class SpaceShip : GameObject, Interfaces.IDynamicable {
		private String name;
		private int ammo;

		public SpaceShip(String name, Vec2D position, short rotation, short velocity)
			: base(position, rotation, velocity) {
			this.name = name;
		}

		public String getName() => this.name;

		public void setName(String name) => this.name = name;
		public void setAmmo(int ammo) => this.ammo = ammo;
		public int getAmmo() => this.ammo;
		public void addAmmo(int ammo) => this.ammo += ammo;
		public void shoot() => this.ammo--;
	}
}
