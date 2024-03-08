

SHELL=cmd
MSBUILD="D:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.com"
SANDCASTLEBUILD="D:\Program Files\Microsoft Visual Studio\2022\Community\Msbuild\Current\Bin\MSBuild.exe"
ANDROIDBUILD="gradlew"
NSISBUILD="D:\Program Files (x86)\NSIS\makensis.exe"
JAVA=java
GIT=git
BRANCH=$(shell $(GIT) branch --show-current)
NOW=$(shell python Scripts/other/getdate.py)

VERSION_FILE=version.txt
VERSION=$(shell python Scripts/other/getversion.py $(VERSION_FILE))
UPGRADE_CODE=$(shell python Scripts/other/getproductcode.py $(VERSION_FILE))
SOLUTION=.NET\CactiService\Cacti.sln
PATRICK=DatabaseHelper.exe

RELEASE_DIR=Release
SERVER_BUILD_DIR=$(RELEASE_DIR)\server
CLIENT_BUILD_DIR=$(RELEASE_DIR)\client
DATABASE_BUILD_DIR=$(RELEASE_DIR)\database
DOCUMENTATION_BUILD_DIR=$(RELEASE_DIR)\documentation
SCRIPTS_BUILD_DIR=Scripts
BACK_UP_DIR=\\waldotron4000\Werk\Cacti\Releases\$(NOW)_$(VERSION)_$(UPGRADE_CODE)
PROCEDURES_SOURCES=Database

all: release

clean:
	if not exist "$(SERVER_BUILD_DIR)" mkdir $(SERVER_BUILD_DIR)
	if not exist "$(CLIENT_BUILD_DIR)" mkdir $(CLIENT_BUILD_DIR)
	if not exist "$(DATABASE_BUILD_DIR)" mkdir $(DATABASE_BUILD_DIR)
	if not exist "$(DOCUMENTATION_BUILD_DIR)" mkdir $(DOCUMENTATION_BUILD_DIR)
	del /S/Q "$(SERVER_BUILD_DIR)/"
	del /S/Q "$(CLIENT_BUILD_DIR)/"
	del /S/Q "$(DATABASE_BUILD_DIR)/"
	del /S/Q "$(DOCUMENTATION_BUILD_DIR)/"
	del /S/Q "$(STAGE_DIR)/Client/"
	del /S/Q "$(STAGE_DIR)/Documentation/"
	del /S/Q "$(STAGE_DIR)/Database/"
	del /S/Q "$(STAGE_DIR)/Server/"
	del /S/Q "$(STAGE_DIR)/Scripts/"
	python3 Scripts\other\cleandatabasefiles.py $(abspath ./Database)/
	$(MSBUILD) $(SOLUTION) /Clean "Release|x64"
	$(MSBUILD) $(SOLUTION) /Clean "Release|x86"

# Release files
release: clean update_version build stage back_up_release git

# Update version files
update_version:
	@echo version:$(VERSION)
	python3 Scripts\version\update_version.py $(abspath ./)/ $(VERSION_FILE)

# Build 
build: DatabaseFiles CactiClient.exe app-release.apk build_server CactiService.exe

DatabaseFiles: database_dir create_db.sql updatescripts.sql
	xcopy /s $(PROCEDURES_SOURCES)\_*.sql "$(DATABASE_BUILD_DIR)" /Y

database_dir:
	if not exist "$(DATABASE_BUILD_DIR)" mkdir $(DATABASE_BUILD_DIR)

create_db.sql:
	cd Database\helper  && \
	$(PATRICK) CREATE_DATABASE ..\config.json --crOut=..\..\$(DATABASE_BUILD_DIR)\output\create_db.sql crAdd=..\additional_create_script\additional_create_script.sql

updatescripts.sql:
	cd Database\helper  && \
	$(PATRICK) UPDATESCRIPTS_CREATE ..\config.json --updOut=..\..\$(DATABASE_BUILD_DIR)\output\updatescripts.sql

CactiClient.exe:
	if not exist "$(CLIENT_BUILD_DIR)" mkdir $(CLIENT_BUILD_DIR)
	$(MSBUILD) $(SOLUTION) /Build "Release|x86" /Project CactiClient
#$(NSISBUILD) /DUPGRADE_CODE=$(UPGRADE_CODE) Installers/CactiClient.nsi

app-release.apk:
	cd Android  && \
	$(ANDROIDBUILD) :app:assembleRelease
	xcopy "Android\app\build\outputs\apk\release\app-release*.apk" "$(CLIENT_BUILD_DIR)\CactiPhone_$(VERSION)_Release_$(UPGRADE_CODE).apk"* /Y

build_server:
	if not exist "$(SERVER_BUILD_DIR)" mkdir $(SERVER_BUILD_DIR)
	$(MSBUILD) $(SOLUTION) /Build "Release|x64" /Project CactiServer
	xcopy ".NET\CactiService\CactiService\bin\Release\net7.0\*" $(SERVER_BUILD_DIR) /Y

CactiService.exe: #build_server
	$(NSISBUILD) /DUPGRADE_CODE=$(UPGRADE_CODE) Installers/ServerInstaller.nsi

# Stage
stage:

back_up_release:
	if not exist "$(BACK_UP_DIR)\Release" mkdir $(BACK_UP_DIR)\Release
	xcopy /s /e "$(RELEASE_DIR)" "$(BACK_UP_DIR)\Release" /Y

# Git
git: git_tag git_update_master

git_tag:
	$(GIT) commit -am "Update to version $(VERSION) - $(UPGRADE_CODE)"
	$(GIT) tag -a V$(VERSION)_$(UPGRADE_CODE) -m "Release version $(NOW)-$(VERSION) - $(UPGRADE_CODE)"
	$(GIT) push origin
	$(GIT) push origin --tags
	
git_update_master:	
	$(GIT) checkout master
	$(GIT) pull origin
	$(GIT) merge -s ours $(BRANCH)
	$(GIT) push origin
	$(GIT) checkout $(BRANCH)
