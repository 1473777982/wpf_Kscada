4.4.7
=====
Enh: Optimization of ReadValue() methods internally using ValueByName instead of ValueByHandle reducing roundtrips.
Enh: Using Project Encoding for TcAdsClient.ReadSymbol and TcAdsClient.WriteSymbol
Enh: Adding TcAdsClient.ReadSymbolByName method.
Enh: Ignoring DataType with wrong PointerSize preventing ArgumentOutOfRangeException in AdsParseSymbols.SetPlatformPointerSize
Bug: Race condition accessing data via Symbol.ReadValue / Symbol.WriteValue (KeyAlreadyInListException)
Bug: Fixing NullReferenceException on accessing ManagedType property on PVOID DataType

4.4.6
=====
Enh: Extended Handling for AmsNetIds (SubNets), AmsNetId.IsSameTarget and AmsNetId.NetIdsEqual extended.

4.4.5
=====
Fix: DefaultNotificationSettings on SymbolLoader are not derived to the NotificationSettings of the Symbol 
Fix: ArgumentOutOfRangeException when calling RpcMethods without Return parameter
Fix: InvalidCastException on Writing Values on type 'T_MaxString'

4.4.4
=====
Fix: NullReferenceException on DynamicTree SymbolLoader if PVOID DataTypes are used in the symbols.

4.4.3
=====
Fix: InvokeRpcMethod checks for In and Out parameters corrected.
Fix: TwinCAT.AdsSymbolLoaderSettings.Default corrected to 'Symbolic' Value Access.

4.4.2
=====
Enh: Refactoring InvokeRpcMethod (Support of out-parameters)
Enh: Support of platform specific data types 'UXINT, XINT, XWORD'.
Enh: Support of Recreating cached symbol handles on SymbolVersionChanged event when using TcAdsClients ReadSymbol, WriteSymbol methods and AddDeviceNotifications with SymbolPath.

4.4.1
=======
Fix: ArgumentOutOfRangeException with SByte types in PrimitiveTypeConverter.TryConvert
Fix: Internal Exceptions when registering Notifications via AddRegisterDeviceNotification with UserData something different than ISymbol

4.4.0
=====
Enh: Upgrading the Beckhoff.TwinCAT.Ads Nuget Package to a new Minor AssemblyVersion number 4.4.0.0 to prevent Nuget package to clash with Software packages
installing this DLL into the Global Assembly Cache (GAC) (reenabling Nuget Package Semantic versioning)

4.3.12.0
========
Fix: Fixing AdsErrorException.GetObjectData enabling Serialization of AdsErrorException derived types.

4.3.11.0
========
Fix: Corrections of text messages in 'Obsolete-warnings' where IndexGroup/IndexOffset is not used with the uint overload
Fix: Gap year correction (year 2400) for PlcOpen DataTypes DT and DATE.
Fix: Wrong implementation of the IsPersistant Datatype Flag (AdsDataTypeFlags.Persistent) 
Fix: SubSymbol resolution of PVOID and POINTER TO VOID types.
Enh: Pointer support for InvokeRpcMethod in parameters.

4.3.10.0
========
Fix: AdsErrorCode TcAdsClient.TryReadWrite(uint,uint,AdsStream,int,int,AdsStream,int,int,out int) wrong parameter check.
Fix: Some minor issues with creation of DynamicValues.

4.3.8.0
=======
Fix: TcAdsSymbolInfoCollection.GetSymbol now finds also Symbols that are not Main (Root) Symbols.
Fix: Fixing issue with ReadSymbol/WriteSymbol using Structs and using this Struct type beforehand with reflection (.NET Type.GetFields caching issue)
Enh: Support for jagged ANYSIZE Arrays.

4.3.7.0
=======
Enh: Adding ITcAdsRpcInvoke to IAdsConnection interface to support ITcAdsRpcInvoke overloads on AdsConnection object
Fix: NullReferenceException in SymbolIterator (Symbol Browsing)
Fix: Fixing some issues Dereferencing Pointers via Instance Names and Instance Paths.
Fix: TcAdsClient.WriteAnyString(uint handle, string value, int chars, Encoding encoding) now supports also	Unicode as encoding.

4.3.6.0
=======
Fix: NullReferencesExceptions and missing Symbols on Browsing TwinCAT 4018 Targets.

4.3.5.0
=======
Enh: Adding Connection Property on Symbols with Value Access (IValueSymbol2.Connection)
Enh: Enhanced support for Pointer symbols in TcAdsClient.ReadSymbol / TcAdsClient.WriteSymbol
Fix: IValueSymbol.ValueChanged deregistration could leak exceptions in older versions. Now exceptions will be handled internally.

4.3.4.0
=======
Enh: Support of ISubRangeType types that base on other base types than Int32.

4.3.3.0
=======
Enh: Support of runtime sized Array Instances (AnySizeArrayInstance)
Enh: Adding SubSymbolCount property on TwinCAT.Ads.TypeSystem.Symbol

4.3.2.0
=======
Enh: Support of byte[] type for PrimitiveTypeConverter class

4.3.1.0
=======
Fix: NullReferenceException in SymbolLoaderV2 in .NET 2 Environment
Enh: Version numbering of CLR2 aligned to CLR4

4.3.0.0
=======
First version of the 4.3.X.X series of the Beckhoff.TwinCAT.Ads package