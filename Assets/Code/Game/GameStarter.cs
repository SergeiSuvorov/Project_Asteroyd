﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Asteroids
{
    internal sealed class GameStarter 
    {
        private PlayerController CreatePlayer(GameObject bulletGameObject, GameObject shipGameObject, ShipData shipData, Transform barrelTransform, WeaponData weaponData)
        {
            return new PlayerController(bulletGameObject, shipGameObject, shipData, barrelTransform, weaponData);
        }

        private Input CreateInputController(GameController gameController)
        {
            return new Input(gameController);
        }

        private AsterroidManager CreateAsterroids()
        {
            return new AsterroidManager();
        }

        private ListExecuteObject FillExecuteList(ListExecuteObject listExecuteObject, Input inputController, AsterroidManager asterroidManager, TimeRemainingController timeRemainingController)
        {
            listExecuteObject.AddExecuteObject(inputController);
            listExecuteObject.AddExecuteObject(asterroidManager);
            listExecuteObject.AddExecuteObject(timeRemainingController);
            Execute execute = Object.FindObjectOfType<Execute>();
            execute.SetListtExecuteObject(listExecuteObject);
            return listExecuteObject;
        }

        private ListExecuteObject CreateExecuteList()
        {
            return new ListExecuteObject();
        }

        private TimeRemainingController CreateTimeRemainingController()
        {
            return new TimeRemainingController();
        }

        public void CreateGame(GameObject bulletGameObject, GameObject shipGameObject, ShipData shipData, Transform barrelTransform, WeaponData weaponData,GameController gameController, out PlayerController playerController,out ListExecuteObject listExecuteObject,out Input inputController,out AsterroidManager asterroidManager)
        {
            TimeRemainingController timeRemainingController = CreateTimeRemainingController();
            listExecuteObject = CreateExecuteList();
            playerController = CreatePlayer(bulletGameObject, shipGameObject, shipData, barrelTransform, weaponData);
            inputController = CreateInputController(gameController);
            asterroidManager =CreateAsterroids();
            listExecuteObject = FillExecuteList(listExecuteObject, inputController, asterroidManager, timeRemainingController);
        }
    }
}
  