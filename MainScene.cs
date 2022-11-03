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
		private const int MAX_ASTEROIDS_ONLINE = 5;

		private ComponentResourceManager resources;
		private Player player = null;
		private SpaceShip ship = null;
		private Dictionary<GameObject, PictureBox> gameObjectsMap = new Dictionary<GameObject, PictureBox>();
		private List<GameObject> deletedObjList = new List<GameObject>();
		private int asteroidRespawnTime = 50;
		private int bulletCooldown = 5;
		private int asteroidsCount = 0;

		private PictureBox shipPicture = null;
		private delegate void RotatePic(ref PictureBox pic, float angle, bool isRotationLeft);
		private RotatePic rotatePic;
		private delegate void UpdateVisualObject(ref PictureBox pic);
		private UpdateVisualObject updateVisualObj;
		private Mutex mutexObj;
		private bool isGameOver = false;

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

			Point2D pos = new Point2D(367, 327);
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
			while (!this.isGameOver) {
				mutexObj.WaitOne();
				asteroidRespawnTime--;

				int objectX = 0, objectY = 0;
				short objectRotation = (short)0;
				Random rnd = new Random();

				this.addAsteroid(rnd, ref objectX, ref objectY, ref objectRotation);

				GameObject obj, nestedObj;
				PictureBox pic, nestedPic;
				Point2D objectPos;

				long count = 0;
				foreach (KeyValuePair<GameObject, PictureBox> kvp in this.gameObjectsMap) {
					count++;
					obj = kvp.Key;
					pic = kvp.Value;

					objectPos = obj.getPosition(); 
					objectX = objectPos.getX();
					objectY = objectPos.getY();

					this.OnGameObjectUpdate(obj, rnd);
					this.OnGameObjectMovedOutside(obj, pic.Size.Height, objectX, objectY);

					foreach (KeyValuePair<GameObject, PictureBox> nestedKvp in this.gameObjectsMap) {
						nestedObj = nestedKvp.Key;
						nestedPic = nestedKvp.Value;

						if (obj.Equals(nestedObj)) {
							continue;
                        }

						if (obj.ToString().Equals("SpaceShip") && nestedObj.ToString().Equals("Bullet")
							|| obj.ToString().Equals("Bullet") && nestedObj.ToString().Equals("SpaceShip")) {
							continue;
                        }

						if (Point2D.getDistance(objectPos, nestedObj.getPosition()) > (double)pic.Size.Height) {
							continue;
                        }

						Console.WriteLine(obj.ToString() + " cross " + nestedObj.ToString() + " out");

						if (obj.ToString().Equals("SpaceShip") || nestedObj.ToString().Equals("SpaceShip")) {
							this.isGameOver = true;
							continue;
                        }

						this.deletedObjList.Add(obj);
						this.deletedObjList.Add(nestedKvp.Key);
					}

					pic.Invoke(
						new Action(() => {
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

					if (gameObject.ToString().Equals("Asteroid")) {
						this.asteroidsCount--;
                    }

					if (pic != null) {
						pic.Invoke(new Action(() => { pic.Dispose(); }));
					}
				}

				this.deletedObjList.Clear();
				Thread.Sleep(10);
				mutexObj.ReleaseMutex();
			}

			Application.Exit();
		}

		private void OnGameObjectMovedOutside(GameObject obj, int picHeight, int objectX, int objectY) {
			if (objectY > this.Size.Height - picHeight) {
				if (obj.ToString().Equals("SpaceShip")) {
					obj.setPosition(
						new Point2D(objectX, 0 + picHeight)
					);
				} else {
					this.deletedObjList.Add(obj);
                }
			} else if (objectY < 0 + picHeight) {
				if (obj.ToString().Equals("SpaceShip")) {
					obj.setPosition(
						new Point2D(objectX, this.Size.Height - picHeight)
					);
				} else {
					this.deletedObjList.Add(obj);
                }
			}

			if (objectX > this.Size.Width - picHeight) {
				if (obj.ToString().Equals("SpaceShip")) {
					obj.setPosition(
						new Point2D(0 + picHeight, objectY)
					);
				} else {
					this.deletedObjList.Add(obj);
                }
			} else if (objectX < 0 + picHeight) {
				if (obj.ToString().Equals("SpaceShip")) {
					obj.setPosition(
						new Point2D(this.Size.Width - picHeight, objectY)
					);
				} else {
					this.deletedObjList.Add(obj);
                }
			}
        }

		private void OnGameObjectUpdate(GameObject obj, Random rnd) {
			double objectVelocity = obj.getVelocity();

			if (obj.ToString().Equals("SpaceShip")) {
				if (objectVelocity <= 1f) {
					return;
                }

				obj.setVelocity(objectVelocity - 0.02f);
            }
			
			if (obj.ToString().Equals("Asteroid") && this.asteroidRespawnTime % 25 == 0) {
				obj.setRotation((short)rnd.Next(0, 360));
			}

			obj.moveForwardWithRotation();
        }

		private void addAsteroid(Random rnd, ref int objectX, ref int objectY, ref short objectRotation) {
			if (this.asteroidsCount >= MAX_ASTEROIDS_ONLINE) {
				return;
            }

			if (this.asteroidRespawnTime > 0) {
				this.asteroidRespawnTime--;
				return;
            }

			this.asteroidRespawnTime = 50;

            objectX = rnd.Next(0, this.Size.Width);
			objectY = rnd.Next(0, this.Size.Height);
			objectRotation = (short)rnd.Next(0, 360);

			double distanceToShip = Point2D.getDistance(
				this.ship.getPosition(), new Point2D(objectX, objectY)
			);

			if (distanceToShip > this.shipPicture.Size.Height) {
				Asteroid asteroid = new Asteroid(
					new Point2D(objectX, objectY),
					objectRotation,
					1,
					2
				);
				PictureBox asteroidPicture = new PictureBox {
					Image = (Image)(resources.GetObject("asteroidPicture.Image")),
					Location = new Point(objectX, objectY),
					Size = new Size(49, 38),
					Visible = true,
					Name = "asteroidPicture"
				};

				this.rotatePic.Invoke(
					ref asteroidPicture,
					(float)asteroid.getRotation(),
					false
				);
				this.Invoke(
					new Action(() => {
						this.Controls.Add(asteroidPicture);
					}
				));

				this.gameObjectsMap.Add(asteroid, asteroidPicture);
				this.asteroidsCount++;
            }
        }

		private void OnPlayerKeyStateChange() {
			while (!this.isGameOver) {
				this.mutexObj.WaitOne();
				if (this.ship == null || this.player == null || this.shipPicture == null) {
					continue;
				}

				short shipRotation = this.ship.getRotation();
				int
					shipX = this.ship.getPosition().getX(),
					shipY = this.ship.getPosition().getY();

				if (this.player.isButtonPressed(Keys.Space)) {
					bulletCooldown--;

					if (bulletCooldown <= 0) {
						bulletCooldown = 5;
						this.ship.shoot();

						Bullet bullet = new Bullet(
							new Point2D(shipX, shipY),
							shipRotation,
							5
						);
						PictureBox bulletPic = new PictureBox {
							Image = (Image)(resources.GetObject("bulletPicture.Image")),
							Location = new Point(shipX, shipY),
							Size = new Size(28, 27),
							Visible = true,
							Name = "bulletPicture"
						};

						this.Invoke(
							new Action(() => {
								this.Controls.Add(bulletPic);
							}
						));
						this.gameObjectsMap.Add(bullet, bulletPic);

						this.rotatePic.Invoke(
							ref bulletPic,
							(float)bullet.getRotation(),
							false
						);
					}
				}

				if (this.player.isButtonPressed(Keys.Up)) {
					double shipVelocity = this.ship.getVelocity();
					double newShipVelocity = shipVelocity >= 2.5f ? 2.5f : shipVelocity + 0.05f;

					if (this.player.isButtonPressed(Keys.Right)) {
						this.ship.rotateRight();

						this.rotatePic.Invoke(
							ref this.shipPicture,
							(float)shipRotation + SpaceShip.ROTATION_DEGREE,
							false
						);

						newShipVelocity -= newShipVelocity < 1.05f ? 0f : 0.05f;
					} else if (this.player.isButtonPressed(Keys.Left)) {
						this.ship.rotateLeft();

						this.rotatePic.Invoke(
							ref this.shipPicture,
							-((float)shipRotation + SpaceShip.ROTATION_DEGREE),
							true
						);
						newShipVelocity -= newShipVelocity < 1.05f ? 0f : 0.05f;
					}

					this.ship.setVelocity(newShipVelocity);
				} else if (this.player.isButtonPressed(Keys.Left)) {
					this.ship.rotateLeft();

					this.rotatePic.Invoke(
						ref this.shipPicture,
						-((float)shipRotation + SpaceShip.ROTATION_DEGREE),
						true
					);

					this.ship.setVelocity(1f);
				} else if (this.player.isButtonPressed(Keys.Right)) {
					this.ship.rotateRight();

					this.rotatePic.Invoke(
						ref this.shipPicture,
						(float)shipRotation + SpaceShip.ROTATION_DEGREE,
						false
					);

					this.ship.setVelocity(1f);
				}

				shipX = this.ship.getPosition().getX();
				shipY = this.ship.getPosition().getY();

				if (shipY > this.Size.Height - this.shipPicture.Size.Height) {
					this.ship.setPosition(
						new Point2D(shipX, 0 + this.shipPicture.Size.Height)
					);
				} else if (shipY < 0 + this.shipPicture.Size.Height) {
					this.ship.setPosition(
						new Point2D(shipX, this.Size.Height - this.shipPicture.Size.Height)
					);
				}

				if (shipX > this.Size.Width - this.shipPicture.Size.Height) {
					this.ship.setPosition(
						new Point2D(0 + this.shipPicture.Size.Height, shipY)
					);
				} else if (shipX < 0 + this.shipPicture.Size.Height) {
					this.ship.setPosition(
						new Point2D(this.Size.Width - this.shipPicture.Size.Height, shipY)
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
