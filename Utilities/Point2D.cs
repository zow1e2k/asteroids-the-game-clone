using System;
using System.Collections.Generic;

namespace asteroids_the_game_clone.Utilities {
    public class Point2D {
        private const sbyte MAX_VECTOR_DIRECTIONS = 2;
        private int x, y;
        public Point2D(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Point2D(int[] arr) {
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

        public static double getDistance(Point2D p1, Point2D p2) {
            double ac = p2.x - p1.x;
            double bc = p2.y - p1.y;

            return Math.Sqrt(Math.Pow(ac, 2) + Math.Pow(bc, 2));
        }
    }
}
