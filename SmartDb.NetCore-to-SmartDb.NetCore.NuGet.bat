set SouceDir=F:\work\dotnetcore-work\gg\SmartDb.NetCore
set DestDir=F:\work\dotnetcore-work\gg\SmartDb.NetCore.NuGet

:: copy SmartDb.Mapper.NetCore All Files
xcopy "%SouceDir%\SmartDb.Mapper.NetCore\*.cs"  "%DestDir%\SmartDb.Mapper.NetCore\" /s /r /c /v /y

:: copy SmartDb.NetCore All Files
xcopy "%SouceDir%\SmartDb.NetCore\Builder\*"  "%DestDir%\SmartDb.NetCore\Builder\" /s /r /c /v /y
xcopy "%SouceDir%\SmartDb.NetCore\Entity\*"  "%DestDir%\SmartDb.NetCore\Entity\" /s /r /c /v /y
xcopy "%SouceDir%\SmartDb.NetCore\Enum\*"  "%DestDir%\SmartDb.NetCore\Enum\" /s /r /c /v /y
xcopy "%SouceDir%\SmartDb.NetCore\Utilitys\*"  "%DestDir%\SmartDb.NetCore\Utilitys\" /s /r /c /v /y
xcopy "%SouceDir%\SmartDb.NetCore\*.cs"  "%DestDir%\SmartDb.NetCore\" /s /r /c /v /y

:: copy SmartDb.MySql.NetCore All Files
xcopy "%SouceDir%\SmartDb.MySql.NetCore\*.cs"  "%DestDir%\SmartDb.MySql.NetCore\" /s /r /c /v /y

:: copy SmartDb.PostgreSql.NetCore All Files
xcopy "%SouceDir%\SmartDb.PostgreSql.NetCore\*.cs"  "%DestDir%\SmartDb.PostgreSql.NetCore\" /s /r /c /v /y

:: copy SmartDb.SQLite.NetCore All Files
xcopy "%SouceDir%\SmartDb.SQLite.NetCore\*.cs"  "%DestDir%\SmartDb.SQLite.NetCore\" /s /r /c /v /y

:: copy SmartDb.SqlServer.NetCore All Files
xcopy "%SouceDir%\SmartDb.SqlServer.NetCore\*.cs"  "%DestDir%\SmartDb.SqlServer.NetCore\" /s /r /c /v /y

:: copy SmartDb.Repository.NetCore All Files
xcopy "%SouceDir%\SmartDb.Repository.NetCore\*.cs"  "%DestDir%\SmartDb.Repository.NetCore\" /s /r /c /v /y

:: copy TestSmartDbRepository All Files
xcopy "%SouceDir%\TestSmartDbRepository\*.cs"  "%DestDir%\TestSmartDbRepository\" /s /r /c /v /y
pause

