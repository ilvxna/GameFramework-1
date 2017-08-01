﻿//----------------------------------------------
// Flip Web Apps: Game Framework
// Copyright © 2016 Flip Web Apps / Mark Hewitt
//
// Please direct any bugs/comments/suggestions to http://www.flipwebapps.com
// 
// The copyright owner grants to the end user a non-exclusive, worldwide, and perpetual license to this Asset
// to integrate only as incorporated and embedded components of electronic games and interactive media and 
// distribute such electronic game and interactive media. End user may modify Assets. End user may otherwise 
// not reproduce, distribute, sublicense, rent, lease or lend the Assets. It is emphasized that the end 
// user shall not be entitled to distribute or transfer in any way (including, without, limitation by way of 
// sublicense) the Assets in any other way than as integrated components of electronic games and interactive media. 

// The above copyright notice and this permission notice must not be removed from any files.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//----------------------------------------------

#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
#else
using System;
using GameFramework.GameStructure.GameItems.ObjectModel;
using GameFramework.GameStructure.Players.ObjectModel;
using GameFramework.Localisation.ObjectModel;
using GameFramework.Messaging;
using NUnit.Framework;
using UnityEngine;
using GameFramework.GameStructure.Game.ObjectModel;

namespace GameFramework.GameStructure.GameItems
{
    /// <summary>
    /// Test cases for GameItems. You can also view these to see how you might use the API.
    /// </summary>
    public class GameItemTests
    {

        #region Helper functions for verifying testing

        internal static void SetCommonPreferences(int playerNumber, int number, string identifierBasePrefs, int highScore, bool isUnlocked, bool isUnlockedAnimationShown, bool isBought)
        {
            PlayerPrefs.SetInt(string.Format("P{0}.{1}{2}.HS", playerNumber, identifierBasePrefs, number), highScore);
            PlayerPrefs.SetInt(string.Format("P{0}.{1}{2}.IsU", playerNumber, identifierBasePrefs, number), isUnlocked ? 1 : 0);
            PlayerPrefs.SetInt(string.Format("P{0}.{1}{2}.IsUAS", playerNumber, identifierBasePrefs, number), isUnlockedAnimationShown ? 1 : 0);
            PlayerPrefs.SetInt(string.Format("{0}{1}.IsB", identifierBasePrefs, number), isBought ? 1 : 0);
        }

        internal static void AssertCommonPreferences(int playerNumber, int number, string identifierBasePrefs, GameItem gameItem)
        {
            Assert.AreEqual(PlayerPrefs.GetInt(string.Format("P{0}.{1}{2}.HS", playerNumber, identifierBasePrefs, number), 0), gameItem.HighScore, "HighScore not set correctly");
            // note for the below, unlocked is changed by setting bought - we should have a test for this somewhere... 
            Assert.AreEqual(PlayerPrefs.GetInt(string.Format("P{0}.{1}{2}.IsU", playerNumber, identifierBasePrefs, number), 0) == 1, gameItem.IsUnlocked, "IsUnlocked not set correctly when not bought");
            Assert.AreEqual(PlayerPrefs.GetInt(string.Format("P{0}.{1}{2}.IsUAS", playerNumber, identifierBasePrefs, number), 0) == 1, gameItem.IsUnlockedAnimationShown, "IsUnlockedAnimationShown not set correctly");
            Assert.AreEqual(PlayerPrefs.GetInt(string.Format("{0}{1}.IsB", identifierBasePrefs, number), 0) == 1, gameItem.IsBought, "IsBought not set correctly");
        }

        #endregion Helper functions for verifying testing

