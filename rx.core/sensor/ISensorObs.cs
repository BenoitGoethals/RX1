﻿namespace rx.core.sensor;

public interface ISensorObs
{
    bool IsRunning { get; }
    string Name { get; set; }
    void Start();
    void Restart();
    IDisposable Subscribe(IObserver<Measurement> observer);
}