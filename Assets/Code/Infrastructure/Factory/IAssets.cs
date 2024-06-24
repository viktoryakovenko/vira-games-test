﻿using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IAssets : IService
    {
        public GameObject Instantiate(string path);
        public GameObject Instantiate(string path, Vector3 at);
    }
}
