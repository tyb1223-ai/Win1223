!include "MUI2.nsh"
Name "Win1223"
OutFile "Win1223_Setup.exe"
InstallDir "$PROGRAMFILES64\Win1223"
RequestExecutionLevel Admin

!define MUI_ICON "icon.ico"
!define MUI_UNICON "icon.ico"

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_LANGUAGE "SimpChinese"

Section
SetOutPath "$INSTDIR"
File "Win1223.exe"

CreateShortcut "$DESKTOP\Win1223.lnk" "$INSTDIR\Win1223.exe"
CreateDirectory "$SMPROGRAMS\Win1223"
CreateShortcut "$SMPROGRAMS\Win1223\Win1223.lnk" "$INSTDIR\Win1223.exe"

WriteUninstaller "$INSTDIR\Uninstall.exe"
SectionEnd

Section "Uninstall"
Delete "$DESKTOP\Win1223.lnk"
Delete "$SMPROGRAMS\Win1223\Win1223.lnk"
RMDir "$SMPROGRAMS\Win1223"
Delete "$INSTDIR\Win1223.exe"
Delete "$INSTDIR\Uninstall.exe"
RMDir "$INSTDIR"
SectionEnd
