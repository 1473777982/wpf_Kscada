TwinCAT ADS Reactive Extensions (ADS.Rx)
========================================
Extends the TwinCAT.Ads.TcAdsClient with Reactive Interfaces for ADS Notifications to support observable Streams of value changes.

## Prerequisites
- a TwinCAT 2 or 3 Installation (XAR-Runtime or Full)
- .NET Framework 4.6

## Features
- Observable Streams of ADS Notifications (type safe or raw)
- Observable Streams of ADS State changes
- ... to be continued

## First Steps

```cs
using TwinCAT.Ads;
using TwinCAT.Ads.Reactive;

class ReactiveTest
{
  public void Communicate()
  {
    // To Test the Observer run a project on the local PLC System (Port 851)
    using (TcAdsClient client = new TcAdsClient())
    {
      // Connect to target
      client.Connect(new AmsAddress(AmsNetId.Local, 851));

      // Usage of ANY_TYPES in reactive stream (here the 'ushort' type) without usage of the 'SymbolLoader'
      var valueObserver = Observer.Create<ushort>(val =>
          {
              Console.WriteLine(string.Format("Value: {0}", val.ToString()));
          }
      );

      // Turning ADS Notifications into sequences of Value Objects 
      // and subscribe to them.
      IDisposable subscription = client.WhenNotification<ushort>(
        "TwinCAT_SystemInfoVarList._TaskInfo.CycleCount",
		NotificationSettings.Default
	  )
          .Subscribe(valueObserver);

      ...
      subscription.Dispose(); // Dispose the Subscription


      // Polling of values to prevent the usage of Notifications
      // Create Symbol information
      var symbolLoader = SymbolLoaderFactory.Create(client, SymbolLoaderSettings.Default);
      IValueSymbol cycleCount = (IValueSymbol)symbolLoader
	    .Symbols["TwinCAT_SystemInfoVarList._TaskInfo.CycleCount"];

      // Reactive Notification Handler
      var valueObserver = Observer.Create<object>(val =>
        {
          Console.WriteLine(
            string.Format("Instance: {0}, Value: {1}", cycleCount.InstancePath, val.ToString())
		  );
        }
      );

      // Take Values in an Interval of 500ms triggered by an application timer (no notifications)
      IDisposable subscription = cycleCount
        .PollValues(TimeSpan.FromMilliseconds(500))
		.Subscribe(valueObserver);
      
      ...
      subscription.Dispose(); // Dispose the subscription


      // Create Symbol information
      var symbolLoader = SymbolLoaderFactory.Create(client, SymbolLoaderSettings.DefaultDynamic);

      int eventCount = 1;

      // Reactive Notification Handler
      var valueObserver = Observer.Create<TwinCAT.Ads.Reactive.SymbolNotification>(not =>
          {
              Console.WriteLine(
                  string.Format("{0} {1:u} {2} = '{3}' ({4})",
                  eventCount++,
                  not.TimeStamp,
                  not.Symbol.InstancePath,
                  not.Value,
                  not.Symbol.DataType)
              );
          }
      );

      // Collect the symbols that are registered as Notification sources for their changed values.

      SymbolCollection notificationSymbols = new SymbolCollection();
      IArrayInstance taskInfo = (IArrayInstance)symbolLoader
          .Symbols["TwinCAT_SystemInfoVarList._TaskInfo"];
          
      foreach(ISymbol element in taskInfo.Elements)
      {
          ISymbol cycleCount = element.SubSymbols["CycleCount"];
          ISymbol lastExecTime = element.SubSymbols["LastExecTime"];

          notificationSymbols.Add(cycleCount);
          notificationSymbols.Add(lastExecTime);
      }

      // Create a subscription for the first 200 Notifications on Symbol Value changes.
      IDisposable subscription = client.WhenNotification(notificationSymbols)
          .Take(200)
          .Subscribe(valueObserver);

      ...
      subscription.Dispose(); // Dispose the Subscription
    }
  }
}
```

## Documentation and further learning

[Extension classes in the TwinCAT.Ads.Reactive namespace](https://infosys.beckhoff.com/content/1033/tcadsnetref/7313610891.html?id=3925137517396292438)

[TcAdsClient class description with Samples](https://infosys.beckhoff.com/content/1033/tc3_adsnetref/7313399947.html?id=2143854398042839406)

[Documentation ADS .NET API](https://infosys.beckhoff.com/content/1033/tc3_adsnetref/7312567947.html?id=1468782086487140895)

[API Reference](https://infosys.beckhoff.com/content/1033/tc3_adsnetref/7312592011.html?id=7587521548766668780)


## Links
[Beckhoff Homepage](https://www.beckhoff.com)
