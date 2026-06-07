[Setup]
AppName=Tic Tac Toe
AppVersion=1.0
DefaultDirName={autopf}\Radish\TicTacToe
DefaultGroupName=Radish
SetupIconFile=app.ico
UninstallDisplayIcon={app}\TicTacToe.exe
LicenseFile=LICENSE.txt
OutputBaseFilename=TicTacToeSetup
ArchitecturesInstallIn64BitMode=x64compatible
ArchitecturesAllowed=x64compatible
AppPublisher=Radish
AppPublisherURL=https://radish-vert.vercel.app
AppId={{e57f0884-6281-4494-b942-c91d37d83bc0}

[Files]
Source: "bin\Release\net10.0-windows\publish\win-x64\*"; DestDir: "{app}"; Flags: recursesubdirs

[Icons]
Name: "{group}\TicTacToe"; Filename: "{app}\TicTacToe.exe"
Name: "{commondesktop}\TicTacToe"; Filename: "{app}\TicTacToe.exe"; Tasks: desktopicon

[Tasks]
Name: desktopicon; Description: "Create a &desktop shortcut"; GroupDescription: "Additional icons:"

[Run]
Filename: "{app}\TicTacToe.exe"; Description: "Launch Tic Tac Toe"; Flags: nowait postinstall skipifsilent
