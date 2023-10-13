import sys
import os

if __name__ == "__main__":

    database_dir = sys.argv[1]
    if not os.path.exists(database_dir):
        print("Directory does not exist")
        exit(1)
    print(database_dir)

    for f in os.listdir(database_dir):
        if not f.startswith("_") and f.endswith(".sql"):
            os.remove(database_dir + f)
