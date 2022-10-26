using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asteroids_the_game_clone.Utilities {
    public class Vec2D {
        private const sbyte MAX_VECTOR_DIRECTIONS = 2;
        private int x, y;
        public Vec2D(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Vec2D(int[] arr) {
            if (arr.Length == 0 || arr.Length > MAX_VECTOR_DIRECTIONS) {
                return;
            }

            this.x = arr[0];
            this.y = arr[1];
        }

        public void setX(int x) => this.x = x;
        public void setY(int y) => this.y = y;
        public void setCoords(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public List<int> getCoords() => new List<int>() {
            this.x,
            this.y
        };

        public int getX() => this.x;

        public int getY() => this.y;
    }
}
