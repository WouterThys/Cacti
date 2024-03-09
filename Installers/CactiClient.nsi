;CLIENT
!define PRODUCT_NAME "CactiClient"
!define DISPLAY_NAME "My Happy Cacti Collection"
!define BUILD_TYPE "Release"
!define SOURCE_DIRIRECTORY "..\Release\client\"
!define EXECUTABLE "CactiClient.exe"
!define ICON "cactus-icon-vector.ico"
!define COMMAND_LINE_PARAMS ""

!define LINK_NAME "${DISPLAY_NAME}.lnk"
!getdllversion "${SOURCE_DIRIRECTORY}\${EXECUTABLE}" version_
!define OUTPUT_FILE "..\Release\installers\${PRODUCT_NAME}_${PRODUCT_VERSION}_${BUILD_TYPE}_${UPGRADE_CODE}.exe"
!define PRODUCT_VERSION "${version_1}.${version_2}.${version_3}"
!define PRODUCT_PUBLISHER "WouterThys"
!define PRODUCT_WEB_SITE "http://www.WouterThys.be/"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\${DISPLAY_NAME}"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${DISPLAY_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; Include again for compression
;SetCompressor /SOLID lzma

; Includes
!include LogicLib.nsh
!include "MUI.nsh"
!include "FileFunc.nsh"
!insertmacro GetTime

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${ICON}"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Language Selection Dialog Settings
!define MUI_LANGDLL_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_LANGDLL_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_LANGDLL_REGISTRY_VALUENAME "NSIS:Language"

; Welcome page
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP ${MUI_ICON}
!define MUI_WELCOMEFINISHPAGE_BITMAP "cactus-icon-vector.bmp"
!insertmacro MUI_PAGE_WELCOME
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN
!define MUI_FINISHPAGE_RUN_TEXT "Start ${DISPLAY_NAME}"
!define MUI_FINISHPAGE_RUN_FUNCTION "LaunchLink"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "Dutch"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "${OUTPUT_FILE}"
InstallDir "$PROGRAMFILES\Cacti\${PRODUCT_NAME}"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Function LaunchLink
  ExecShell "" "$INSTDIR\${LINK_NAME}"
FunctionEnd

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  CreateDirectory "$INSTDIR\Logs"

  ; get current time as follows: $3$2$1$5$6$7
  ${GetTime} "" "L" $1 $2 $3 $4 $5 $6 $7

  FileOpen $9 '.\Logs\installation.log' w ;Opens a Empty File and fills it
  FileWrite $9 "Install log for ${DISPLAY_NAME} - ${PRODUCT_VERSION} $\r$\n"
  FileWrite $9 "----------------------------- $\r$\n$\r$\n"
  FileWrite $9 "DATE: $3$2$1$5$6$7  $\r$\n"
  FileWrite $9 "PRODUCT_NAME: ${PRODUCT_NAME}  $\r$\n"
  FileWrite $9 "DISPLAY_NAME: ${DISPLAY_NAME}  $\r$\n"
  FileWrite $9 "INSTDIR: $INSTDIR  $\r$\n"
  FileWrite $9 "BUILD_TYPE: ${BUILD_TYPE}  $\r$\n"
  FileWrite $9 "SOURCE_DIRIRECTORY: ${SOURCE_DIRIRECTORY}  $\r$\n"
  FileWrite $9 "EXECUTABLE: ${EXECUTABLE}  $\r$\n"
  FileWrite $9 "ICON: ${ICON}  $\r$\n"
  FileWrite $9 "COMMAND_LINE_PARAMS: ${COMMAND_LINE_PARAMS}  $\r$\n"
  FileWrite $9 "PRODUCT_DIR_REGKEY: ${PRODUCT_DIR_REGKEY}  $\r$\n"
  FileWrite $9 "$\r$\n"
  FileClose $9 ;Closes the filled file

  ; Main files
  SetOverwrite try
  File /r "${SOURCE_DIRIRECTORY}\*"
  File "${ICON}"

  ; Directories and links
  CreateDirectory "$SMPROGRAMS\Cacti"
  CreateShortCut "$INSTDIR\${LINK_NAME}" "$INSTDIR\${EXECUTABLE}" "${COMMAND_LINE_PARAMS}" "${ICON}"
  CreateShortCut "$SMPROGRAMS\Cacti\${LINK_NAME}" "$INSTDIR\${EXECUTABLE}" "${COMMAND_LINE_PARAMS}" "${ICON}"
  CreateShortCut "$DESKTOP\${LINK_NAME}" "$INSTDIR\${EXECUTABLE}" "${COMMAND_LINE_PARAMS}" "${ICON}"


  FileOpen $9 '.\Logs\installation.log' a ;Opens a Empty File and fills it
  FileSeek $9 0 END
  FileWrite $9 "Installation finished $\r$\n$\r$\n"
  FileClose $9 ;Closes the filled file

SectionEnd

Section -AdditionalIcons
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\${EXECUTABLE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "${ICON}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

Function .onInit
  ${If} ${BUILD_TYPE} == "Debug"
    StrCpy $INSTDIR "$PROGRAMFILES\Cacti\Debug\${PRODUCT_NAME}"
  ${Else}
    StrCpy $INSTDIR "$PROGRAMFILES\Cacti\${PRODUCT_NAME}"
  ${EndIf}
FunctionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) is verwijderd van uw computer."
FunctionEnd

Function un.onInit
!insertmacro MUI_UNGETLANGUAGE
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Weet u zeker dat u $(^Name) volledig wil verwijderen?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\${LINK_NAME}"
  Delete "$SMPROGRAMS\Cacti\${LINK_NAME}"
  Delete "$DESKTOP\${LINK_NAME}"

  RMDir /r "$APPDATA\CactiClient\${PRODUCT_NAME}\"
  RMDir /r "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd