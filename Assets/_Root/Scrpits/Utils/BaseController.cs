using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TextProject.Utils
{
    public abstract class BaseController : IDisposable
    {
        protected List<BaseController> _controllers;
        protected List<GameObject> _gameObjects;
        private bool _isDisposed;

        protected void AddGameObject(GameObject go)
        {
            _gameObjects ??= new List<GameObject>();
            _gameObjects.Add(go);
        }

        protected void AddController(BaseController controller)
        {
            _controllers ??= new List<BaseController>();
            _controllers.Add(controller);
        }

        protected virtual void OnDispose()
        {
            
        }

        private void DisposeControllers()
        {
            if (_controllers == null)
            {
                return;
            }

            foreach (BaseController controller in _controllers)
            {
                controller.Dispose();
            }
            _controllers.Clear();
        }
        
        private void DisposeGameObjects()
        {
            if (_gameObjects == null)
            {
                return;
            }

            foreach (GameObject go in _gameObjects)
            {
                Object.Destroy(go);
            }
            _gameObjects.Clear();
        }
        
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            OnDispose();
            _isDisposed = true;
            DisposeControllers();
            DisposeGameObjects();
        }
    }
}