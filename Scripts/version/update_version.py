import os
import re
import fileinput
import glob
import sys
import uuid

# [assembly: AssemblyVersion("1.2.1.*")]
def update_assembly_info_version_string(file_name, major, minor, build):
    if os.path.exists(file_name):
        with fileinput.FileInput(file_name, inplace=True) as assembly_file:
            for line in assembly_file:
                match = re.match(r'\[assembly: AssemblyVersion\("\d+\.\d+\.\d+\.\*"\)]', line)
                if match:
                    group = match.group(0)
                    print(line.replace(group, '[assembly: AssemblyVersion("'+str(major)+'.'+str(minor)+'.'+str(build)+'.*")]'), end='')
                else:
                    print(line, end='')

# <AssemblyVersion>2.0.6.0</AssemblyVersion>
# <FileVersion>2.0.6.0</FileVersion>
def update_csproj_version_string(file_name, major, minor, build):
    if os.path.exists(file_name):
        with fileinput.FileInput(file_name, inplace=True) as assembly_file:
            for line in assembly_file:
                assembly_code_match = re.match(r'\s*<AssemblyVersion>\d+\.\d+\.\d+\.\d+</AssemblyVersion>', line)
                file_code_match = re.match(r'\s*<FileVersion>\d+\.\d+\.\d+\.\d+</FileVersion>', line)
                product_code_match = re.match(r'\s*<ProductVersion>\d+\.\d+\.\d+\.\d+</ProductVersion>', line)
                if assembly_code_match:
                    old_version = re.search(r'\d+\.\d+\.\d+\.\d+', assembly_code_match.group(0)).group(0)
                    new_version = str(major) + '.' + str(minor) + '.' + str(build) + '.0'
                    print(line.replace(old_version, new_version), end='')
                elif file_code_match:
                    old_version = re.search(r'\d+\.\d+\.\d+\.\d+', file_code_match.group(0)).group(0)
                    new_version = str(major) + '.' + str(minor) + '.' + str(build) + '.0'
                    print(line.replace(old_version, new_version), end='')
                elif product_code_match:
                    old_version = re.search(r'\d+\.\d+\.\d+\.\d+', product_code_match.group(0)).group(0)
                    new_version = str(major) + '.' + str(minor) + '.' + str(build) + '.0'
                    print(line.replace(old_version, new_version), end='')
                else:
                    print(line, end='')


# "ProductCode" = "8:{639C12D3-A206-4CA0-BB33-011FB5227A50}"
# "PackageCode" = "8:{E246BA9F-CBC9-4D3D-A3E1-8AD505E7896C}"
# "ProductVersion" = "8:1.2.78"
def update_vdproj_version_string(file_name, major, minor, build):
    if os.path.exists(file_name):
        with fileinput.FileInput(file_name, inplace=True) as assembly_file:
            for line in assembly_file:
                product_code_match = re.match(r'\s*"ProductCode" = "8:{(.*)}"', line)
                package_code_match = re.match(r'\s*"PackageCode" = "8:{(.*)}"', line)
                production_version_match = re.match(r'\s*"ProductVersion" = "8:(.*)"', line)
                if product_code_match:
                    old_uuid = re.search(r'\{(.*?)}', product_code_match.group(0)).group(1)
                    new_uuid = str(uuid.uuid4()).upper()
                    print(line.replace(old_uuid, new_uuid), end='')
                elif package_code_match:
                    old_uuid = re.search(r'\{(.*?)}', package_code_match.group(0)).group(1)
                    new_uuid = str(uuid.uuid4()).upper()
                    print(line.replace(old_uuid, new_uuid), end='')
                elif production_version_match:
                    old_version = production_version_match.group(0).split(':')[-1]
                    new_version = str(major) + '.' + str(minor) + '.' + str(build) + '"'
                    print(line.replace(old_version, new_version), end='')
                else:
                    print(line, end='')


def update_android_version_string(file_name, major, minor, build, upgrade_code):
    if os.path.exists(file_name):
        with fileinput.FileInput(file_name, inplace=True) as grade_file:
            for line in grade_file:
                code_match = re.match(r'version_code=\d+', line)
                name_match = re.match(r'version_name=\d+\.\d+\.\d+', line)
                if code_match:
                    group = code_match.group(0)
                    print(line.replace(group, 'version_code=' + str(upgrade_code)), end='')
                elif name_match:
                    group = name_match.group(0)
                    print(line.replace(group, 'version_name=' + str(major) + '.' + str(minor) + '.' + str(build)), end='')
                else:
                    print(line, end='')


if __name__ == '__main__':

    if len(sys.argv) < 2:
        print('Please provide an input file you fool')
        exit(1)

    base_dir = sys.argv[1]

    if len(sys.argv) < 3:
        print('Please provide a version (file) you fool')
        exit(1)

    var = sys.argv[2]
    # Version file as argument
    if os.path.exists(var):
        with open(var, 'r') as file:
            version_string = file.readline().strip()
            upgrade_code = file.readline().strip()
        if '.' in version_string:
            data = version_string.split('.')
        else:
            data = version_string.split(' ')
        major = data[0]
        minor = data[1]
        build = data[2]
    # Version as argument
    else:
        major = var
        minor = sys.argv[3]
        build = sys.argv[4]

    print('UPDATING TO VERSION ' + str(major) + '.' + str(minor) + '.' + str(build) + ' UPGRADE CODE: ' + upgrade_code)

    # .Net AssemblyInfo.cs files
    # files = glob.glob(base_dir + '.NET\\**\\AssemblyInfo.cs', recursive=True)
    # if files:
    #     for file in files:
    #         update_assembly_info_version_string(file, major, minor, build)
    #         print('replaced version in ' + file)

    # .Net csproj files
    files = glob.glob(base_dir + '.NET\\**\\*.csproj', recursive=True)
    if files:
        for file in files:
            update_csproj_version_string(file, major, minor, build)
            print('replaced version in ' + file)

    # Setup project .vdproj files
    files = glob.glob(base_dir + '**\\*.vdproj', recursive=True)
    if files:
        for file in files:
            update_vdproj_version_string(file, major, minor, build)
            print('replaced version in ' + file)

    # Android Gradle files
    android_version_file = base_dir + 'Android\\Terminal\\gradle.properties'
    update_android_version_string(android_version_file, major, minor, build, upgrade_code)
    print('replaced version in ' + android_version_file)

    exit(0)

