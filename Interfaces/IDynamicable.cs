using asteroids_the_game_clone.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asteroids_the_game_clone.Interfaces {
    public interface IDynamicable {
        void moveTo(Point2D vec);
        void moveUp();
        void moveRight();
        void moveLeft();
        void moveDown();
        void rotateRight();
        void rotateLeft();
        void rotate(float angle);
        void moveForwardWithRotation();
    }
}
