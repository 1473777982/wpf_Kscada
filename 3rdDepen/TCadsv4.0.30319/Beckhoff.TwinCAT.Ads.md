The TwinCAT ADS API is a .NET Assembly enabling to develop own .NET applications (e.g. visualization, scientific automation) for communication with TwinCAT devices (e.g. PLC, NC or IO-devices).

## Prerequisites
- a TwinCAT 2 or 3 Installation (XAR-Runtime or Full)
- .NET Framework 4.0

## Features
- the implementation of ADS Clients
- the browsing of (ADS) server side symbolic information
- Symbolic Read/Write from/to ADS Servers (Process Images)
- Value Change Events (ADS Notifications)
- Support of Raw ProcessImageData, AnyType concept or full dynamic typed (type safe) symbols

## First Steps
```c#
using TwinCAT.Ads;

public class AdsTest
{
	public void Communicate()
	{
		using (TcAdsClient client = new TcAdsClient())
		{
			// Connect to Local System AdsPort 851 (First PLC)
			client.Connect(AmsNetId.Local,851); 

			client.Read ...
			client.Write ...
		}
	}
}
```

## Documentation and further learning
[TcAdsClient class description with Samples](https://infosys.beckhoff.com/content/1033/tc3_adsnetref/7313399947.html?id=2143854398042839406)
[Documentation ADS .NET API](https://infosys.beckhoff.com/content/1033/tc3_adsnetref/7312567947.html?id=1468782086487140895)
[API Reference](https://infosys.beckhoff.com/content/1033/tc3_adsnetref/7312592011.html?id=7587521548766668780)


## Links
[Beckhoff Homepage](https://www.beckhoff.com)