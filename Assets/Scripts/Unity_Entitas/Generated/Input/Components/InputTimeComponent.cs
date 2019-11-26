//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity timeEntity { get { return GetGroup(InputMatcher.Time).GetSingleEntity(); } }
    public Enemies.Components.Input.TimeComponent time { get { return timeEntity.time; } }
    public bool hasTime { get { return timeEntity != null; } }

    public InputEntity SetTime(float newDeltaTime, float newRealTimeSinceStarup) {
        if (hasTime) {
            throw new Entitas.EntitasException("Could not set Time!\n" + this + " already has an entity with Enemies.Components.Input.TimeComponent!",
                "You should check if the context already has a timeEntity before setting it or use context.ReplaceTime().");
        }
        var entity = CreateEntity();
        entity.AddTime(newDeltaTime, newRealTimeSinceStarup);
        return entity;
    }

    public void ReplaceTime(float newDeltaTime, float newRealTimeSinceStarup) {
        var entity = timeEntity;
        if (entity == null) {
            entity = SetTime(newDeltaTime, newRealTimeSinceStarup);
        } else {
            entity.ReplaceTime(newDeltaTime, newRealTimeSinceStarup);
        }
    }

    public void RemoveTime() {
        timeEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public Enemies.Components.Input.TimeComponent time { get { return (Enemies.Components.Input.TimeComponent)GetComponent(InputComponentsLookup.Time); } }
    public bool hasTime { get { return HasComponent(InputComponentsLookup.Time); } }

    public void AddTime(float newDeltaTime, float newRealTimeSinceStarup) {
        var index = InputComponentsLookup.Time;
        var component = (Enemies.Components.Input.TimeComponent)CreateComponent(index, typeof(Enemies.Components.Input.TimeComponent));
        component.DeltaTime = newDeltaTime;
        component.RealTimeSinceStarup = newRealTimeSinceStarup;
        AddComponent(index, component);
    }

    public void ReplaceTime(float newDeltaTime, float newRealTimeSinceStarup) {
        var index = InputComponentsLookup.Time;
        var component = (Enemies.Components.Input.TimeComponent)CreateComponent(index, typeof(Enemies.Components.Input.TimeComponent));
        component.DeltaTime = newDeltaTime;
        component.RealTimeSinceStarup = newRealTimeSinceStarup;
        ReplaceComponent(index, component);
    }

    public void RemoveTime() {
        RemoveComponent(InputComponentsLookup.Time);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherTime;

    public static Entitas.IMatcher<InputEntity> Time {
        get {
            if (_matcherTime == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Time);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherTime = matcher;
            }

            return _matcherTime;
        }
    }
}