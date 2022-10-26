using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace asteroids_the_game_clone {
    public class Player {
        private String name;
        private long score;
        private List<Keys> pressedBtns = new List<Keys>();

        public Player(String name) {
            this.name = name;
            this.score = 0;
        }

        public void setName(String name) => this.name = name;
        public String getName() => this.name;

        public void setScore(long score) => this.score = score;
        public long getScore() => this.score;
        public void addScore(long score) => this.score += score;
        public void addPressedBtn(Keys btn) {
            if (this.pressedBtns.IndexOf(btn) != -1) {
                return;
            }

            this.pressedBtns.Add(btn);
        }
        public void removePressedBtn(Keys btn) {
            if (this.pressedBtns.IndexOf(btn) == -1) {
                return;
            }

            this.pressedBtns.Remove(btn);
        }
        public bool isButtonPressed(Keys btn) {
            if (this.pressedBtns.IndexOf(btn) == -1) {
                return false;
            }

            return true;
        }
    }
}
