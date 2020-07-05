using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class Test
    {
        //// A Test behaves as an ordinary method
        //[Test]
        //public void TestSimplePasses()
        //{
        //    // Use the Assert class to test conditions
        //}

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestA_LoadMenuScene()
        {
            GameObject gameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("MenuScene"));
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            Assert.IsTrue(gameObject.scene.isLoaded);
            yield return new WaitForSeconds(3);
        }

        [UnityTest]
        public IEnumerator TestB_PlayButtonClicked()
        {
            var playButtonGameObject = GameObject.Find("PlayButton");
            var playButton = playButtonGameObject.GetComponent<Button>();
            Assert.NotNull(playButton);
            playClicked = false;
            playButton.onClick.AddListener(playButtonClicked);
            playButton.onClick.Invoke();
            Assert.True(playClicked);

            GameObject gamePlay =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Gameplay"));
            Assert.True(gamePlay.scene.isLoaded);
            yield return new WaitForSeconds(5);
        }

        // play button check
        private bool playClicked;
        private void playButtonClicked()
        {
            playClicked = true;
            SceneManager.LoadScene("Game");
        }

        [UnityTest]
        public IEnumerator TestC_PlayerMovement()
        {
            var playerGameObject = GameObject.Find("Player");
            Assert.NotNull(playerGameObject);

            float initialPosition = playerGameObject.transform.position.x;
            yield return new WaitForSeconds(5);

            Assert.AreNotEqual(playerGameObject.transform.position.x, initialPosition);
        }

        [UnityTest]
        public IEnumerator TestE_PlayerCollider()
        {
            var playerGameObject = GameObject.Find("Player");
            var playerCollide = playerGameObject.GetComponent<PlayerMotor>();
            yield return new WaitForSeconds(5);

            Assert.IsTrue(playerCollide.playerCollideTrue());
        }

        [UnityTest]
        public IEnumerator TestF_DeathMenu()
        {
            var deathMenuObject = GameObject.Find("DeathMenu");
            yield return new WaitForSeconds(2);
            Assert.IsTrue(deathMenuObject.scene.isLoaded);
        }

        [UnityTest]
        public IEnumerator TestD_SpawningLanes()
        {
            var laneGameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("LaneManager"));
            var laneSpawn = laneGameObject.GetComponent<LaneManager>();
            yield return new WaitForSeconds(2);

            Assert.IsTrue(laneSpawn.laneSpawnedTrue());
        }

        // menu button check
        private bool menuClicked;
        private void menuButtonClicked()
        {
            menuClicked = true;
            SceneManager.LoadScene("Menu");
        }

        [UnityTest]
        public IEnumerator TestG_MenuButton()
        {
            var menuButtonObject = GameObject.Find("MenuButton");
            var menuButton = menuButtonObject.GetComponent<Button>();
            Assert.NotNull(menuButton);
            menuClicked = false;
            menuButton.onClick.AddListener(menuButtonClicked);
            menuButton.onClick.Invoke();
            Assert.True(menuClicked);

            GameObject gameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("MenuScene"));
            Assert.True(gameObject.scene.isLoaded);

            yield return new WaitForSeconds(5);
        }
    }
}
