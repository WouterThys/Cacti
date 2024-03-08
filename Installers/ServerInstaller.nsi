; Server Script
!define BASE_NAME "Cacti-Server"
!define PRODUCT_NAME "${BASE_NAME}"
!define DISPLAY_NAME "Cacti Server"
!define BUILD_TYPE "Release"
!define SOURCE_DIRECTORY "..\Release\"
!define SCRIPT_DIRECTORY "..\Scripts"
!define DATABASE_DIRECTORY "..\Database"
!define EXECUTABLE "CactiServer.exe"
!define ICON "cactus-icon-vector.ico"
!define COMMAND_LINE_PARAMS ""

!getdllversion "${SOURCE_DIRECTORY}\server\${EXECUTABLE}" version_
!define OUTPUT_FILE "..\Release\client\${BASE_NAME}_${PRODUCT_VERSION}_${BUILD_TYPE}_${UPGRADE_CODE}.exe"
!define PRODUCT_VERSION "${version_1}.${version_2}.${version_3}"
!define PRODUCT_PUBLISHER "WouterThys"
!define PRODUCT_WEB_SITE "http://www.WouterThys.be/"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\${DISPLAY_NAME}"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${DISPLAY_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; Include again for compression
SetCompressor /SOLID lzma

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
;!define MUI_FINISHPAGE_RUN
;!define MUI_FINISHPAGE_RUN_TEXT "Start ${DISPLAY_NAME}"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "Dutch"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "${OUTPUT_FILE}"
InstallDir "C:\Program Files\Cacti\${BASE_NAME}\"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Section "MainSection" SEC01

  ; MessageBox MB_ICONSTOP|MB_OK "Install dir is: $INSTDIR"
  SetOutPath "$INSTDIR\"

  ; get current time as follows: $3$2$1$5$6$7
  ${GetTime} "" "L" $1 $2 $3 $4 $5 $6 $7

  ; Needed script files
  SetOverwrite on
  File "${SCRIPT_DIRECTORY}\service_create.bat"
  File "${SCRIPT_DIRECTORY}\service_remove.bat"
  File "${SCRIPT_DIRECTORY}\service_start.bat"
  File "${SCRIPT_DIRECTORY}\service_stop.bat"
  File "${SCRIPT_DIRECTORY}\replace_content.bat"

  ; Prepare scripts
  SetOutPath "$INSTDIR\"
  nsExec::Exec 'replace_content.bat service_create.bat DEFAULT_SERVICE_NAME ${PRODUCT_NAME} service_create.bat > installation.log'
  nsExec::Exec 'replace_content.bat service_remove.bat DEFAULT_SERVICE_NAME ${PRODUCT_NAME} service_remove.bat >> installation.log'
  nsExec::Exec 'replace_content.bat service_start.bat DEFAULT_SERVICE_NAME ${PRODUCT_NAME} service_start.bat >> installation.log'
  nsExec::Exec 'replace_content.bat service_stop.bat DEFAULT_SERVICE_NAME ${PRODUCT_NAME} service_stop.bat >> installation.log'

  ; Stop service if it is running
  nsExec::Exec 'service_stop.bat >> installation.log'

  ; Create database backup
  CreateDirectory "$INSTDIR\Backup\$3$2$1$5$6$7"
  SetOutPath "$INSTDIR\database\"
  File "${SCRIPT_DIRECTORY}\backup_db.bat"
  nsExec::Exec 'backup_db.bat cacti $INSTDIR\Backup\$3$2$1$5$6$7\database_backup.sql >> installation.log'

  ; Bakcup server files
  CreateDirectory "$INSTDIR\Backup\$3$2$1$5$6$7\server"
  SetOutPath "$INSTDIR\"
  File "${SCRIPT_DIRECTORY}\copy_folder.bat"
  nsExec::Exec 'copy_folder.bat "$INSTDIR\server" $INSTDIR\Backup\$3$2$1$5$6$7\server >> installation.log'

  ; Clean up server
  SetOutPath "$INSTDIR\"
  Delete "$INSTDIR\client\*.exe" ; Remove all executable from client folder
  Delete "$INSTDIR\client\*.apk" ; Remove all android apps from client folder
  Delete "$INSTDIR\database\*.*" ; Remove all from database folder
  RMDir /r "$INSTDIR\server\" ; Remove all files from server folder

  ; Copy all source files
  SetOverwrite try
  SetOutPath "$INSTDIR\client\"
  File /nonfatal /r "${SOURCE_DIRECTORY}\client\*${UPGRADE_CODE}.exe"
  File /nonfatal /r "${SOURCE_DIRECTORY}\client\*${UPGRADE_CODE}.apk"

  ; Server files
  SetOverwrite on
  SetOutPath "$INSTDIR\server\"
  File /r "${SOURCE_DIRECTORY}\server\*"

  ; Config files
  ; SetOverwrite try
  ; SetOutPath "$INSTDIR\server\"
  ; File /r "${CONFIG_DIRECTORY}\Server\*"

  ; Execute database scripts
  SetOverwrite on
  SetOutPath "$INSTDIR\database\"
  File "${SCRIPT_DIRECTORY}\create_db.bat"
  File "${SCRIPT_DIRECTORY}\update_db.bat"
  ; All helper files
  File /r /x "Logs" "${DATABASE_DIRECTORY}\helper\*.*"
  SetOutPath "$INSTDIR\database\procedures\"
  File /r "${DATABASE_DIRECTORY}\procedures\*.sql"
  SetOutPath "$INSTDIR\database\"
  File /r "${DATABASE_DIRECTORY}\config.json"
  ; Output files
  File /r "${SOURCE_DIRECTORY}\database\*.sql"
  ; Execute!
  SetOutPath "$INSTDIR\database\"
  nsExec::Exec 'create_db.bat cacti .\output\create_db.sql .\output\updatescripts.sql >> create_db.log'
  nsExec::Exec 'update_db.bat cacti ${PRODUCT_VERSION} .\output\updatescripts.sql .\output\translations.sql >> update_db.log'

  ; Create (if not exist) and configure it
  SetOutPath "$INSTDIR\"
  nsExec::Exec 'service_create.bat ${PRODUCT_NAME} "${DISPLAY_NAME} - ${PRODUCT_VERSION}" "$INSTDIR\Server\${EXECUTABLE}" >> installation.log'

  ; Finally start the service
  SetOutPath "$INSTDIR\"
  nsExec::Exec 'service_start.bat >> installation.log'

SectionEnd

Section -AdditionalIcons
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\${EXECUTABLE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\${ICON}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

Function .onInit
  ; ${If} ${BUILD_TYPE} == "Debug"
  ;   StrCpy $INSTDIR "$PROGRAMFILES\MultiBusy\Debug\${PRODUCT_NAME}"
  ; ${Else}
  ;   StrCpy $INSTDIR "$PROGRAMFILES\MultiBusy\${PRODUCT_NAME}"
  ; ${EndIf}
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

  SetOutPath "$INSTDIR\"
  nsExec::Exec 'service_remove.bat > uninstall.log'

  Delete "$INSTDIR\*.exe"
  Delete "$INSTDIR\*.dll"
  Delete "$INSTDIR\*"
  RMDir /r "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd