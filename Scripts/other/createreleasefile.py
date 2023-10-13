import sys
import os
import subprocess

from git import *

def getChangeLog(repo_directory):
    os.chdir(repo_directory)
    repo = Repo(repo_directory)
    assert repo.bare == False

    tags = repo.tags
    start_tag = ""
    last_tag = -1
    for tag in tags:
        if tag.tag is not None:
            if tag.tag.tagged_date > last_tag:
                last_tag = tag.tag.tagged_date
                start_tag = tag.tag.tag
    log_txt = "Changes from version " + start_tag + "\n"
    command = "git log " + start_tag + "..HEAD --date=short --graph --pretty=format:'%h%x09%an%x09%ad%x09%s%d'"
    print(command)
    output = subprocess.check_output(command, stderr=subprocess.STDOUT, shell=True)
    log_txt += output.decode('utf-8')
    return log_txt


if __name__ == "__main__":

    now = sys.argv[1]
    version = sys.argv[2]
    upgrade_code = sys.argv[3]
    client = sys.argv[4]
    release_dir = sys.argv[5]
    repo_dir = sys.argv[6]

    filePath = release_dir + "/release_summary_" + version + ".txt" 
    with open(filePath, 'w') as f:
        f.write(" Release Summary for " + version + "\n")
        f.write(" --------------------------------\n")
        f.write("\n")
        f.write("Release Info\n")
        f.write("- Version: " + version + "\n")
        f.write("- Upgrade code: " + upgrade_code + "\n")
        f.write("- Customer: " + client + "\n")
        f.write("- Date: " + now + "\n")
        f.write("\n")
        f.write("What's new\n")
        f.write(getChangeLog(repo_dir))

    