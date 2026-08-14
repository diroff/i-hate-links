using NUnit.Framework;
using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Testers.R3Samples
{
    public class R3Sample : MonoBehaviour
    {

        private void Start()
        {
            Example11();
        }

        public Observable<int> Health11 => _health11;
        private readonly ReactiveProperty<int> _health11;

        private void Example11()
        {
            var texts = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                texts.Add("Step " +  i);
            }

            var deferredPrintings = texts.Select((text, i) => Observable.Defer(() => Print11(text)));
            var queuedPrintings = Observable.Concat(deferredPrintings);

            var lastPrinting = queuedPrintings.TakeLast(1);

            lastPrinting.Subscribe(_ => Debug.Log("All elements is over")).AddTo(gameObject);
        }

        private Observable<Unit> Print11(string text)
        {
            Debug.Log(text);
            return Observable.Timer(TimeSpan.FromSeconds(1), UnityTimeProvider.UpdateIgnoreTimeScale);
        }

        public IObservableCollection<string> ObservableCollection10 => _observableCollection10;
        private readonly ObservableList<string> _observableCollection10 = new ObservableList<string>();

        private void Example10()
        {
            _observableCollection10.Add("El 1");
            _observableCollection10.Add("El 2");
            //Те, что выше, не учитываются при подписке

            ObservableCollection10.ObserveAdd().Subscribe(e => Debug.Log($"Element added: {e.Value}")).AddTo(gameObject);
            ObservableCollection10.ObserveRemove().Subscribe(e => Debug.Log($"Element removed: {e.Value}")).AddTo(gameObject);

            _observableCollection10.Add("El 3");
            _observableCollection10.Remove("El 2");
            _observableCollection10.Add("El 4");
            _observableCollection10.Add("El 5");
        }

        private event Action<int> ValueChanged9;

        private void Example9()
        {
            Observable.FromEvent<int>(action => ValueChanged9 += action, action => ValueChanged9 -= action)
                .Subscribe(value => Debug.Log("Value: " + value))
                .AddTo(gameObject);

            ValueChanged9?.Invoke(10);
            ValueChanged9?.Invoke(100);
        }


        public Observable<int> Health8 => _health8;
        private readonly ReactiveProperty<int> _health8 = new ReactiveProperty<int>();

        private void Example8()
        {
            Health8.Where(h => h > 50).Subscribe(value => Debug.Log($"Event. Health: {value}")).AddTo(gameObject);

            _health8.Value = 52;
            _health8.Value = 32;
            _health8.Value = 112;
            _health8.Value = 152;
            _health8.Value = 51;
            _health8.Value = 35;
        }

        public Observable<int> Health7 => _health7;
        public Observable<int> Armor7 => _armor7;

        private readonly ReactiveProperty<int> _health7 = new ReactiveProperty<int>();
        private readonly ReactiveProperty<int> _armor7 = new ReactiveProperty<int>();
        private readonly CompositeDisposable _compositeDisposable7 = new CompositeDisposable();

        private void Example7()
        {
            Health7.Merge(Armor7)
                .Subscribe(_ => Debug.Log($"Event. Health: {_health7.CurrentValue}, Armor: {_armor7.CurrentValue}"))
                .AddTo(_compositeDisposable7);

            _health7.Value = 1;
            _armor7.Value = 2;

            _health7.Value = 10;
            _armor7.Value = 20;

            _health7.Value = 15;
            _health7.Value = 16;

            _armor7.Value = 21;
            _armor7.Value = 22;
        }

        public Observable<int> Health6 => _health6;
        public Observable<int> Armor6 => _armor6;

        private readonly ReactiveProperty<int> _health6 = new ReactiveProperty<int>();
        private readonly ReactiveProperty<int> _armor6 = new ReactiveProperty<int>();
        private readonly CompositeDisposable _compositeDisposable6 = new CompositeDisposable();

        private void Example6()
        {
            _health6.Value = 1;
            _armor6.Value = 1;

            var subscrtiptionHealth = Health6.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); })
                .AddTo(_compositeDisposable6);

            var subscrtiptionArmor = Armor6.Subscribe(newValue => { Debug.Log($"Armor: {newValue}"); })
                .AddTo(_compositeDisposable6);

            _health6.Value = 2;
            _armor6.Value = 2;

            _compositeDisposable6.Dispose();

            _health6.Value = 3;
            _armor6.Value = 3;
        }

        public Observable<int> Health5 => _health5;
        private readonly ReactiveProperty<int> _health5 = new ReactiveProperty<int>();
        private IDisposable _disposable5;

        private void Example5()
        {
            _health5.Value = 100;

            _disposable5 = Health5
                .Subscribe(newValue => { Debug.Log($"Health: {newValue}"); });

            _health5.Value = 200;

            _disposable5.Dispose();

            _health5.Value = 300;
        }

        public Observable<int> HealthChanged => _healthChanged;
        private readonly Subject<int> _healthChanged = new Subject<int>();

        private void Example4()
        {
            _healthChanged.OnNext(100);

            HealthChanged.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); }); //Значение 100 не получим, т.к. Subject при подписке не подтягивает текущее значение

            _healthChanged.OnNext(200); //А отсюда уже все подтянет
            _healthChanged.OnNext(300);
        }

        public Observable<int> Health3 => _health3;
        private readonly ReactiveProperty<int> _health3 = new ReactiveProperty<int>();

        private void Example3()
        {
            Health3.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); }); //На Health3 можно только только подписаться 
                                                                                  //Этого достаточно, т.к. при подписке мы сразу получаем value из ReactiveProperty
            _health3.Value = 100;
            _health3.Value += 20;
            _health3.Value -= 50;
            _health3.OnNext(150);
        }

        public ReadOnlyReactiveProperty<int> Health2 => _health2;

        private readonly ReactiveProperty<int> _health2 = new ReactiveProperty<int>();

        private void Example2()
        {
            Health2.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); }); //Health2 можно только обрабатывать на подписке + получать CurrentValue

            _health2.Value = 110;
            _health2.Value = 95;
            _health2.Value -= 50;
            _health2.Value += 12;
        }

        private ReactiveProperty<int> _health;

        private void Example1()
        {
            _health = new ReactiveProperty<int>(200);
            _health.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); }); // Можем извне делать с _health что угодно

            _health.Value = 100;
            _health.Value = 25;
            _health.Value += 125;
            _health.Value -= 10;
            _health.OnNext(10);
        }
    }
}