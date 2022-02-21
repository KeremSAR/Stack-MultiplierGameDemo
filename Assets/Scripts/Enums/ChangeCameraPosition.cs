using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeCameraPosition : MonoBehaviour
{
    
    
    // BUTONLARDA ON CLICK KULLANILARAK KAMERA POZİSYONU AYARLANABİLİR, KODDAKİ _statesWhenActive İLE HIERARCHY DE CameraPositions'DAKİ KARIŞIKLIĞI ÖNLER. MESELA STARTA BASINCA KAMERANIN BİRAZ DAHA GERİYE GİTMESİ GİBİ. UI HARİCİ DİĞER TÜM KAMERA AYARLARI Main.Instance = State.x ŞEKLİNDE OLACAK. 
    
    
    
    
    [SerializeField] private State _setState;
    [SerializeField] private State[] _statesWhenActive;

    private Action _onClickAction;

    private void Awake() =>
        Main.OnChangeState += Main_OnChangeState;

    private void OnDestroy() =>
        Main.OnChangeState -= Main_OnChangeState;

    public void OnClick() =>
        _onClickAction?.Invoke();

    private void Main_OnChangeState(State state)
    {
        if (_statesWhenActive.Any(s => s == state))
        {
            _onClickAction = () => Main.Instance.State = _setState;

        }
        else
        {
            _onClickAction = null;
            
        }
    }
}
