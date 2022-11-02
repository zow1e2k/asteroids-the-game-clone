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

		//private SpaceShip ship = null;

		private SpaceShip ship = null;
		//private List<GameObject> bullets = new List<GameObject>();
		private Dictionary<GameObject, PictureBox> gameObjectsList = new Dictionary<GameObject, PictureBox>();
		//private SortedList<GameObject, PictureBox> bullets = new SortedList<GameObject, PictureBox>();

		private PictureBox shipPicture = null;
		private delegate void RotatePic(ref PictureBox pic, float angle, bool isRotationLeft);
		private RotatePic rotatePic;
		private delegate void UpdateVisualObject(ref PictureBox pic);
		private UpdateVisualObject updateVisualObj;
		private Mutex mutexObj;

		public MainScene(Player player) {
			mutexObj = new Mutex();

			rotatePic = rotatePicture;
			updateVisualObj = updateVisualObject;

			this.player = player;
			this.KeyPreview = true;

			this.KeyDown += new KeyEventHandler(
				(object sender, KeyEventArgs e) => player.addPressedBtn(e.KeyCode)
			);

			this.KeyUp += new KeyEventHandler(
				(object sender, KeyEventArgs e) => player.removePressedBtn(e.KeyCode)
			);

			Vec2D pos = new Vec2D(367, 327);
			ship = new SpaceShip("kefir ship", pos, 0, 1);

			Thread onPlayerKeyStateChange = new Thread(
				new ThreadStart(this.OnPlayerKeyStateChange)
			);
			onPlayerKeyStateChange.Start();

			Thread onEnvirontmentUpdate = new Thread(
				new ThreadStart(this.OnEnvironmentUpdate)
			);
			onEnvirontmentUpdate.Start();

			InitializeComponent();
			gameObjectsList.Add(ship, shipPicture);
		}

        private void SceneLoad(object sender, EventArgs e) { }

		private void OnEnvironmentUpdate() {
			while (true) {
				mutexObj.WaitOne();

				foreach (KeyValuePair<GameObject, PictureBox> kvp in gameObjectsList) {
					if (InvokeRequired) {
						kvp.Value.Invoke(
							new Action(() => {
								if (!kvp.Key.Equals(this.ship)) {
									kvp.Key.moveForwardWithRotation();
                                }

								kvp.Value.Location = new Point(
									kvp.Key.getPosition().getX(),
									kvp.Key.getPosition().getY()
								);
							}
						));
					} else {
						kvp.Value.Location = new Point(
							kvp.Key.getPosition().getX(),
							kvp.Key.getPosition().getY()
						);
                    }
				}

				Thread.Sleep(10);
				mutexObj.ReleaseMutex();
			}
		}

		private void OnPlayerKeyStateChange() {
			while (true) {
				mutexObj.WaitOne();
				if (ship == null || player == null || shipPicture == null) {
					continue;
				}

				if (player.isButtonPressed(Keys.Space)) {
					ship.shoot();

					GameObject bullet = new GameObject(
						ship.getPosition(),
						ship.getRotation(),
						5
					);
                    PictureBox bulletPicture = new PictureBox {
                        Image = (Image)(resources.GetObject("bulletPicture.Image")),
                        Location = new Point(
							ship.getPosition().getX(),
							ship.getPosition().getY()
						),
                        Size = new Size(28, 27),
                        Visible = true,
						Name = "bulletPicture"
                    };

					this.Invoke(
						new Action(() => {
							this.Controls.Add(bulletPicture);
                        }
					));
                    gameObjectsList.Add(bullet, bulletPicture);

					rotatePic.Invoke(
						ref bulletPicture,
						(float)bullet.getRotation(),
						false
					);
				}

				if (player.isButtonPressed(Keys.Up)) {
					if (player.isButtonPressed(Keys.Right)) {
						ship.rotateRight();

						rotatePic.Invoke(
							ref this.shipPicture,
							(float)ship.getRotation() + SpaceShip.ROTATION_DEGREE,
							false
						);
					} else if (player.isButtonPressed(Keys.Left)) {
						ship.rotateLeft();

						rotatePic.Invoke(
							ref this.shipPicture,
							-((float)ship.getRotation() + SpaceShip.ROTATION_DEGREE),
							true
						);
					}

					ship.moveForwardWithRotation();
				} else if (player.isButtonPressed(Keys.Left)) {
					ship.rotateLeft();

					rotatePic.Invoke(
						ref this.shipPicture,
						-((float)ship.getRotation() + SpaceShip.ROTATION_DEGREE),
						true
					);
				} else if (player.isButtonPressed(Keys.Right)) {
					ship.rotateRight();

					rotatePic.Invoke(
						ref this.shipPicture,
						(float)ship.getRotation() + SpaceShip.ROTATION_DEGREE,
						false
					);
				}

				Thread.Sleep(10);
				mutexObj.ReleaseMutex();
			}
		}

		private void updateVisualObject(ref PictureBox pic) {
            if (pic == null) {
				throw new NullReferenceException();
			}
			
			pic.Location = new Point(30, 30);
			return;
        }

		private void rotatePicture(ref PictureBox pic, float angle, bool isRotationLeft) {
			if (pic == null) {
				throw new NullReferenceException();
			}

			if (isRotationLeft) {
				angle *= -1;
			}

			Image oldImage = pic.Image;

			if (oldImage != null) {
				oldImage.Dispose();
			}

			Image newImage = (Image)(resources.GetObject(pic.Name + ".Image"));
			newImage = Visual.RotateImage(newImage, angle);
			pic.Image = newImage;
			return;
		}
    }
}
