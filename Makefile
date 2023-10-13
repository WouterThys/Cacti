

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

DatabaseFiles:

CactiClient.exe:

app-release.apk:

build_server:

CactiService.exe:

# Stage
stage:

back_up_release:

git:
