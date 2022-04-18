using UnityEngine;

namespace Singleton
{
    public abstract class SingletonBase : MonoBehaviour
    {
        //Making sure no Object can override Awake (Only implementation inside Singleton)
        protected virtual void Awake()
        {

        }
    }
}

