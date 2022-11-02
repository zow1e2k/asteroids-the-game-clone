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
		private Dictionary<GameObject, PictureBox> gameObjectsMap = new Dictionary<GameObject, PictureBox>();
		private List<GameObject> deletedObjList = new List<GameObject>();
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
			gameObjectsMap.Add(ship, shipPicture);
		}

        private void SceneLoad(object sender, EventArgs e) { }

		private void OnEnvironmentUpdate() {
			while (true) {
				mutexObj.WaitOne();

				GameObject obj;
				PictureBox pic;
				int objectX, objectY;

				foreach (KeyValuePair<GameObject, PictureBox> kvp in this.gameObjectsMap) {
					obj = kvp.Key;
					pic = kvp.Value;

					objectX = obj.getPosition().getX();
					objectY = obj.getPosition().getY();

					pic.Invoke(
						new Action(() => {
							if (!obj.Equals(this.ship)) {
								obj.moveForwardWithRotation();
                            }

							if (objectY > this.Size.Height - pic.Size.Height) {
								if (obj.Equals(this.ship)) {
									obj.setPosition(
										new Vec2D(objectX, 0 + pic.Size.Height)
									);
								} else {
									this.deletedObjList.Add(obj);
                                }
							} else if (objectY < 0 + pic.Size.Height) {
								if (obj.Equals(this.ship)) {
									obj.setPosition(
										new Vec2D(objectX, this.Size.Height - pic.Size.Height)
									);
								} else {
									this.deletedObjList.Add(obj);
                                }
							}

							if (objectX > this.Size.Width - pic.Size.Height) {
								if (obj.Equals(this.ship)) {
									obj.setPosition(
										new Vec2D(0 + pic.Size.Height, objectY)
									);
								} else {
									this.deletedObjList.Add(obj);
                                }
							} else if (objectX < 0 + pic.Size.Height) {
								if (obj.Equals(this.ship)) {
									obj.setPosition(
										new Vec2D(this.Size.Width - pic.Size.Height, objectY)
									);
								} else {
									this.deletedObjList.Add(obj);
                                }
							}

							pic.Location = new Point(
								obj.getPosition().getX(),
								obj.getPosition().getY()
							);
						}
					));
				}

				foreach (GameObject gameObject in this.deletedObjList) {
					this.gameObjectsMap.TryGetValue(gameObject, out pic);
					this.gameObjectsMap.Remove(gameObject);
					pic.Invoke(new Action(() => { pic.Dispose(); }));
				}

				this.deletedObjList.Clear();
				Thread.Sleep(10);
				mutexObj.ReleaseMutex();
			}
		}

		private void OnPlayerKeyStateChange() {
			while (true) {
				this.mutexObj.WaitOne();
				if (this.ship == null || this.player == null || this.shipPicture == null) {
					continue;
				}

				short shipRotation = this.ship.getRotation();
				int
					shipX = this.ship.getPosition().getX(),
					shipY = this.ship.getPosition().getY();

				if (this.player.isButtonPressed(Keys.Space)) {
					this.ship.shoot();

					GameObject bullet = new GameObject(
						new Vec2D(shipX, shipY),
						shipRotation,
						5
					);
                    PictureBox bulletPicture = new PictureBox {
                        Image = (Image)(resources.GetObject("bulletPicture.Image")),
                        Location = new Point(shipX, shipY),
                        Size = new Size(28, 27),
                        Visible = true,
						Name = "bulletPicture"
                    };

					this.Invoke(
						new Action(() => {
							this.Controls.Add(bulletPicture);
                        }
					));
                    this.gameObjectsMap.Add(bullet, bulletPicture);

					this.rotatePic.Invoke(
						ref bulletPicture,
						(float)bullet.getRotation(),
						false
					);
				}

				if (this.player.isButtonPressed(Keys.Up)) {
					if (this.player.isButtonPressed(Keys.Right)) {
						this.ship.rotateRight();

						this.rotatePic.Invoke(
							ref this.shipPicture,
							(float)shipRotation + SpaceShip.ROTATION_DEGREE,
							false
						);
					} else if (this.player.isButtonPressed(Keys.Left)) {
						this.ship.rotateLeft();

						this.rotatePic.Invoke(
							ref this.shipPicture,
							-((float)shipRotation + SpaceShip.ROTATION_DEGREE),
							true
						);
					}

					this.ship.moveForwardWithRotation();
				} else if (this.player.isButtonPressed(Keys.Left)) {
					this.ship.rotateLeft();

					this.rotatePic.Invoke(
						ref this.shipPicture,
						-((float)shipRotation + SpaceShip.ROTATION_DEGREE),
						true
					);
				} else if (this.player.isButtonPressed(Keys.Right)) {
					this.ship.rotateRight();

					this.rotatePic.Invoke(
						ref this.shipPicture,
						(float)shipRotation + SpaceShip.ROTATION_DEGREE,
						false
					);
				}

				shipX = this.ship.getPosition().getX();
				shipY = this.ship.getPosition().getY();

				if (shipY > this.Size.Height - this.shipPicture.Size.Height) {
					this.ship.setPosition(
						new Vec2D(shipX, 0 + this.shipPicture.Size.Height)
					);
				} else if (shipY < 0 + this.shipPicture.Size.Height) {
					this.ship.setPosition(
						new Vec2D(shipX, this.Size.Height - this.shipPicture.Size.Height)
					);
				}

				if (shipX > this.Size.Width - this.shipPicture.Size.Height) {
					this.ship.setPosition(
						new Vec2D(0 + this.shipPicture.Size.Height, shipY)
					);
				} else if (shipX < 0 + this.shipPicture.Size.Height) {
					this.ship.setPosition(
						new Vec2D(this.Size.Width - this.shipPicture.Size.Height, shipY)
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
