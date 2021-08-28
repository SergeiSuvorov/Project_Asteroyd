using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public CompositeDisposable disposables;

    
    void OnEnable()
    {
        disposables = new CompositeDisposable();
        MessageBroker.Default
            .Receive<MessageBase>() // задаем тип MessageBase
            .Where(msg => msg.id == ServiceShareData.MSG_DESTROY)//фильтруем message по id
            .Subscribe(msg => { // подписываемся
            string data = (string)msg.data; // кастим данные в нужный формат
                                            // можем работать как и с sender-ом так и с данными
            Debug.Log("sender:" + msg.sender.name + " receiver:" + name + " data:" + data);
            }).AddTo(disposables);
    }
    void OnDisable()
    { // отписываемся
        if (disposables != null)
        {
            disposables.Dispose();
        }
    }
}
