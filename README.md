# Core

Set of simple functionalities to start prototyping a new project.

## Description

Contains of client-server communication layer, a simple MVP pattern for UI, bootstraping done with Zenject framework.

## Getting Started

### Installing

* Add [new package with URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html) in Unity Editor

### Executing program

* Open sample scene Core/Samples/IntegrationSample/Scenes/Main
* Start Unity Editor

#### Bootstrap

Zenject is used as main DI framework.

CoreContext is main bootstrap class. Installing is done in MonoInstaller scipts; Installers will bind all needed services, models, signals, actions

Lifetime of the application is controlled by FSM - AppStateChart
Every state can have presenter that will create view by ViewID (every prefab that could be instantiated by presenters should be registered with ViewID in ViewManager)


```
Services
```
Service layer where data transformation is happening. Only the service is allowed to change data. 
After changes are done it will rise a signal to notify anyone who is interested in those changes.

```
Models
```
Models are business data. Every model implements IDataProvider interface that gives readonly access.

```
Actions
```
Command pattern implementation to abstract client-server communication.
Before sending request UpdateModel method is triggered allowing to client to update view before receiving a response.
It is possible to override OnActionFail method to rollback in case of negative response.

```
MVP
```
UI is implemented with similar to MVC pattern
Presenters - object that reacts to input and signal triggering actions and setting ViewData to Views
Views - UI and any representation
ViewData - model with additional presentation data if needed
