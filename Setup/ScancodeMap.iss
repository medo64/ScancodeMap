#define AppName        GetStringFileInfo('..\Binaries\ScancodeView.exe', 'ProductName')
#define AppVersion     GetStringFileInfo('..\Binaries\ScancodeView.exe', 'ProductVersion')
#define AppFileVersion GetStringFileInfo('..\Binaries\ScancodeView.exe', 'FileVersion')
#define AppCompany     GetStringFileInfo('..\Binaries\ScancodeView.exe', 'CompanyName')
#define AppCopyright   GetStringFileInfo('..\Binaries\ScancodeView.exe', 'LegalCopyright')
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


[Tasks]
Name: extension_psafe3;  GroupDescription: "Associate additional extension:";  Description: "Password Safe 3.x (.psafe3)";  Flags: unchecked;


[Files]
Source: "ScancodeView.exe";  DestDir: "{app}";                            Flags: ignoreversion;
Source: "ScancodeView.pdb";  DestDir: "{app}";                            Flags: ignoreversion;
Source: "..\LICENSE.md";     DestDir: "{app}";  DestName: "License.txt";  Flags: overwritereadonly uninsremovereadonly;  Attribs: readonly;


[Icons]
Name: "{userstartmenu}\Scancode View"; Filename: "{app}\ScancodeView.exe"


[Registry]
Root: HKCU; Subkey: "Software\Josip Medved\ScancodeMap";  ValueType: dword; ValueName: "Installed"; ValueData: "1"; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\Josip Medved";                                                                        Flags: uninsdeletekeyifempty

[Run]
Filename: "{app}\ScancodeView.exe";  Flags: postinstall nowait skipifsilent runasoriginaluser;                      Description: "Launch Scancode Viewer now";
Filename: "{app}\ReadMe.txt";        Flags: postinstall nowait skipifsilent runasoriginaluser unchecked shellexec;  Description: "View ReadMe.txt";


[Code]

procedure InitializeWizard;
begin
  WizardForm.LicenseAcceptedRadio.Checked := True;
end;