        #region Initialisation

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void BasicInitialisationDefaults(int number)
        {
            //// Arrange
            PlayerPrefs.DeleteAll();
            var gameConfiguration = ScriptableObject.CreateInstance<GameConfiguration>();
            var messenger = new Messenger();
            var player = ScriptableObject.CreateInstance<Player>();
            player.Initialise(gameConfiguration, null, messenger, 1);

            //// Act
            var gameItem = ScriptableObject.CreateInstance<GameItem>();
            gameItem.Initialise(gameConfiguration, player, messenger, number);

            //// Assert
            Assert.IsNotNull(gameItem, "GameItem not setup.");
            Assert.AreEqual(number, gameItem.Number, "Number not set correctly");
            //TODO: Verify if we can test the below, or if localisation setup will interfere?
            //Assert.AreEqual("Name", gameItem.Name, "Name not set correctly");
            //Assert.AreEqual("Desc", gameItem.Description, "Description not set correctly");
            Assert.AreEqual("", gameItem.IdentifierBase, "IdentifierBase not set correctly");
            Assert.AreEqual("", gameItem.IdentifierBasePrefs, "IdentifierBasePrefs not set correctly");
            Assert.AreEqual(0, gameItem.Score, "Score not set correctly");
            Assert.AreEqual(0, gameItem.Coins, "Coins not set correctly");
            Assert.AreEqual(0, gameItem.HighScore, "HighScore not set correctly");
            Assert.AreEqual(false, gameItem.IsBought, "IsBought not set correctly");
            Assert.AreEqual(false, gameItem.IsUnlocked, "IsUnlocked not set correctly");
            Assert.AreEqual(false, gameItem.IsUnlockedAnimationShown, "IsUnlockedAnimationShown not set correctly");
        }


        [TestCase(1, "Name", "Desc", "Test", "T")]
        [TestCase(2, "Name2", "Desc2", "Another", "A")]
        public void BasicInitialisationSpecifiedValues(int number, string name, string desc, string identifierBase, string identifierBasePrefs)
        {
            //// Arrange
            PlayerPrefs.DeleteAll();
            var gameConfiguration = ScriptableObject.CreateInstance<GameConfiguration>();
            var messenger = new Messenger();
            var player = ScriptableObject.CreateInstance<Player>();
            player.Initialise(gameConfiguration, null, messenger, 1);

            //// Act
            var gameItem = ScriptableObject.CreateInstance<GameItem>();
            gameItem.Initialise(gameConfiguration, player, messenger,
                number, LocalisableText.CreateNonLocalised(name), LocalisableText.CreateNonLocalised(desc), 
                identifierBase: identifierBase, identifierBasePrefs: identifierBasePrefs);

            //// Assert
            Assert.IsNotNull(gameItem, "GameItem not setup.");
            Assert.AreEqual(number, gameItem.Number, "Number not set correctly");
            Assert.AreEqual(name, gameItem.Name, "Name not set correctly");
            Assert.AreEqual(desc, gameItem.Description, "Description not set correctly");
            Assert.AreEqual(identifierBase, gameItem.IdentifierBase, "IdentifierBase not set correctly");
            Assert.AreEqual(identifierBasePrefs, gameItem.IdentifierBasePrefs, "IdentifierBasePrefs not set correctly");
            Assert.AreEqual(0, gameItem.Score, "Score not set correctly");
            Assert.AreEqual(0, gameItem.Coins, "Coins not set correctly");
            Assert.AreEqual(0, gameItem.HighScore, "HighScore not set correctly");
            Assert.AreEqual(false, gameItem.IsBought, "IsBought not set correctly");
            Assert.AreEqual(false, gameItem.IsUnlocked, "IsUnlocked not set correctly");
            Assert.AreEqual(false, gameItem.IsUnlockedAnimationShown, "IsUnlockedAnimationShown not set correctly");
        }


