using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace Loaders
{
    //reasonable example tying coroutines + async/await
    //https://gist.github.com/krzys-h/9062552e33dd7bd7fe4a6c12db109a1a
    public class UnityWebRequestAwaiter : INotifyCompletion
    {
        private UnityWebRequestAsyncOperation asyncOp;
        private Action continuation;

        public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
        {
            this.asyncOp = asyncOp;
            asyncOp.completed += OnRequestCompleted;
        }

        public bool IsCompleted { get { return asyncOp.isDone; } }

        public void GetResult() { }

        public void OnCompleted(Action continuationIn)
        {
            this.continuation = continuationIn;
        }

        private void OnRequestCompleted(AsyncOperation obj)
        {
            continuation();
        }
    }

    public static class ExtensionMethods
    {
        public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
        {
            return new UnityWebRequestAwaiter(asyncOp);
        }
    }

/*
// Usage example:
UnityWebRequest www = new UnityWebRequest();
// ...
await www.SendWebRequest();
Debug.Log(req.downloadHandler.text);
//warning: potential bug if immediate completion:
//@krzys-h the code fails in situations where the UnityWebRequestAsyncOperation is immediately successful, which is the case when you try to load a file from StreamingAssets on Android. Then OnRequestCompleted is called without OnCompleted being called first and therefore continuation is null. The whole thing never returns.
//the "completed" event: If a handler is registered after the operation has completed and has already invoked the complete event, the handler will be called synchronously.

*/
}
