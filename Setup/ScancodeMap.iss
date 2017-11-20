#define AppName        GetStringFileInfo('..\Binaries\ScancodeMap.exe', 'ProductName')
#define AppVersion     GetStringFileInfo('..\Binaries\ScancodeMap.exe', 'ProductVersion')
#define AppFileVersion GetStringFileInfo('..\Binaries\ScancodeMap.exe', 'FileVersion')
#define AppCompany     GetStringFileInfo('..\Binaries\ScancodeMap.exe', 'CompanyName')
#define AppCopyright   GetStringFileInfo('..\Binaries\ScancodeMap.exe', 'LegalCopyright')
#define AppBase        LowerCase(StringChange(AppName, ' ', ''))
#define AppSetupFile   AppBase + StringChange(AppVersion, '.', '')

#define AppVersionEx   StringChange(AppVersion, '0.00', '')
#ifdef VersionHash
#  if "" != VersionHash
#    define AppVersionEx AppVersionEx + " (" + VersionHash + ")"
#  endif
#endif


[Setup]
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName} {#AppVersion}
AppPublisher={#AppCompany}
AppPublisherURL=https://medo64.com/{#AppBase}/
AppCopyright={#AppCopyright}
VersionInfoProductVersion={#AppVersion}
VersionInfoProductTextVersion={#AppVersionEx}
VersionInfoVersion={#AppFileVersion}
DefaultDirName={pf}\{#AppCompany}\{#AppName}
OutputBaseFilename={#AppSetupFile}
OutputDir=..\Releases
SourceDir=..\Binaries
AppId=JosipMedved_ScancodeMap
CloseApplications="yes"
RestartApplications="no"
AppMutex=Global\JosipMedved_ScancodeMap
UninstallDisplayIcon={app}\ScancodeView.exe
AlwaysShowComponentsList=no
ArchitecturesInstallIn64BitMode=x64
DisableProgramGroupPage=yes
MergeDuplicateFiles=yes
MinVersion=0,5.1.2600
PrivilegesRequired=admin
ShowLanguageDialog=no
SolidCompression=yes
ChangesAssociations=yes
DisableWelcomePage=yes
LicenseFile=..\Setup\License.rtf


[Messages]
SetupAppTitle=Setup {#AppName} {#AppVersionEx}
SetupWindowTitle=Setup {#AppName} {#AppVersionEx}
BeveledLabel=medo64.com


[Files]
Source: "ScancodeMap.exe";      DestDir: "{app}";                            Flags: ignoreversion;
Source: "ScancodeMap.pdb";      DestDir: "{app}";                            Flags: ignoreversion;
Source: "ScancodeMapExec.exe";  DestDir: "{app}";                            Flags: ignoreversion;
Source: "ScancodeMapExec.pdb";  DestDir: "{app}";                            Flags: ignoreversion;
Source: "ScancodeView.exe";     DestDir: "{app}";                            Flags: ignoreversion;
Source: "ScancodeView.pdb";     DestDir: "{app}";                            Flags: ignoreversion;
Source: "..\LICENSE.md";        DestDir: "{app}";  DestName: "License.txt";  Flags: overwritereadonly uninsremovereadonly;  Attribs: readonly;
Source: "..\README.md";         DestDir: "{app}";  DestName: "ReadMe.txt";   Flags: overwritereadonly uninsremovereadonly;  Attribs: readonly;


[Icons]
Name: "{userstartmenu}\Scancode Map";   Filename: "{app}\ScancodeMap.exe"
Name: "{userstartmenu}\Scancode View";  Filename: "{app}\ScancodeView.exe"


[Registry]
Root: HKCU; Subkey: "Software\Josip Medved\ScancodeMap";  ValueType: dword; ValueName: "Installed"; ValueData: "1"; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\Josip Medved";                                                                        Flags: uninsdeletekeyifempty

[Run]
Filename: "{app}\ScancodeMap.exe";   Flags: postinstall nowait skipifsilent runasoriginaluser;                      Description: "Launch application";
Filename: "{app}\ScancodeView.exe";  Flags: postinstall nowait skipifsilent runasoriginaluser;                      Description: "Launch Scancode Viewer";
Filename: "{app}\ReadMe.txt";        Flags: postinstall nowait skipifsilent runasoriginaluser unchecked shellexec;  Description: "View ReadMe.txt";


[Code]

procedure InitializeWizard;
begin
  WizardForm.LicenseAcceptedRadio.Checked := True;
end;