        /// <summary>
        /// Seperate test from creating, saving and then loadina GameItem to verify the consistency of saved preferences
        /// across different versions of the framework (that we use the same preferences keys).
        /// </summary>
        [TestCase(0, 1, "Test", "T", 10, false, false, false)]
        [TestCase(1, 2, "Another", "A", 10, false, false, false)]
        [TestCase(0, 1, "Test", "T", 20, false, false, false)]
        [TestCase(0, 1, "Test", "T", 20, true, false, false)]
        [TestCase(0, 1, "Test", "T", 20, false, true, false)]
        [TestCase(0, 1, "Test", "T", 20, false, false, true)]
        public void PersistentValuesLoaded(int playerNumber, int number, string identifierBase, string identifierBasePrefs, 
            int highScore, bool isUnlocked, bool isUnlockedAnimationShown, bool isBought)
        {
            //// Arrange
            PlayerPrefs.DeleteAll();
            SetCommonPreferences(playerNumber, number, identifierBasePrefs, highScore, isUnlocked, isUnlockedAnimationShown, isBought);
            var gameConfiguration = ScriptableObject.CreateInstance<GameConfiguration>();
            var messenger = new Messenger();
            var player = ScriptableObject.CreateInstance<Player>();
            player.Initialise(gameConfiguration, null, messenger, playerNumber);

            //// Act
            var gameItem = ScriptableObject.CreateInstance<GameItem>();
            gameItem.Initialise(gameConfiguration, player, messenger,
                number, identifierBase: identifierBase, identifierBasePrefs: identifierBasePrefs);

            //// Assert
            Assert.IsNotNull(gameItem, "GameItem not setup.");
            Assert.AreEqual(highScore, gameItem.HighScore, "HighScore not set correctly");
            Assert.AreEqual(isBought, gameItem.IsBought, "IsBought not set correctly");
            if (isBought)
                Assert.AreEqual(true, gameItem.IsUnlocked, "IsUnlocked not set correctly when bought");
            else
                Assert.AreEqual(isUnlocked, gameItem.IsUnlocked, "IsUnlocked not set correctly when not bought");
            Assert.AreEqual(isUnlockedAnimationShown, gameItem.IsUnlockedAnimationShown, "IsUnlockedAnimationShown not set correctly");
        }


        /// <summary>
        /// Seperate test from creating, saving and then loadina GameItem to verify the consistency of saved preferences
        /// across different versions of the framework (that we use the same preferences keys).
        /// </summary>
        [TestCase(0, 1, "Test", "T", 10, false, false, false)]
        [TestCase(1, 2, "Another", "A", 10, false, false, false)]
        [TestCase(0, 1, "Test", "T", 20, false, false, false)]
        [TestCase(0, 1, "Test", "T", 20, true, false, false)]
        [TestCase(0, 1, "Test", "T", 20, false, true, false)]
        [TestCase(0, 1, "Test", "T", 20, false, false, true)]
        public void PersistentValuesSaved(int playerNumber, int number, string identifierBase, string identifierBasePrefs,
            int highScore, bool isUnlocked, bool isUnlockedAnimationShown, bool isBought)
        {
            //// Arrange
            PlayerPrefs.DeleteAll();
            var gameConfiguration = ScriptableObject.CreateInstance<GameConfiguration>();
            var messenger = new Messenger();
            var player = ScriptableObject.CreateInstance<Player>();
            player.Initialise(gameConfiguration, null, messenger, playerNumber);

            //// Act
            var gameItem = ScriptableObject.CreateInstance<GameItem>();
            gameItem.Initialise(gameConfiguration, player, messenger,
                number, identifierBase: identifierBase, identifierBasePrefs: identifierBasePrefs);
            gameItem.HighScore = highScore;
            gameItem.IsUnlocked = isUnlocked;
            gameItem.IsUnlockedAnimationShown = isUnlockedAnimationShown;
            gameItem.IsBought = isBought;
            gameItem.UpdatePlayerPrefs();
            PlayerPrefs.Save();

            //// Assert
            Assert.IsNotNull(gameItem, "GameItem not setup.");
            AssertCommonPreferences(playerNumber, number, identifierBasePrefs, gameItem);
        }


        //TODO: 
        // Test startUnlocked flag
        // IsBought changes Unlocked flag.

        #endregion Initialisation

        #region Unlocking

        [Test]
        public void Unlocking()
        {
            ////// Arrange
            //var gameItemManager = new GameItemManager<GameItem, GameItem>();
            ////var messenger = new Messenger();
            ////_testHandlerCalled = false;
            ////messenger.AddListener<BaseMessage>(TestHandler);

            ////// Act
            ////messenger.TriggerMessage(new BaseMessage());

            ////// Assert
            //Assert.IsNotNull(gameItemmanager.Items, "The items array should be initialised");
            //Assert.AreEqual(gameItemmanager.Items.Length, 0, "The items array should be be empty on initialisation");

            ////// Cleanup
            ////messenger.RemoveListener<BaseMessage>(TestHandler);
        }

        #endregion Unlocking
    }
}
#endif