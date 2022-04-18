using System;
using Singleton;

namespace Core
{
    public class ActionManager : Singleton<ActionManager>
    {
        public Action<string> OnFormulaAddition;

        public override void Start()
        {
            base.Start();
            setDontDestroyOnLoad();
        }
    }
}