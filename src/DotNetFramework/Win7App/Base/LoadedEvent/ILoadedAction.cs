using System;

namespace Win7App.Base.LoadedEvent
{
    // https://randombitsandbytes.com/2018/07/05/wpf-bring-the-loaded-event-to-mvvm/

    public interface ILoadedAction
    {
        void ContentControlLoaded();
    }
}
