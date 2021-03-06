; Script generated by the Inno Script Studio Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppVersion "20170711"
#define MyAppArchitecture "x86"      
#define MyAppName "PDFraster Tool"
#define MyAppPublisher "TWAIN Working Group"
#define MyAppURL "http://www.pdfraster.org/"
#define MyAppExeName "pdfras_tool.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{ECB5A60D-C59E-4F99-8D2A-3BF6937F2F94}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName=TWAIN {#MyAppName} ({#MyAppArchitecture})
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppPublisher}\{#MyAppName}
DefaultGroupName={#MyAppName}
LicenseFile=license.txt
OutputBaseFilename=pdfras_tool-setup
Compression=lzma
SolidCompression=yes
AppCopyright=2017
OutputDir=..\bin\x86\Release
VersionInfoVersion=1.0
VersionInfoCompany=TWAIN Working Group
VersionInfoCopyright=2017
VersionInfoProductName=pdfras_tool
VersionInfoProductTextVersion=PDF/raster Tool
SetupIconFile=twain.ico
UninstallDisplayIcon={app}\twain.ico
DisableStartupPrompt=False

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "..\bin\x86\Release\pdfras_tool.exe"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files
Source: "sample.pdf"; DestDir: "{app}"; Flags: ignoreversion
Source: "README.pdf"; DestDir: "{app}"; Flags: ignoreversion
Source: "twain.ico"; DestDir: "{app}"; Flags: ignoreversion

[ThirdParty]
UseRelativePaths=True
