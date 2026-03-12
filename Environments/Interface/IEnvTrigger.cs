using System;

public interface IEnvTrigger
{
    void Trigger();

    Action afterTrigger { get; set; }
    Action onTrigger { get; set; }
}
