4.4.7
=====
Bug: Nuget Package Dependencies are referencing System.Reactive Version 4.1. Changed to 4.4.1 

4.4.2
=====
Enh: Reactive extension methods WhenNotification, WhenNotificationEx, WhenAdsStateChanges, PollAdsState, PollValues, WriteValues now detect the SymbolVersionChanged event and recreate internally stored
		handles. This enables the Observables to survive an upload of PlcPrograms (with still existing symbols.)
Enh: Added WhenSymbolVersionChanges Observable.

4.4.0
=====
Enh: Referencing TwinCAT.Ads.dll AssemblyVersion 4.4.0.0 to prevent GAC usage in TwinCAT installations and restore Nuget Semantic Package Versioning.

4.3.10.0
========
Enh: Updated System.Reactive package from 4.1.0 to 4.1.6

4.3.7.0
=======
Enh: Some minor improvements.

4.3.1.0
=======
Enh: AdsClientExtensions.PollAdsState added

4.3.0.0
=======
First version of the 4.3.X.X series of the Beckhoff.TwinCAT.Ads.Reactive package