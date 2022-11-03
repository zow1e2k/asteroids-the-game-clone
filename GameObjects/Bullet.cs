using asteroids_the_game_clone.Utilities;

namespace asteroids_the_game_clone.GameObjects {
    public class Bullet : GameObject {
         public Bullet(Point2D position, short rotation, double velocity)
            : base(position, rotation, velocity) { }

        public override string ToString() => "Bullet";
    }
}
