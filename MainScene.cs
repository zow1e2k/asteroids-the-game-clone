using asteroids_the_game_clone.GameObjects;
using asteroids_the_game_clone.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace asteroids_the_game_clone {
    public partial class MainScene : Form {
        private ComponentResourceManager resources;
        private Player player = null;
        private SpaceShip ship = null;
        private PictureBox shipPicture = null;

        public MainScene(Player player) {
            this.player = player;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(
                (object sender, KeyEventArgs e) => player.addPressedBtn(e.KeyCode)
            );

            this.KeyUp += new KeyEventHandler(
                (object sender, KeyEventArgs e) => player.removePressedBtn(e.KeyCode)
            );

            Vec2D pos = new Vec2D(367, 327);
            ship = new SpaceShip("kefir ship", 1, pos, 0.0f);

            Thread onPlayerKeyStateChange = new Thread(
                new ThreadStart(this.OnPlayerKeyStateChange)
            );
            onPlayerKeyStateChange.Start();

            InitializeComponent();
        }

        private void SceneLoad(object sender, EventArgs e) { }

        private void OnPlayerKeyStateChange() {
            while (true) {
                if (ship == null || player == null || shipPicture == null) {
                    continue;
                }

                if (player.isButtonPressed(Keys.Up)) {
                    if (player.isButtonPressed(Keys.Right)) {
                        ship.rotateRight();
                        this.rotatePicture(ship.getRotation() + SpaceShip.ROTATION_DEGREE, false);
                    } else if (player.isButtonPressed(Keys.Left)) {
                        ship.rotateLeft();
                        this.rotatePicture(-(ship.getRotation() + SpaceShip.ROTATION_DEGREE), true);
                    }

                    ship.moveUp();
                } else if (player.isButtonPressed(Keys.Left)) {
                    ship.rotateLeft();
                    this.rotatePicture(-(ship.getRotation() + SpaceShip.ROTATION_DEGREE), true);
                } else if (player.isButtonPressed(Keys.Right)) {
                    ship.rotateRight();
                    this.rotatePicture(ship.getRotation() + SpaceShip.ROTATION_DEGREE, false);
                } else if (player.isButtonPressed(Keys.Space)) {
                    ship.shoot();
                }

                this.updatePicture();
                Thread.Sleep(10);
            }
        }

        private void updatePicture() {
            if (InvokeRequired) {
                Invoke(new Action(() => {
                    if (ship == null || this.shipPicture == null) {
                        throw new NullReferenceException();
                    }

                    this.shipPicture.Location = new Point(
                        ship.getPosition().getX(),
                        ship.getPosition().getY()
                    );
                }));
            }
        }

        private void rotatePicture(float angle, bool isRotationLeft) {
            if (InvokeRequired) {
                Invoke(new Action(() => {
                    if (this.ship == null || this.shipPicture == null) {
                        throw new NullReferenceException();
                    }

                    if (isRotationLeft) {
                        angle *= -1;
                    }

                    Image oldImage = this.shipPicture.Image;

                    if (oldImage != null) {
                        oldImage.Dispose();
                    }

                    Image newImage = (Image)(resources.GetObject("shipPicture.Image"));
                    newImage = Visual.RotateImage(newImage, angle);
                    this.shipPicture.Image = newImage;
                    this.shipPicture.Refresh();
                }));
            }
        }
    }
}
