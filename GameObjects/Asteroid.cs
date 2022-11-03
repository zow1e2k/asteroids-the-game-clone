using asteroids_the_game_clone.Utilities;

namespace asteroids_the_game_clone.GameObjects {
    public class Asteroid : GameObject {
        private int size;
        public Asteroid(Point2D position, short rotation, double velocity, int size)
            : base(position, rotation, velocity) => this.size = size;

        public int getSize() => this.size;
        public int setSize(int size) => this.size = size;

        public override string ToString() => "Asteroid";
    }
}
